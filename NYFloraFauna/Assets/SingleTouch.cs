
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SingleTouch: StandaloneInputModule
{

    protected override void Start()
    {
        base.Start();
        Input.multiTouchEnabled = false;
    }

}


