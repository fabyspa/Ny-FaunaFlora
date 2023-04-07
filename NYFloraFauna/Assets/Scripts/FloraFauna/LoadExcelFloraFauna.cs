using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadExcelFloraFauna : MonoBehaviour
{
    public Fauna blankFauna;
    public List<Fauna> faunaDatabase = new List<Fauna>();
    public List<Fauna> floraDatabase = new List<Fauna>();
    public List<Fauna> tempList = new List<Fauna>();
    public List<Fauna> faunaDatabaseType = new List<Fauna>();
    public List<Fauna> ordenList = new List<Fauna>();
    public Dictionary<GameObject, string[]> regioniSplit = new Dictionary<GameObject, string[]>();
    [SerializeField]
    public GameObject info, scheda;
    public List<string> type = new List<string>();
    public List<string> regioni = new List<string>();
    [SerializeField] GameObject scrolling;
    public string actualType;
    public Fauna aItem;
    public Dictionary<string, string> ita2engType = new Dictionary<string, string>();
    public bool loaded;

    [SerializeField]
    public GameObject regGameObject;
    private char[] delimiters = {','};

    public void Start()
    {
        LoadItemData();
        scrolling.GetComponent<VariableStringListBankFauna>().ChangeContents();
        SortListByType();
        info.GetComponent<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");
        scheda.GetComponent<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");
        scheda.GetComponentInChildren<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");    
    }

    public void Init()
    {
        LoadItemData();
        scrolling.GetComponent<VariableStringListBankFauna>().ChangeContents();
        SortListByType();
        info.GetComponent<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");
        scheda.GetComponent<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");
        scheda.GetComponentInChildren<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");
    }

    public void LoadItemData()
    {
        loaded = false;
        faunaDatabaseType.Clear();
        type.Clear();
        ita2engType.Clear();
        //READ CSV FILE
        if (SceneManager.GetActiveScene().name=="Master" && loaded==false)
        {
            Debug.Log("loaded" + loaded);
            List<Dictionary<string, object>> data = CSVReader.Read("FAUNA_Nuovo");
            InstantiateFloraFauna(data);
        }
        else if(SceneManager.GetActiveScene().name == "Flora" && loaded == false)
        {
            Debug.Log("FLORA");
            List<Dictionary<string, object>>  data = CSVReader.Read("FLORA_Nuovo");
            InstantiateFloraFauna(data);
            loaded = true;
        }
        GetFaunaReg();
    }

    void InstantiateFloraFauna(List<Dictionary<string, object>> data)
    {
        for (var i = 0; i < data.Count; i++)
        {
            string descr = data[i]["Descrizione ITA"].ToString();
            string classe = data[i]["Tipologia ITA"].ToString();
            string nomeLatino = data[i]["Nome latino"].ToString();
            string Aprotetta = data[i]["Area protetta raggruppamento cc biodiversita"].ToString();
            string ADistr = data[i]["Areale di distribuzione"].ToString();
            string nomeComune = data[i]["Nome ITA"].ToString();
            string nameENG = data[i]["Nome ENG"].ToString();
            string typeENG = data[i]["Tipologia ENG"].ToString();
            string descrENG = data[i]["Descrizione ENG"].ToString();
            string[] regioni = data[i]["Regione"].ToString().Split(delimiters);

            //0=fauna 1=flora
            if (classe != "" && (SceneManager.GetActiveScene().name=="Master"))
                AddFauna(0,classe, nomeComune, nomeLatino, ADistr, Aprotetta, descr, nameENG, typeENG, descrENG, regioni);

            else if (classe != "" && SceneManager.GetActiveScene().name == "Flora")
            {
                Debug.Log("ADD FLORA");
                AddFauna(1, classe, nomeComune, nomeLatino, ADistr, Aprotetta, descr, nameENG, typeENG, descrENG, regioni);
            }
        }
        GetFaunaTypes();
    }

    void AddFauna(int faunaFlora, string classe, string nomeComune, string nomeLatino,  string ADistr, string AProtetta, string descr, string nomeENG, string typeENG, string descrENG, string[] regioni)
    {
        Fauna tempItem = new Fauna(blankFauna);
        tempItem.florafauna= faunaFlora;
        tempItem.classe = classe;
        tempItem.nomeComune = nomeComune;
        tempItem.nomeLatino = nomeLatino;
        tempItem.ADistr = ADistr;
        tempItem.AProtetta = AProtetta;
        tempItem.descr = descr;
        tempItem.typeENG = typeENG;
        tempItem.descrENG = descrENG;
        tempItem.nameENG = nomeENG;
        tempItem.regioni = regioni;
        if(tempItem.florafauna==0)
            faunaDatabase.Add(tempItem);

        if(tempItem.florafauna==1)
        {
            floraDatabase.Add(tempItem);

        }
    }

    public List<Fauna> SwitchDB(){
        tempList.Clear();

        if (SceneManager.GetActiveScene().name == "Master" || SceneManager.GetActiveScene().name == "Fauna")
        {
            Debug.Log("TEMP LIST FAUNA");
            tempList.AddRange(faunaDatabase);
        }
        if (SceneManager.GetActiveScene().name == "Flora")
        {
            Debug.Log("TEMP LIST Flora");
            tempList.Clear();
            tempList.AddRange(floraDatabase);
        }
        return tempList;
    }
    public void GetFaunaTypes()
    {
        var index = 0;
        
        foreach (Fauna r in SwitchDB())
        {
           if (!type.Contains(r.classe)){
                type.Add(r.classe);
            }

            if (!ita2engType.ContainsValue(r.classe))
            {
                if (r.typeENG != "")
                {
                    Debug.Log(r.typeENG);

                    ita2engType.Add(r.typeENG, r.classe);
                }
                else
                {
                    Debug.Log("vuoto");
                    ita2engType.Add("val" + index, r.classe);
                    index++;
                }
            }

        }
        Debug.Log(string.Join(",", ita2engType.Keys));

    }

    public List<Fauna> LoadFaunaByType(string type)
    {
        if (actualType != type)
            faunaDatabaseType.Clear();
            foreach (Fauna r in SwitchDB())
            {
                if (r.classe.ToUpper() == type.ToUpper())
                {
                    faunaDatabaseType.Add(r);
                }
            }
            actualType = type;
        return faunaDatabaseType;
    }

    public List<Fauna> SortListByType()
    {
        foreach (string t in type)
        {
            ordenList.AddRange(LoadFaunaByType(t));
        }
        return ordenList;

    }
    public Fauna LoadFaunaByName(string name)
    {

        foreach (Fauna r in SwitchDB())
        {
            if (r.nomeComune == name) return r;
        }

        return null;

    }

    //Crea una lista con tutte le regioni
    public void GetFaunaReg()
    {
        foreach (Fauna f in SwitchDB())
        {
            foreach(string s in f.regioni)
            {

                if (!regioni.Contains(s) && s != "")
                {
                    regioni.Add(s);
                }
            }
            
        }

    }

    public void ResetScroll()
    {
        var i = FindObjectOfType<CircularScrollingListFauna>();
        i._isInitialized = false;
        i.Initialize();

        info.GetComponent<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");


        
    }

}
