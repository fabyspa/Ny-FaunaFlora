using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace AirFishLab.ScrollingList
{
    
    /// <summary>
    /// Manage and control the circular scrolling list
    /// </summary>
    public class CircularScrollingListFauna : MonoBehaviour,
        IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
    {
        private enum DirectionScroll
        {
            Right,
            Left
        }
        private GameObject info;
        private GameObject scheda;
        public string tagScroll;
        private DirectionScroll direction;
        public bool _toFixInfo;
        public bool _toFixScheda;

        #region Enum Definitions

        /// <summary>
        /// The type of the list
        /// </summary>
        public enum ListType
        {
            Circular,
            Linear
        };

        /// <summary>
        /// The controlling mode of the list
        /// </summary>
        public enum ControlMode
        {
            /// <summary>
            /// Control the list by the mouse pointer or finger
            /// </summary>
            Drag,
            /// <summary>
            /// Control the list by invoking functions
            /// </summary>
            Function,
            /// <summary>
            /// Control the list by the mouse wheel
            /// </summary>
            MouseWheel
        };

        /// <summary>
        /// The major moving direction of the list
        /// </summary>
        public enum Direction
        {
            Vertical,
            Horizontal
        };

        #endregion

        #region Settings

        [SerializeField]
        [Tooltip("The game object that stores the contents for the list to display. " +
                 "It should be derived from the class BaseListBank.")]
        private BaseListBank _listBank;
        [SerializeField]
        [Tooltip("The game objects that used for displaying the content. " +
                 "They should be derived from the class ListBox")]
        public List<ListBox> _listBoxes;
        [SerializeField]
        [Tooltip("The setting of this list")]
        private CircularScrollingListSetting _setting;

        //[SerializeField]
        //CircularScrollingListRiserva _info;
        #endregion

        #region Exposed Properties

        public BaseListBank listBank => _listBank;
        public CircularScrollingListSetting setting => _setting;

        #endregion

        #region Private Members
        
        /// <summary>
        /// The rect transform that this list belongs to
        /// </summary>
        private RectTransform _rectTransform;
        /// <summary>
        /// The camera that the parent canvas is referenced
        /// </summary>
        private Camera _canvasRefCamera;
        /// <summary>
        /// The component that controlling the position of each box
        /// </summary>
        public ListPositionCtrl _listPositionCtrl;
        /// <summary>
        /// The component that controlling the content for each box
        /// </summary>
        private ListContentManager _listContentManager;
        /// <summary>
        /// Is the list initialized?
        /// </summary>
        public bool _isInitialized;
        /// <summary>
        /// Does the list bank has no content?
        /// </summary>
        /// It is used for blocking any input if the list has nothing to display.
        private bool _hasNoContent;

        #endregion

        private void Awake()
        {
            GetComponentReference();

            info =  GameObject.FindGameObjectWithTag("Info");
            scheda = GameObject.FindGameObjectWithTag("Scheda");

        }

        private void Start()
        {
            if (_setting.initializeOnStart)
                Initialize();
        }

        #region Initialization
        ///FABI
        
        /// <summary>
        /// Initialize the list
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized)
                return;

            InitializeListComponents();
            // Make the list position ctrl initialize its position state
            _listPositionCtrl.LateUpdate();
            _listPositionCtrl.InitialImageSorting();
            _isInitialized = true;
        }

        /// <summary>
        /// Get the reference of the used component
        /// </summary>
        private void GetComponentReference()
        {
            _rectTransform = GetComponent<RectTransform>();
            var parentCanvas = GetComponentInParent<Canvas>();
            if (parentCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
                _canvasRefCamera = parentCanvas.worldCamera;
        }

        /// <summary>
        /// Initialize the related list components
        /// </summary>
        private void InitializeListComponents()
        {
            _listPositionCtrl =
                new ListPositionCtrl(
                    _setting, _rectTransform, _canvasRefCamera, _listBoxes);
            _listContentManager =
                new ListContentManager(
                    _setting, _listBank, _listBoxes.Count);

            if (_setting.centerSelectedBox)
                _setting.onBoxClick.AddListener(SelectContentID);

            for (var i = 0; i < _listBoxes.Count; ++i)
                _listBoxes[i].Initialize(
                    _setting, _listPositionCtrl, _listContentManager,
                    _listBoxes, i);

            _hasNoContent = _listBank.GetListLength() == 0;
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Get the box that is closest to the center
        /// </summary>
        public ListBox GetCenteredBox()
        {
            return _listPositionCtrl.GetCenteredBox();
        }

        /// <summary>
        /// Get the content ID of the box that is closest to the center
        /// </summary>
        public int GetCenteredContentID()
        {
            return _listPositionCtrl.GetCenteredContentID();
        }

        /// <summary>
        /// Move the list one unit up or right
        /// </summary>
        public void MoveOneUnitUp()
        {
            if (_hasNoContent)
                return;

            _listPositionCtrl.SetUnitMove(30);
        }

        /// <summary>
        /// Move the list one unit down or left
        /// </summary>
        public void MoveOneUnitDown()
        {
            if (_hasNoContent)
                return;

            _listPositionCtrl.SetUnitMove(-30);
        }

        /// <summary>
        /// Make the boxes recalculate their content ID and
        /// reacquire the contents from the bank
        /// </summary>
        /// If the specified <c cref="centeredContentID">centeredContentID</c> is negative,
        /// it will take current centered content ID. <para />
        /// If current centered content ID is int.MinValue, it will be 0. <para />
        /// If current centered content ID is larger than the number of contents,
        /// it will be the ID of the last content.
        /// <param name="centeredContentID">
        /// The centered content ID after the list is refreshed
        /// </param>
        public void Refresh(int centeredContentID = -1)
        {
            Initialize();
            if (_listPositionCtrl != null)
            {
                var centeredBox = _listPositionCtrl.GetCenteredBox();
                if (centeredBox == null) centeredBox = _listBoxes[2];
                var numOfContents = _listBank.GetListLength();
                if (centeredContentID < 0)
                    centeredContentID =
                        centeredBox.contentID == int.MinValue
                            ? 0
                            : Mathf.Min(centeredBox.contentID, numOfContents - 1);
                else if (centeredContentID >= numOfContents)
                    throw new IndexOutOfRangeException(
                        $"{nameof(centeredContentID)} is larger than the number of contents");

                _listPositionCtrl.numOfLowerDisabledBoxes = 0;
                _listPositionCtrl.numOfUpperDisabledBoxes = 0;

                foreach (var listBox in _listBoxes)
                    listBox.Refresh(centeredBox.listBoxID, centeredContentID);

                _hasNoContent = numOfContents == 0;
            }

           
        }

        /// <summary>
        /// Select the specified content ID and make it be aligned at the center
        /// </summary>
        /// <param name="contentID">The target content ID</param>
        public void SelectContentID(int contentID)
        {
            if (_hasNoContent)
                return;

            if (!_listContentManager.IsIDValid(contentID))
                throw new IndexOutOfRangeException(
                    $"{nameof(contentID)} is larger than the number of contents");

            var centeredBox = _listPositionCtrl.GetCenteredBox();
            var centeredContentID = centeredBox.contentID;
            _listPositionCtrl.SetSelectionMovement(
                _listContentManager.GetShortestDiff(centeredContentID, contentID));
            //_listPositionCtrl.SetSelectionMovement( contentID - centeredContentID );
        }

        #endregion

        #region Event System Callback
        public void UpdateTagScroll()
        {
            //_toFixScheda = false;
            //_toFixInfo = false;
            tagScroll = this.gameObject.tag.ToString();
            if (_listPositionCtrl.tagscroll!= tagScroll)
            {
                _listPositionCtrl.tagscroll = tagScroll;
            }

        }
        void MoveScrollUp(PointerEventData e, TouchPhase t)
        {
            scheda.GetComponent<CircularScrollingListFauna>()._listPositionCtrl.InputPositionHandler(e, t);
            scheda.GetComponent<CircularScrollingListFauna>()._toFixInfo = false;
            _toFixScheda= true;  
        } 
        
        void MoveScrollDown(PointerEventData e, TouchPhase t)
        {
            Debug.Log("move");

            //centeredContentId = GetCenteredContentID();
            //if(_listBank.GetListLength()>4)
            info.GetComponent<CircularScrollingListFauna>()._listPositionCtrl.InputPositionHandler(e, t);
            info.GetComponent<CircularScrollingListFauna>()._toFixScheda = false;
            _toFixInfo = true;
            
   
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_hasNoContent)
                return;
            UpdateTagScroll();
            //_toFix = false;
            if (tagScroll == "Info")
                MoveScrollUp(eventData, TouchPhase.Began);
            
            if (tagScroll == "Scheda")
                MoveScrollDown(eventData, TouchPhase.Began);

            _listPositionCtrl.InputPositionHandler(eventData, TouchPhase.Began);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_hasNoContent)
                return;
            if (tagScroll == "Info")
                MoveScrollUp(eventData, TouchPhase.Moved);
             if (tagScroll == "Scheda")
                MoveScrollDown(eventData, TouchPhase.Moved);


            _listPositionCtrl.InputPositionHandler(eventData, TouchPhase.Moved);
        }

       
        public void OnEndDrag(PointerEventData eventData)
        {
            if (_hasNoContent)
                return;

            if (tagScroll == "Info")
                MoveScrollUp(eventData,TouchPhase.Ended);
            if (tagScroll == "Scheda")
                MoveScrollDown(eventData,TouchPhase.Ended);

            //_toFixScheda = false;
            //_toFixInfo = false;
            _listPositionCtrl.InputPositionHandler(eventData, TouchPhase.Ended);
            //Debug.Log(string.Join("," ,eventData));
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (_hasNoContent)
                return;

            //if (tagScroll == "Info")
            //    MoveScrollUp(eventData, TouchPhase.Ended);
            // if (tagScroll == "Scheda")
            //    MoveScrollDown(eventData, TouchPhase.Ended);

            _listPositionCtrl.ScrollHandler(eventData);
        }

        #endregion

        private void Update()
        {
            if (!_isInitialized)
                return;

            _listPositionCtrl.Update();

        }

        private void LateUpdate()
        {
            if (!_isInitialized)
                return;

            _listPositionCtrl.LateUpdate();

           
                
                //if (centeredContentId!=oldContentId && tagScroll == "Scheda")_toFixInfo = true;
                //else _toFixInfo = false;
                //if (centeredContentId!=oldContentId && tagScroll == "Info") _toFixScheda= true;
                //else _toFixScheda= false;
                if (_toFixScheda||_toFixInfo)
                FixCardInfo();

        }

        private bool SameItem() {
            if (info.GetComponent<VariableGameObjectListBankFauna>().GetCenterItem() != scheda.GetComponent<VariableGameObjectListBankFauna>().GetCenterItem())
                return false;
            return true;    
        }

        private void FixCardInfo()
        {

           //_toFix = false;
            int indice_i = 0;
            int indice_j = 0;
            if (_toFixScheda)
            {
                if (!SameItem())
                {
                    for (int i = 0; i < info.GetComponent<VariableGameObjectListBankFauna>()._contents.Length; i++)
                    {
                        if (info.GetComponent<VariableGameObjectListBankFauna>()._contents[i].index == info.GetComponent<VariableGameObjectListBankFauna>().GetCenterItem().index)
                        {
                            indice_i = i;
                        }
                    }
                    for (int j = 0; j < info.GetComponent<VariableGameObjectListBankFauna>()._contents.Length; j++)
                    {
                        if (scheda.GetComponent<VariableGameObjectListBankFauna>()._contents[j].index == scheda.GetComponent<VariableGameObjectListBankFauna>().GetCenterItem().index)
                        {
                            indice_j = j;
                        }
                    }
                    int diff = Mathf.Abs(indice_i - indice_j);
                        scheda.GetComponent<CircularScrollingListFauna>().SelectContentID(indice_i);
                   
                    //else
                    //    scheda.GetComponent<CircularScrollingListFauna>()._listPositionCtrl.SetUnitMove(3 * diff);
                    //oldContentId = centeredContentId;
                }
               //_toFixScheda= false;
            }
            else if (_toFixInfo)
            {
                if (!SameItem())
                {
                    for (int i = 0; i < info.GetComponent<VariableGameObjectListBankFauna>()._contents.Length; i++)
                    {
                        if (info.GetComponent<VariableGameObjectListBankFauna>()._contents[i].index == info.GetComponent<VariableGameObjectListBankFauna>().GetCenterItem().index)
                        {
                            indice_i = i;
                        }
                    }
                    for (int j = 0; j < info.GetComponent<VariableGameObjectListBankFauna>()._contents.Length; j++)
                    {
                        if (scheda.GetComponent<VariableGameObjectListBankFauna>()._contents[j].index == scheda.GetComponent<VariableGameObjectListBankFauna>().GetCenterItem().index)
                        {
                            indice_j = j;
                        }
                    }
                        info.GetComponent<CircularScrollingListFauna>().SelectContentID(indice_j);
                    //else
                     //info.GetComponent<CircularScrollingListFauna>()._listPositionCtrl.SetUnitMove(-3*diff);
                    //oldContentId = centeredContentId;

                }
                //_toFixInfo= false;

            }//scheda.GetComponent<CircularScrollingListFauna>().SelectContentID(indice_i);
                //_toFix = false;

         
        }

#if UNITY_EDITOR

        #region Editor Utility

        [ContextMenu("Assign References of Bank and Boxes")]
        private void AssignReferences()
        {
            _listBank = GetComponent<BaseListBank>();
            if (_listBoxes == null)
                _listBoxes = new List<ListBox>();
            else
                _listBoxes.Clear();
            foreach (Transform child in transform) {
                var listBox = child.GetComponent<ListBox>();
                if (listBox)
                    _listBoxes.Add(listBox);
            }
        }

        #endregion

#endif
    }
}
