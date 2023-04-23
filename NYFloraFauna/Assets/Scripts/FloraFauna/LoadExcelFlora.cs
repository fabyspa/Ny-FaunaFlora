using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadExcelFlora : MonoBehaviour
{
    public Fauna blankFauna;
    public List<Fauna> floraDatabase = new List<Fauna>();
    public List<Fauna> tempList = new List<Fauna>();
    public List<Fauna> faunaDatabaseType = new List<Fauna>();
    public List<Fauna> ordenList = new List<Fauna>();
    public Dictionary<GameObject, string[]> regioniSplit = new Dictionary<GameObject, string[]>();
    [SerializeField]
    public GameObject info, scheda, scrolling;
    public List<string> type = new List<string>();
    public List<string> regioni = new List<string>();
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

        scrolling.GetComponent<VariableStringListBankFlora>().ChangeContents();
        SortListByType();
        info.GetComponent<VariableGameObjectListBankFlora>().ChangeInfoContents("Tutte");
        scheda.GetComponent<VariableGameObjectListBankFlora>().ChangeInfoContents("Tutte");
       // scheda.GetComponentInChildren<VariableGameObjectListBankFlora>().ChangeInfoContents("Tutte");
        
       
    }



    public void LoadItemData()
    {
        loaded = false;
        faunaDatabaseType.Clear();
        type.Clear();
        ita2engType.Clear();
        //READ CSV FILE
        List<Dictionary<string, object>>  data = CSVReader.Read("FLORA_Nuovo");
        InstantiateFloraFauna(data);
        loaded = true;
      
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
            string livC = data[i]["Livello di Conservazione"].ToString();
            bool specB = false;
            if (data[i]["Specie bandiera"].ToString() == "si")
            {
                specB = true;
            }
            string[] regioni = data[i]["Regione"].ToString().Split(delimiters);
            
            AddFauna(classe, nomeComune, nomeLatino, ADistr, Aprotetta, descr, nameENG, typeENG, descrENG, livC, specB, regioni);
            
        }
        GetFaunaTypes();
    }

    void AddFauna(string classe, string nomeComune, string nomeLatino,  string ADistr, string AProtetta, string descr, string nomeENG, string typeENG, string descrENG, string livC, bool specB, string[] regioni)
    {
        Fauna tempItem = new Fauna(blankFauna);
        tempItem.classe = classe;
        tempItem.nomeComune = nomeComune;
        tempItem.nomeLatino = nomeLatino;
        tempItem.ADistr = ADistr;
        tempItem.AProtetta = AProtetta;
        tempItem.descr = descr;
        tempItem.typeENG = typeENG;
        tempItem.descrENG = descrENG;
        tempItem.nameENG = nomeENG;
        tempItem.livC = livC;
        tempItem.specB = specB;
        tempItem.regioni = regioni;
        
        floraDatabase.Add(tempItem);

        
    }
    public void GetFaunaTypes()
    {
        var index = 0;
        
        foreach (Fauna r in floraDatabase)
        {
           if (!type.Contains(r.classe)){
                type.Add(r.classe);
            }

            if (!ita2engType.ContainsValue(r.classe))
            {
                if (r.typeENG != "")
                {
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

    }

    public List<Fauna> LoadFaunaByType(string type)
    {
        if (actualType != type)
        {
            faunaDatabaseType.Clear();
            foreach (Fauna r in floraDatabase)
            {
                if (r.classe.ToUpper() == type.ToUpper())
                {
                    faunaDatabaseType.Add(r);
                }
            }
            actualType = type;
        }
        return faunaDatabaseType;
    }

    public List<Fauna> SortListByType()
    {
        ordenList.Clear();
        foreach (string t in type)
        {
            ordenList.AddRange(LoadFaunaByType(t));
        }
        return ordenList;

    }
    public Fauna LoadFaunaByName(string name)
    {

        foreach (Fauna r in floraDatabase)
        {
            if (r.nomeComune == name) return r;
        }

        return null;

    }

    //Crea una lista con tutte le regioni
    public void GetFaunaReg()
    {
        foreach (Fauna f in floraDatabase)
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
        if (SceneManager.GetActiveScene().name == "Fauna")
        {
            var i = FindObjectOfType<CircularScrollingListFauna>();
            i._isInitialized = false;
            i.Initialize();

            info.GetComponent<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");
        }
       
        if(SceneManager.GetActiveScene().name =="Flora")
        {
            var i = FindObjectOfType<CircularScrollingListFlora>();
            i._isInitialized = false;
            i.Initialize();

            info.GetComponent<VariableGameObjectListBankFlora>().ChangeInfoContents("Tutte");
        }

        
    }

}
