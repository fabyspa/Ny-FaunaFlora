using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList
{
    public class VariableStringListBankRiserva : BaseListBank
    {

        LoadExcel loadexcel;
        //[SerializeField]
        //private InputField _contentInputField;
        private List<string> _contentsList = new List<string>();
        public string[] _contents;
        [SerializeField]
        private CircularScrollingListRiserva _circularList;
        //[SerializeField]
        //private CircularScrollingList _thirdCircular;
        // [SerializeField]
        // private CircularScrollingList _linearList;

        private readonly DataWrapper _dataWrapper = new DataWrapper();

        /// <summary>
        /// Extract the contents from the input field and refresh the list
        /// </summary>
        public void ChangeContents()
        {
            //Debug.Log("CHANGE");
            //if(_thirdCircular!=null)
            //_thirdCircular.GetComponent<VariableStringListBankRiserva>().ChangeContents();
            _contentsList.Add("Tutte");

            loadexcel = GameObject.FindObjectOfType<LoadExcel>();
           
                foreach (string t in loadexcel.type)
                {
                    _contentsList.Add(t);
                    //_contentInputField.text.Split(
                    //    new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }

            _contents = _contentsList.ToArray();
            _circularList.Refresh();
            //_linearList.Refresh();
        }

        public override object GetListContent(int index)
        {
            _dataWrapper.data = _contents[index];
            return _dataWrapper;
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }

        /// <summary>
        /// Used for carry the data of value type to avoid boxing/unboxing
        /// </summary>
        public class DataWrapper
        {
            public string data;
        }

        //public void ChangeInfoContents(string type)
        //{
        //    loadexcel = GameObject.FindObjectOfType<LoadExcel>();

        //    //Debug.Log(type);
        //    if (loadexcel.type.Contains(type) )
        //    {
        //        loadexcel.LoadRiservaByType(type);
        //        _contentsList.Clear();
        //        foreach (Riserva r in loadexcel.riservaDatabaseType)
        //        {
        //            _contentsList.Add(r.name);
        //            //_contentInputField.text.Split(
        //            //    new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //        }
        //        //loadexcel.InstantiatePoints(loadexcel.riservaDatabaseType);
        //        loadexcel.InstantiatePoints(loadexcel.riservaDatabase);
        //        _contents = _contentsList.ToArray();
        //        _circularList.Refresh();
                
        //    }
            

       // }       
    }
}
