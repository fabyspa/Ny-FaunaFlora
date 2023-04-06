using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    public Button yourButton;
    public GameObject canvas;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        canvas = transform.root.gameObject;



        if (this.gameObject.tag == Loader.currentScene.ToString())
        {
            btn.onClick.AddListener(Loader.SwitchScene);
        }
        else
        {
            btn.interactable = false;
        }


    }
}