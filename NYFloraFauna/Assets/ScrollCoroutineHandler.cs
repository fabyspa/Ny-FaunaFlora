using AirFishLab.ScrollingList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCoroutineHandler : MonoBehaviour
{
    // Start is called before the first frame update
   public void ScrollScheda(ListPositionCtrl lpc)
    {
        Debug.Log("COROUTINE");
        StartCoroutine(lpc.CenteredBoxisChangedInfo());
    }

}
