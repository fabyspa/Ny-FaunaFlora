using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] List<string> title;
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] GameObject label;
    Toggle toggle;
    Vector2 handlePosition;


    void Awake()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleRectTransform.anchoredPosition;
        //toggle.onValueChanged.AddListener(OnSwitch);
        InstantiateList(title);
        //if (toggle.isOn)
        //    OnSwitch(true);
    }

   void OnSwitch(bool on)
    {
        if (on)
            uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }

    List<string> InstantiateList(List<string> t)
    {
        List<string> title = t;
        Vector3 origin = new Vector3(0, 0, 0);
        if (title != null)
        foreach(string i in title)
        {
               //toggle.GetComponentInChildren<>
        }
        return title;
    }
}
