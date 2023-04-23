using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


namespace AirFishLab.ScrollingList
{
    public class VariableGameObjectListBankFlora : BaseListBank
    {

        LoadExcelFlora loadexcel;
        private List<Fauna> _contentsList = new List<Fauna>();
        public Fauna[] _contents;
        [SerializeField]
        private CircularScrollingListFlora _circularList;
        [SerializeField]
        private readonly DataWrapper _dataWrapper = new DataWrapper();
        private Image _image;

       

        public override object GetListContent(int index)
        {
            _dataWrapper.data = _contents[index];
            return _dataWrapper;
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }

        public void ChangeInfoContents(string type)
        {
            loadexcel = GameObject.FindObjectOfType<LoadExcelFlora>();
            _contentsList.Clear();
            
            if (type == "Tutte")
            {
                foreach (Fauna r in loadexcel.ordenList)
                {
                    //Debug.Log("PRRROVAAA"+r.nomeComune);
                    _contentsList.Add(r);
                }
                loadexcel.faunaDatabaseType.Clear();
                loadexcel.faunaDatabaseType.AddRange(loadexcel.ordenList);
                loadexcel.actualType = "Tutte";
                _contents = _contentsList.ToArray();
                _circularList.Refresh();
                GetCenterItem();
                
            }
            else if (loadexcel.type.Contains(type))
            {
                //Debug.Log("FILTRO PER TIPO");
                loadexcel.LoadFaunaByType(type);
                _contentsList.Clear();
                foreach (Fauna r in loadexcel.faunaDatabaseType)
                { 
                    _contentsList.Add(r);
                }

                _contents = _contentsList.ToArray();
                _circularList.Refresh();
                 GetCenterItem();

               

                //Debug.Log(loadexcel.pointList.Count);
            }

            //loadexcel.AddState();
           
        }

        //public void ChangeInfoContents()
        //{
        //    loadexcel = GameObject.FindObjectOfType<LoadExcelParchi>();
        //    _contentsList.Clear();

        //    foreach (Parco r in loadexcel.parchiDatabase)
        //    {
        //        _contentsList.Add(r);
        //    }
        //    _contents = _contentsList.ToArray();
        //    _circularList.Refresh();
        //    GetCenterItem();
        //    loadexcel.InstantiatePoints(loadexcel.parchiDatabase);



        //    //loadexcel.AddState();
        //    var myKey = loadexcel.coord2position.FirstOrDefault(x => Enumerable.SequenceEqual(x.Value, Convert_coordinates.remapLatLng(loadexcel.aItem.coord))).Key;
        //    loadexcel._oldGameObjecct = myKey;
        //}


        public Fauna GetCenterItem()
        {
           // Debug.Log("getCenterItem");
            int size = this.transform.childCount;
            //Debug.Log("size " + size);
            GameObject obj = this.transform.GetChild(size - 1).gameObject;
            //Debug.Log(obj.GetComponentInChildren<Text>().text);
            foreach (Fauna r in loadexcel.floraDatabase)
            {
                //Debug.Log("obj " + obj.GetComponentInChildren<Text>().text);
                if(r.nomeComune== obj.GetComponentInChildren<Text>().text)
                {
                    loadexcel.aItem = r;
                    //Debug.Log(r.name);
                    return r;
                }
              
            }
            return null;
        }
        /// <summary>
        /// Used for carry the data of value type to avoid boxing/unboxing
        /// </summary>
        public class DataWrapper
        {
            public Fauna data;
        }

        
    }
}
