using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace AirFishLab.ScrollingList.Demo
{
    public class GameObjectListBoxFauna : ListBox
    {
        [SerializeField]
        private Text _name, _nameENG;
        [SerializeField]
        private Image _image;
        private Sprite tex;
       
        
        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableGameObjectListBankFauna.DataWrapper) content;
            _name.text = dataWrapper.data.nomeComune;
            _nameENG.text = dataWrapper.data.nameENG;

            _image.sprite = UpdateImage(dataWrapper.data.nomeComune);

        }
        public Sprite UpdateImage(string _name)
        {
            if (Resources.Load<Sprite>("Vectors_FLORAFAUNA/" + _name) != null)
            {
                tex = Resources.Load<Sprite>("vectors_FLORAFAUNA/" + _name);
                return tex;
            }
            return null;
        }

    }
    
}
