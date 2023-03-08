using UnityEngine;
using UnityEngine.UI;
using System.Linq;
namespace AirFishLab.ScrollingList
{
    public class StringListBoxFauna : ListBox
    {
        [SerializeField]
        private Text _textITA;//, _textENG;

        private LoadExcelFloraFauna loadexcel;

        private void Start()
        {
            loadexcel = FindObjectOfType<LoadExcelFloraFauna>();
        }
        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableStringListBankFauna.DataWrapper)content;
            _textITA.text = dataWrapper.data;
            //if (dataWrapper.data == "Tutte") _textENG.text = "All";
            //else _textENG.text= loadexcel.ita2engType.FirstOrDefault(x => x.Value == dataWrapper.data).Key;
        }
    }
}

