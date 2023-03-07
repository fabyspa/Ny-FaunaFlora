using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        addScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

    }
}
