using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Fauna
{
    public int index;
    public string classe; //mammiferp,anfibi ecc..
    public string nomeComune; //capriolo,capra..
    public string nomeLatino;
    public string ADistr;
    public string AProtetta;
    public string descr;
    public string descrENG;
    public string typeENG;
    public string nameENG;
    public string livC;
    public bool specB;

    public string[] regioni;

    // Update is called once per frame
    public Fauna(Fauna r)
    {
        classe = r.classe;
        nomeComune = r.nomeComune;
        nomeLatino = r.nomeLatino;
        ADistr = r.ADistr;
        AProtetta = r.AProtetta;
        descr = r.descr;
        descrENG = r.descrENG;
        nameENG= r.nameENG;
        typeENG= r.typeENG;
        regioni = r.regioni;
        livC = r.livC;
        specB = r.specB;
    }
}
