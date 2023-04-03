﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
namespace AirFishLab.ScrollingList.Demo
{
    public class GameObjectListBoxFaunaScheda : ListBox
    {
        [SerializeField]
        private Text _name, _latinName, _descr, _type;
        [SerializeField]
        private Image _vector,_image;
        private Sprite tex; 
        private GameObject regGameObject;
        [SerializeField]
        private Sprite emptySprite;
        public bool isLoaded=false;
        LoadExcelFloraFauna loadExcel;
        private GameObject parent;


        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableGameObjectListBankFauna.DataWrapper) content;
            _name.text = dataWrapper.data.nomeComune;
            _latinName.text = dataWrapper.data.nomeLatino;
            _descr.text = dataWrapper.data.descr;
            _type.text = dataWrapper.data.classe;
             _image.sprite = UpdateImage(dataWrapper.data.nomeComune);
             _vector.sprite = UpdateImageIcon(dataWrapper.data.nomeComune);
            if(isLoaded==false)
            LoadGameObject();
        }
        public Sprite UpdateImageIcon(string _name)
        {
            if (Resources.Load<Sprite>("Vectors_FLORAFAUNA/" + _name) != null)
            {
                tex = Resources.Load<Sprite>("Vectors_FLORAFAUNA/" + _name);
                return tex;

            }
            return null;
        }
        
        public Sprite UpdateImage(string _name)
        {
            if (Resources.Load<Sprite>("Images_FLORAFAUNA/" + _name) != null)
            {
                tex = Resources.Load<Sprite>("Images_FLORAFAUNA/" + _name);
                return tex;

            }
            return null;
            

        }

        public Sprite LoadRegion(string _name)
        {
            //regisloaded = false;
            //InstRegion();
            if (Resources.Load<Sprite>("Images_FLORAFAUNA/Italy_FLORAFAUNA/" + _name) != null)
            {
                Debug.Log("img: " + _name);
                tex = Resources.Load<Sprite>("Images_FLORAFAUNA/Italy_FLORAFAUNA/" + _name);
                return tex;

            }
            return null;
        }



        //prende in input la stringa con le regioni e la spitta in array
        //public void UpdateItaly(string regioni)
        //{
        //    if (reg != null)
        //    {
        //        for (int i = 0; i < reg.Length; i++)
        //        { 
        //            var instanciated = Instantiate(regGameObject);
        //            Sprite s = regGameObject.GetComponent<Sprite>();
        //            s = LoadRegion(reg[i]);
        //        }
        //    }
        //}


        public void LoadGameObject()
        {
            loadExcel = GameObject.FindObjectOfType<LoadExcelFloraFauna>();
            parent = this.transform.Find("Italia").gameObject;
            regGameObject = loadExcel.regGameObject;
            Vector3 pos =new Vector3(regGameObject.transform.position.x, regGameObject.transform.position.y, 0);
            Vector3 localSpacePosition = transform.InverseTransformPoint(pos);
            List<string> listaRegioni = loadExcel.regioni;
            for(int i = 0; i < listaRegioni.Count; i++)
            {
               
                Sprite regioniSprite=  LoadRegion(listaRegioni[i]);
                if (regioniSprite != null) { 
                var instanciated = Instantiate(regGameObject, this.transform.Find("Italia").Find("Base").transform.position, Quaternion.identity, parent.transform);
                instanciated.GetComponent<Image>().sprite = LoadRegion(listaRegioni[i]);
                }
                Debug.Log(listaRegioni[i]);
            }
            isLoaded = true;
        }
    }
    
}
