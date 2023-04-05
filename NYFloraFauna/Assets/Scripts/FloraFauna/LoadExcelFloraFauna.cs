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
    public List<Fauna> faunaDatabaseType = new List<Fauna>();
    public List<Fauna> ordenList = new List<Fauna>();
    public Dictionary<GameObject, string[]> regioniSplit = new Dictionary<GameObject, string[]>();
    [SerializeField]
    public GameObject info,scheda;
    public List<string> type = new List<string>();
    public List<string> regioni = new List<string>();
    [SerializeField] GameObject scrolling;
    public bool loadedItems = false;
    public string actualType;
    public Fauna aItem;
    public Dictionary<string, string> ita2engType = new Dictionary<string, string>();

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

    

    public void LoadItemData()
    {

        faunaDatabase.Clear();
        faunaDatabaseType.Clear();
        type.Clear();
        //READ CSV FILE
        if (SceneManager.GetActiveScene().name=="Fauna")
        {
            List<Dictionary<string, object>> data = CSVReader.Read("FAUNA_Nuovo");
            InstantiateFloraFauna(data);


        }
        else if(SceneManager.GetActiveScene().name == "Flora")
        {
            Debug.Log("FLORA");
            List<Dictionary<string, object>> data = CSVReader.Read("FloraBiodiversita");
            InstantiateFloraFauna(data);

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

            if(classe!="")
            AddFauna(classe, nomeComune, nomeLatino, ADistr, Aprotetta, descr, nameENG, typeENG, descrENG, regioni);
            

        }
        loadedItems = true;
        GetFaunaTypes();
    }

    void AddFauna(string classe, string nomeComune, string nomeLatino,  string ADistr, string AProtetta, string descr, string nomeENG, string typeENG, string descrENG, string[] regioni)
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
        tempItem.regioni = regioni;
        faunaDatabase.Add(tempItem);
    }

    public void GetFaunaTypes()
    {
        var index = 0;
        if (loadedItems == false) LoadItemData();
        
        foreach (Fauna r in faunaDatabase)
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
                    ita2engType.Add("val" + index, r.classe);
                    index++;
                }
            }
            

        }
        Debug.Log(String.Join(",", ita2engType.Keys.ToArray()));

    }

    public List<Fauna> LoadFaunaByType(string type)
    {
        if (actualType != type)
        {
            faunaDatabaseType.Clear();
            if (loadedItems == false) LoadItemData();
            foreach (Fauna r in faunaDatabase)
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
        foreach (string t in type)
        {
            ordenList.AddRange(LoadFaunaByType(t));
        }
        return ordenList;

    }
    public Fauna LoadFaunaByName(string name)
    {

        foreach (Fauna r in faunaDatabase)
        {
            if (r.nomeComune == name) return r;
        }

        return null;

    }

    //Crea una lista con tutte le regioni
    public void GetFaunaReg()
    {
        foreach (Fauna f in faunaDatabase)
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







}
