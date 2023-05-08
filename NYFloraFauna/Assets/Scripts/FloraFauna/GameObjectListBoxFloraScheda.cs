﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
namespace AirFishLab.ScrollingList.Demo
{
    public class GameObjectListBoxFloraScheda : ListBox
    {
        [SerializeField]
        private Text _name, _descr, _type, _descrENG, _nameENG, _typeENG;
        [SerializeField]
        private TextMeshProUGUI _latinName;
        [SerializeField]
        private Image _vector,_image,_livC;
        private Sprite tex; 
        private GameObject regGameObject;
        [SerializeField]
        private Sprite emptySprite;
        public bool isLoaded=false;
        LoadExcelFlora loadExcel;
        private GameObject parent;
        private List<string> loadedRegions;
        [SerializeField]
        private GameObject infos;
        private SwitchITAENG toggle;
        private bool isInitialized = true;


        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableGameObjectListBankFlora.DataWrapper) content;
            _name.text = dataWrapper.data.nomeComune;
            _latinName.text = dataWrapper.data.nomeLatino;
            _descr.text = dataWrapper.data.descr;
            _descrENG.text = dataWrapper.data.descrENG;
            _nameENG.text = dataWrapper.data.nameENG;
            _typeENG.text = dataWrapper.data.typeENG;
            _type.text = dataWrapper.data.classe;
             _image.sprite = UpdateImage(dataWrapper.data.nomeComune);
             _vector.sprite = UpdateImageIcon(dataWrapper.data.nomeComune);
            _livC.sprite = UpdateLivC(dataWrapper.data.livC);


            infos.transform.GetChild(0).GetComponentInChildren<Text>().text = dataWrapper.data.ADistr;
            infos.transform.GetChild(1).GetComponentInChildren<Text>().text = dataWrapper.data.AProtetta;
            

            if (isInitialized)
            {
                toggle = GameObject.FindObjectOfType<SwitchITAENG>();
                isInitialized = false;
            }
            if (dataWrapper.data.specB)
            {
                infos.transform.GetChild(2).Find("Text").GetComponent<Text>().text = "Specie bandiera";
                infos.transform.GetChild(2).Find("Text_ENG").GetComponent<Text>().text = "Flagship species";
                infos.transform.GetChild(2).GetComponentInChildren<Image>().enabled = true;

                if (toggle.tag1 == "ENG")
                    infos.transform.GetChild(2).Find("Text").GetComponent<Text>().enabled = true;
                else
                    infos.transform.GetChild(2).Find("Text_ENG").GetComponent<Text>().enabled = true;
            }
            else
            {
                infos.transform.GetChild(2).Find("Text").GetComponent<Text>().text = "";
                infos.transform.GetChild(2).Find("Text_ENG").GetComponent<Text>().text = "";
                infos.transform.GetChild(2).GetComponentInChildren<Image>().enabled = false;
            }

            if (isLoaded==false)
            LoadGameObject();
            ActivateRegions(dataWrapper.data.regioni);
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
        public Sprite UpdateLivC(string _name)
        {
            if (Resources.Load<Sprite>("Images_IUCN/" + _name) != null)
            {
                tex = Resources.Load<Sprite>("Images_IUCN/" + _name);
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
                tex = Resources.Load<Sprite>("Images_FLORAFAUNA/Italy_FLORAFAUNA/" + _name);
                loadedRegions.Add(_name);

                return tex;

            }
            return null;
        }

        public void ActivateRegions(string[] regioni)
        {
            ClearRegions();
            foreach (string s in regioni)
            {
                foreach (string i in loadedRegions)
                {
                    if (s == i)
                    {
                        this.transform.Find("Italia").Find(s).GetComponent<Image>().enabled = true;
                    }
                }

            }

        }
        public void ClearRegions()
        {
            foreach (string i in loadedRegions)
            {
                this.transform.Find("Italia").Find(i).GetComponent<Image>().enabled = false;
                
            }
        }
        public void LoadGameObject()
        {
            loadExcel = GameObject.FindObjectOfType<LoadExcelFlora>();
            parent = this.transform.Find("Italia").gameObject;
            regGameObject = loadExcel.regGameObject;
            Vector3 pos =new Vector3(regGameObject.transform.position.x, regGameObject.transform.position.y, 0);
            Vector3 localSpacePosition = transform.InverseTransformPoint(pos);
            List<string> listaRegioni = loadExcel.regioni;
            loadedRegions = new List<string>();
            for (int i = 0; i < listaRegioni.Count; i++)
            {

                Sprite regioniSprite = LoadRegion(listaRegioni[i]);

                if (regioniSprite != null)
                {
                    var instanciated = Instantiate(regGameObject, this.transform.Find("Italia").Find("Base").transform.position, Quaternion.identity, parent.transform);
                    instanciated.GetComponent<Image>().sprite = LoadRegion(listaRegioni[i]);
                    instanciated.name = listaRegioni[i];
                }
                //Debug.Log(listaRegioni[i]);
            }
            isLoaded = true;
        }
    }
    
}
