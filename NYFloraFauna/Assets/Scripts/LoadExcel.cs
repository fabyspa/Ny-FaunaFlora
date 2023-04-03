using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList;
using System.Linq;
using UnityEngine.UI;


public class LoadExcel : MonoBehaviour
{
    //inizializzo un oggetto Riserva
    public Riserva blankRiserva;
    public List<Riserva> riservaDatabase = new List<Riserva>();
    public List<Riserva> riservaDatabaseType = new List<Riserva>();
    public List<Riserva> ordenList = new List<Riserva>();
   // public Image _image;
    public List<string> type = new List<string>();
    [SerializeField] GameObject scrolling;
    [SerializeField] VariableGameObjectListBankRiserva VariableGameObjectListBankRiserva;
    public bool loadedItems = false;
    public string actualType;
    public Dictionary<GameObject, float[]> coord2position = new Dictionary<GameObject, float[]>();
    public GameObject _oldGameObjecct;
    public Transform parent;
    public List<GameObject> pointList = new List<GameObject>();
    public Dictionary<string, string> ita2engType = new Dictionary<string, string>();
    //Ogetto contenente l'item attivo in questo momento
    public Riserva aItem;
    [SerializeField] GameObject point;



    public void Start()
    {
        LoadItemData();
        scrolling.GetComponent<VariableStringListBankRiserva>().ChangeContents();
        SortListByType();
        GameObject.FindGameObjectWithTag("Info").GetComponent<VariableGameObjectListBankRiserva>().ChangeInfoContents("Tutte");
        // Debug.Log("ITEM "+aItem.coord);
    }

    

    public void LoadItemData()
    {

        //clear database
        riservaDatabase.Clear();
        riservaDatabaseType.Clear();
        type.Clear();
        //READ CSV FILE
        List<Dictionary<string, object>> data = CSVReader.Read("Riserve_TraduzioneENG");
        for (var i = 0; i < data.Count; i++)
        {
            string type = data[i]["Type"].ToString();
            string name = data[i]["Name_ITA"].ToString();
            string coord = data[i]["Coord"].ToString();
            string descr = data[i]["Descr_ITA"].ToString();
            string descr_eng = data[i]["Descr_ENG"].ToString();
            string name_eng = data[i]["Name_ENG"].ToString();
            if (name_eng == "")
            {
                name_eng = name;
            }
            string luogo = data[i]["Luogo"].ToString();
            string anno = data[i]["Anno"].ToString();
            string sup = data[i]["Sup"].ToString();
            string region = data[i]["Regione"].ToString();
            string type_eng = data[i]["Type_ENG"].ToString();
            //Sprite sprite = UpdateImage((data[i]["Name_ITA"]).ToString());
           //Sprite sprite = null;
            AddRiserva(type, name, coord, descr,region,sup,anno,luogo,name_eng,descr_eng, type_eng);

        }
        loadedItems = true;
        GetRiservaTypes();
       // AddState();
        /* InstantiatePoints(riservaDatabase,tipo);*/
    }

    //public Sprite UpdateImage(string _name)
    //{
    //    if (Resources.Load<Sprite>("Images/" + _name) != null)
    //    {
    //        tex = Resources.Load<Sprite>("Images/" + _name);
    //        return tex;
    //    }
    //    return null;
    //}
    //se viene modificato il file excel da esterno facciamo in modo che si aggiorni direttamente la build
    public void ReLoadItemData()
    {
        loadedItems = false;
        LoadItemData();
    }

    void AddRiserva(string type, string name, string coord,  string descr, string region, string sup, string anno, string luogo, string name_eng, string descr_eng, string type_eng)
    {
        Riserva tempItem = new Riserva(blankRiserva);

        tempItem.type = type;
        tempItem.coord = coord;
        tempItem.name = name;
        tempItem.descr = descr;
        //tempItem.sprite = sprite;
        tempItem.region = region;
        tempItem.sup = sup;
        tempItem.anno = anno;
        tempItem.luogo = luogo;
        tempItem.name_eng = name_eng;
        tempItem.descr_eng = descr_eng;
        tempItem.type_eng = type_eng;
        riservaDatabase.Add(tempItem);
    }

    //Instanzio i punti passandogli la lista 
    public void InstantiatePoints(List<Riserva> r)
    {
        ClearPoints();
        coord2position.Clear();
        AddState();
        foreach (Riserva c in r) {
            float[] coord = Convert_coordinates.remapLatLng(c.coord);
            Vector3 worldSpacePosition = new Vector3(coord[1], coord[0], 0);
            Vector3 localSpacePosition = transform.InverseTransformPoint(worldSpacePosition);
            GameObject Tpoint = TransformPoint(c.state);

            var instanciated = Instantiate(Tpoint, localSpacePosition, Quaternion.identity, parent);
            pointList.Add(instanciated);
            //Debug.Log(instanciated.transform.localPosition);
           // Debug.Log(c.coord);
            if(!coord2position.ContainsKey(instanciated)) coord2position.Add(instanciated,coord);

           // Debug.Log(string.Join(",", coord2position));
        } 
    }

    public void CoordToPositionMap()
    {

    }

    public void ClearPoints()
    {
        foreach (GameObject c in pointList)
        {
            GameObject.Destroy(c);
        }
    }

    //aggiunge lo stato alla variabile state
    public void AddState()
    {
         Debug.Log("addstate");

        foreach (Riserva r in riservaDatabase)
        {
            if (riservaDatabaseType.Contains(r))
            {

                if (r.name == aItem.name)
                {
                    r.state = "selected";
                }
                else r.state = "active";
            }

            else
                r.state = "unselected";
        }

        
    }

    public void ChangeStateTo(GameObject g, string newstate)
    {
        Vector3 highlights = new Vector3((float)1.5, (float)1.5, 0);
        Vector3 grande = new Vector3((float)0.8, (float)0.8, 0);


        Riserva r = GetRiservaByCoord(g);
        if (_oldGameObjecct != null)
        {
            Riserva oldR = GetRiservaByCoord(_oldGameObjecct);
            oldR.state = "active";
            _oldGameObjecct.transform.localScale = grande;
        }
        r.state = newstate;
        g.transform.localScale = highlights;
        _oldGameObjecct = g;

    }
    //gestisco scala punti
    public GameObject TransformPoint(string state)
    {
        Debug.Log("transform");
        GameObject t = point;
        Vector3 piccolo = new Vector3((float)0.4, (float)0.4, 0);
        Vector3 grande = new Vector3((float)0.8, (float)0.8, 0);
        Vector3 highlights = new Vector3((float)1.5, (float)1.5, 0);
        switch (state)
        {
            case "active":
                t.transform.localScale = grande;
                break;
            case "selected":
                t.transform.localScale = highlights;
                break;
            default:
                t.transform.localScale = piccolo;
                break;
        }
         
            return t;
    }

    //torna tutti i tipi di riserve diverse
    public void GetRiservaTypes()
    {
        if (loadedItems == false) LoadItemData();
        
        foreach (Riserva r in riservaDatabase)
        {
            if (!type.Contains(r.type)){
                type.Add(r.type);
            }

            if (r.type_eng != "")
            {
                ita2engType.Add(r.type_eng, r.type);
            }
        }

        //Debug.Log(type);
    }

    public  List<Riserva> LoadRiservaByType(string type)
    {
        if (actualType != type)
        {
            riservaDatabaseType.Clear();
            if (loadedItems == false) LoadItemData();
            foreach (Riserva r in riservaDatabase)
            {
                if (r.type.ToUpper() == type.ToUpper())
                {
                    //r.sprite = UpdateImage(r.name);
                    riservaDatabaseType.Add(r);
                }
            }
            actualType = type;

        }

        return riservaDatabaseType;

    }
    public Riserva LoadRiservaByName(string name)
    {

        foreach (Riserva r in riservaDatabase)
        {
            if (r.name == name) return r;
        }

        return null;

    }

    public Riserva GetRiservaByCoord(GameObject p)
    {
        
        foreach(Riserva r in riservaDatabase)
        {
            float[] coord = Convert_coordinates.remapLatLng(r.coord);
            var value = new float[2];
            coord2position.TryGetValue(p, out value);
            //Debug.Log("val "+ string.Join(" ,", value));
            //Debug.Log("coord " + string.Join(" ,", coord));

            if (Enumerable.SequenceEqual(coord,value))
            {
                //Debug.Log("SELEZIONATA "+ r.name);
                return r;
            }
           
        }
        return null;
    }

    public List<Riserva> SortListByType()
    {
        foreach (string t in type)
        {
            ordenList.AddRange(LoadRiservaByType(t));
        }
        return ordenList;

    }

}
