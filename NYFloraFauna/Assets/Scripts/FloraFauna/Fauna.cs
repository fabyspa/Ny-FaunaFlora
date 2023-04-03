using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Fauna
{
    public string classe; //mammiferp,anfibi ecc..
    public string nomeComune; //capriolo,capra..
    public string nomeLatino;
    public string ADistr;
    //public Sprite sprite;
    public string AProtetta;
    public string descr;
    public string[] regioni;

    // Update is called once per frame
    public Fauna( Fauna r)
    {
        classe = r.classe;
        nomeComune = r.nomeComune;
        nomeLatino = r.nomeLatino;
        ADistr = r.ADistr;
        AProtetta = r.AProtetta;
        descr = r.descr;
        regioni = r.regioni;
    }
}
