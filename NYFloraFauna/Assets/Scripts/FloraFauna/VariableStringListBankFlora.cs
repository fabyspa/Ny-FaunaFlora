using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AirFishLab.ScrollingList
{
    public class VariableStringListBankFlora : BaseListBank
    {

        LoadExcelFlora loadexcel;

        private List<string> _contentsList = new List<string>();
        public string[] _contents;
        [SerializeField]
        private CircularScrollingListFlora _circularList;
        private readonly DataWrapper _dataWrapper = new DataWrapper();
        public void ChangeContents()
        {
            _contentsList.Add("Tutte");

            loadexcel = GameObject.FindObjectOfType<LoadExcelFlora>();

            foreach (string t in loadexcel.type)
            {
                _contentsList.Add(t);
            }
            _contents = _contentsList.ToArray();

            _circularList.Refresh();
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
    }
}

