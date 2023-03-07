using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Riserva
{
    public string type;
    public string name;
    public string coord;
    public string descr;
    public string state;
    //public Sprite sprite;
    public string name_eng;
    public string descr_eng;
    public string type_eng;
    public string sup;
    public string region;
    public string anno;
    public string luogo;
    // Update is called once per frame
    public Riserva( Riserva r)
    {
        type = r.type;
        name = r.name;
        coord = r.coord;
        descr = r.descr;
        //sprite= r.sprite;
        state = r.state;
        name_eng = r.name_eng;
        descr_eng = r.descr_eng;
        type_eng = r.type_eng;
        sup = r.sup;
        anno = r.anno;
        luogo = r.luogo;
        region = r.region;

    }
}
