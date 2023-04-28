using AirFishLab.ScrollingList;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SoundManager
{
    public static CircularScrollingListFlora circularScrollingListFlora;
    public static CircularScrollingListFauna circularScrollingListFauna;
    public static GameObject GetAudioSourceToReproduce()
    {
        Debug.Log("play");
        if (circularScrollingListFlora != null || circularScrollingListFauna != null)
        {
            if (SceneManager.GetActiveScene().name == "Fauna")
            {
                return circularScrollingListFauna.gameObject ;
            }
            else if (SceneManager.GetActiveScene().name == "Flora")
            {
                return circularScrollingListFlora.gameObject; 

            }
            else return null;
        }
        else return null;

    }

}
