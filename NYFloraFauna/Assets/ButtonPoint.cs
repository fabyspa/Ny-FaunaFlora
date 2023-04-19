using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AirFishLab.ScrollingList;


public class ButtonPoint : Button
{
    LoadExcel loadexcel;
    GameObject info;
    public int val=0;


    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override Selectable FindSelectableOnDown()
    {
        return base.FindSelectableOnDown();
    }

    public override Selectable FindSelectableOnLeft()
    {
        return base.FindSelectableOnLeft();
    }

    public override Selectable FindSelectableOnRight()
    {
        return base.FindSelectableOnRight();
    }

    public override Selectable FindSelectableOnUp()
    {
        return base.FindSelectableOnUp();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool IsActive()
    {
        return base.IsActive();
    }

    public override bool IsInteractable()
    {
        return base.IsInteractable();
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
    }

    public override void OnMove(AxisEventData eventData)
    {
        base.OnMove(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        int indice_i=0;
        int indice_j=0;

        base.OnPointerClick(eventData);

        if (loadexcel != null)
        {
            Vector3 localPosition = this.transform.localPosition;
            Riserva riserva = loadexcel.GetRiservaByCoord(this.gameObject);
            if (riserva.state == "active")
            {
                loadexcel.ChangeStateTo(this.gameObject, "selected");
                for (int  i = 0; i < info.GetComponent<VariableGameObjectListBankRiserva>()._contents.Length; i++)
                {
                    if (info.GetComponent<VariableGameObjectListBankRiserva>()._contents[i].name == info.GetComponent<VariableGameObjectListBankRiserva>().GetCenterItem().name)
                    {
                        indice_i = i;
                    }
                    if (info.GetComponent<VariableGameObjectListBankRiserva>()._contents[i].name == riserva.name)
                    {
                        indice_j = i;
                    }
                  }
               

                int diff= indice_i - indice_j ;
                int val = info.GetComponent<VariableGameObjectListBankRiserva>()._contents.Length;
                if (Mathf.Abs(diff) > (val -1 ) / 2) diff = Mathf.CeilToInt(-1* Mathf.Sign(diff)*(val - Mathf.Abs(diff)));

                if (diff>0)
                {
                    /*PROVVISORIO- per quache strano motivo quando si è su tutte non fsa il numero di salti giusti quando si è a metà*/
                   if (loadexcel.riservaDatabase.Count==loadexcel.riservaDatabaseType.Count && diff == (val-1)/2) diff++;

                    Debug.Log("NUMERO DI PASSI sinistra" + diff +" per raggiungere" + riserva.name +" da "+ info.GetComponent<VariableGameObjectListBankRiserva>().GetCenterItem().name);
                    info.GetComponent<CircularScrollingListRiserva>()._listPositionCtrl.SetUnitMove(1 * Mathf.Abs(diff));
                    loadexcel.aItem = riserva;
                }
                else
                {
                    /*PROVVISORIO*/
                    if (loadexcel.riservaDatabase.Count == loadexcel.riservaDatabaseType.Count  &&  diff == - (val - 1) / 2) diff--;

                    Debug.Log("NUMERO DI PASSI destra" + diff + " per raggiungere" + riserva.name + "da " + info.GetComponent<VariableGameObjectListBankRiserva>().GetCenterItem().name);
                    info.GetComponent<CircularScrollingListRiserva>()._listPositionCtrl.SetUnitMove(-1 * Mathf.Abs(diff));
                    loadexcel.aItem = riserva;

                }
               }
            }
       }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
    }

    public override void Select()
    {
        base.Select();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
    }

    protected override void InstantClearState()
    {
        base.InstantClearState();
    }

    protected override void OnBeforeTransformParentChanged()
    {
        base.OnBeforeTransformParentChanged();
    }

    protected override void OnCanvasGroupChanged()
    {
        base.OnCanvasGroupChanged();
    }

    protected override void OnCanvasHierarchyChanged()
    {
        base.OnCanvasHierarchyChanged();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void OnDidApplyAnimationProperties()
    {
        base.OnDidApplyAnimationProperties();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
    }

    protected override void OnTransformParentChanged()
    {
        base.OnTransformParentChanged();
    }

    //protected override void OnValidate()
    //{
    //    base.OnValidate();
    //}

    //protected override void Reset()
    //{
    //    base.Reset();
    //}

    protected override void Start()
    {
        base.Start();
        loadexcel = FindObjectOfType<LoadExcel>();
        info = GameObject.FindGameObjectWithTag("Info");

    }
}
