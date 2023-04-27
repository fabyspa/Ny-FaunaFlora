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
    public static AudioSource GetAudioSourceToReproduce()
    {
        if (circularScrollingListFlora != null || circularScrollingListFauna != null)
        {
            if (SceneManager.GetActiveScene().name == "Fauna")
            {
                return circularScrollingListFauna.gameObject.GetComponent<AudioSource>();
            }
            else if (SceneManager.GetActiveScene().name == "Flora")
            {
                return circularScrollingListFauna.gameObject.GetComponent<AudioSource>();

            }
            else return null;
        }
        else return null;

    }

}
