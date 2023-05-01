using AirFishLab.ScrollingList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PlayAndDelete(GameObject go)
    {
        if(SceneManager.GetActiveScene().name=="Flora")
            if (!GameObject.FindFirstObjectByType<CircularScrollingListFlora>()._listPositionCtrl.isRunning) StartCoroutine(GameObject.FindFirstObjectByType<CircularScrollingListFlora>()._listPositionCtrl.PlayAndDelete(go, "Flora"));
       
        if(SceneManager.GetActiveScene().name=="Fauna")
            if (!GameObject.FindFirstObjectByType<CircularScrollingListFauna>()._listPositionCtrl.isRunning) StartCoroutine(GameObject.FindFirstObjectByType<CircularScrollingListFauna>()._listPositionCtrl.PlayAndDelete(go, "Fauna"));
    }

}
