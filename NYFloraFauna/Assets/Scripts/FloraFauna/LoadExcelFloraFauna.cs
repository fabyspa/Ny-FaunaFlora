using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList;
using System.Linq;
using UnityEngine.UI;


public class LoadExcelFloraFauna : MonoBehaviour
{
    public Fauna blankFauna;
    public List<Fauna> faunaDatabase = new List<Fauna>();
    public List<Fauna> faunaDatabaseType = new List<Fauna>();
    public List<Fauna> ordenList = new List<Fauna>();
    [SerializeField]
    public GameObject info,scheda;
    public List<string> type = new List<string>();
    [SerializeField] GameObject scrolling;
    public bool loadedItems = false;
    public string actualType;
    public Fauna aItem;



    public void Start()
    {
        LoadItemData();
        scrolling.GetComponent<VariableStringListBankFauna>().ChangeContents();
        SortListByType();
        info.GetComponent<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");
        scheda.GetComponent<VariableGameObjectListBankFauna>().ChangeInfoContents("Tutte");
    }

    

    public void LoadItemData()
    {

        faunaDatabase.Clear();
        faunaDatabaseType.Clear();
        type.Clear();
        //READ CSV FILE
        List<Dictionary<string, object>> data = CSVReader.Read("Fauna");
        for (var i = 0; i < data.Count; i++)
        {
            string classe = data[i]["Classe"].ToString();
            string nomeComune = data[i]["Nome comune"].ToString();
            string nomeLatino = data[i]["Nome latino"].ToString();
            string ADistr = data[i]["AREALE DI DISTRIBUZIONE"].ToString();
            string Aprotetta = data[i]["AREA PROTETTA RAGGRUPPAMENTO CC BIODIVERSITA'"].ToString();
            string descr = data[i]["DESCRIZIONE"].ToString();
            AddFauna(classe, nomeComune, nomeLatino, ADistr,Aprotetta,descr);

        }
        loadedItems = true;
         GetFaunaTypes();

    }

    void AddFauna(string classe, string nomeComune, string nomeLatino,  string ADistr, string AProtetta, string descr)
    {
        Fauna tempItem = new Fauna(blankFauna);

        tempItem.classe = classe;
        tempItem.nomeComune = nomeComune;
        tempItem.nomeLatino = nomeLatino;
        tempItem.ADistr = ADistr;
        tempItem.AProtetta = AProtetta;
        tempItem.descr = descr;
        faunaDatabase.Add(tempItem);
    }

    public void GetFaunaTypes()
    {
        if (loadedItems == false) LoadItemData();
        
        foreach (Fauna r in faunaDatabase)
        {
           if (!type.Contains(r.classe)){
                type.Add(r.classe);
            }
        }

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
}
