using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace AirFishLab.ScrollingList.Demo
{
    public class GameObjectListBoxFaunaScheda : ListBox
    {
        [SerializeField]
        private Text _name, _latinName, _descr, _type, _descrENG, _nameENG, _typeENG;
        [SerializeField]
        private Image _vector,_image;
        private Sprite tex;
        [SerializeField]
        private Sprite emptySprite;
        
        protected override void UpdateDisplayContent(object content)
        {
            var dataWrapper = (VariableGameObjectListBankFauna.DataWrapper) content;
            _name.text = dataWrapper.data.nomeComune;
            _latinName.text = dataWrapper.data.nomeLatino;
            _descr.text = dataWrapper.data.descr;
            _descrENG.text = dataWrapper.data.descrENG;
            _nameENG.text = dataWrapper.data.nameENG;
            _typeENG.text = dataWrapper.data.typeENG;
            _type.text = dataWrapper.data.classe;
             _image.sprite = UpdateImage(dataWrapper.data.nomeComune);
             _vector.sprite = UpdateImageIcon(dataWrapper.data.nomeComune);
        }
        public Sprite UpdateImageIcon(string _name)
        {
            if (Resources.Load<Sprite>("Vectors_FLORAFAUNA/" + _name) != null)
            {
                tex = Resources.Load<Sprite>("Vectors_FLORAFAUNA/" + _name);
                return tex;

            }
            return null;
            

        }public Sprite UpdateImage(string _name)
        {
            if (Resources.Load<Sprite>("Images_FLORAFAUNA/" + _name) != null)
            {
                tex = Resources.Load<Sprite>("Images_FLORAFAUNA/" + _name);
                return tex;

            }
            return null;
            

        }

    }
    
}
