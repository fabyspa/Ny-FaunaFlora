using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace AirFishLab.ScrollingList.Demo
{
    public class StringListBoxRiserva : ListBox
    {
        [SerializeField]
        private Text _textITA, _textENG;

        private LoadExcel loadexcel;

        private void Start()
        {
            loadexcel = FindObjectOfType<LoadExcel>();
        }
        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableStringListBankRiserva.DataWrapper) content;
            _textITA.text = dataWrapper.data;
            if (dataWrapper.data == "Tutte") _textENG.text = "All";
            else _textENG.text= loadexcel.ita2engType.FirstOrDefault(x => x.Value == dataWrapper.data).Key;
        }
    }
}
