using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AirFishLab.ScrollingList;

public class Loader
{
    public enum SceneName
    {
        FLORA,
        FAUNA
    }
    public static SceneName currentScene;
    private static bool isParkSceneLoaded;
    private static bool isRiservaSceneLoaded;
    private static bool toggle;
    public static void SwitchScene()
    {
        // Salva lo stato degli oggetti della scena corrente
        SaveToggleState();
        // Carica la nuova scena
        if (currentScene == SceneName.FLORA)
        {
            currentScene = SceneName.FAUNA;
            // Abilita/disabilita i GameObject delle scene in base alla scena corrente
            EnableDisableSceneObjects();

        }
        else
        {
            currentScene = SceneName.FLORA;
            // Abilita/disabilita i GameObject delle scene in base alla scena corrente
            EnableDisableSceneObjects();
        }
        // Carica lo stato degli oggetti della nuova scena
        LoadSceneState();
    }
    public static void EnableDisableSceneObjects()
    {
        if (currentScene == SceneName.FLORA && SceneManager.GetSceneByName(SceneName.FLORA.ToString()).isLoaded)
        {
            GameObject[] objectsInScene = SceneManager.GetSceneByName(SceneName.FAUNA.ToString()).GetRootGameObjects();
            // Salva lo stato di ogni oggetto
            Debug.Log(SceneManager.GetActiveScene().name);
            foreach (GameObject obj in objectsInScene)
            {
                obj.SetActive(false);
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName.FLORA.ToString()));
            GameObject[] objectsInSceneActive = SceneManager.GetActiveScene().GetRootGameObjects();
            // Salva lo stato di ogni oggetto
            foreach (GameObject obj in objectsInSceneActive)
            {
                obj.SetActive(true);
            }
            GameObject.FindGameObjectWithTag("TOGGLE").GetComponent<Toggle>().isOn = toggle;
            //if (isRiservaSceneLoaded) SceneManager.UnloadSceneAsync(SceneName.RISERVE.ToString());
            ResetScroll(SceneName.FLORA);

            isParkSceneLoaded = true;
            isRiservaSceneLoaded = false;
        }
        else if (currentScene == SceneName.FAUNA && SceneManager.GetSceneByName(SceneName.FAUNA.ToString()).isLoaded)
        {
            GameObject[] objectsInScene = SceneManager.GetSceneByName(SceneName.FLORA.ToString()).GetRootGameObjects();
            // Salva lo stato di ogni oggetto
            foreach (GameObject obj in objectsInScene)
            {
                obj.SetActive(false);
            }
            // Disabilita gli oggetti della scena Park
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName.FAUNA.ToString()));
            GameObject[] objectsInSceneActive = SceneManager.GetActiveScene().GetRootGameObjects();
            // Salva lo stato di ogni oggetto
            foreach (GameObject obj in objectsInSceneActive)
            {
                obj.SetActive(true);
            }
            GameObject.FindGameObjectWithTag("TOGGLE").GetComponent<Toggle>().isOn = toggle;
            ResetScroll(SceneName.FAUNA);

            //if (isParkSceneLoaded) SceneManager.UnloadSceneAsync(SceneName.PARCHI.ToString());
            isRiservaSceneLoaded = true;
            isParkSceneLoaded = false;
        }
    }
    public static void SaveSceneState()
    {
        // Ottiene tutti gli oggetti della scena corrente
        GameObject[] objectsInScene = SceneManager.GetActiveScene().GetRootGameObjects();
        // Salva lo stato di ogni oggetto
        foreach (GameObject obj in objectsInScene)
        {
            //ObjectState objectState = obj.GetComponent<ObjectState>();
            //if (objectState != null)
            //{
            //    objectState.SaveState();
            //}
        }
    }


    public static void SetCurrentScene(SceneName s)
    {
        currentScene = s;
    }

    public static void LoadSceneState()
    {
        // Ottiene tutti gli oggetti della nuova scena
        GameObject[] objectsInScene = SceneManager.GetActiveScene().GetRootGameObjects();
        // Carica lo stato di ogni oggetto
        foreach (GameObject obj in objectsInScene)
        {
            //ObjectState objectState = obj.GetComponent<ObjectState>();
            //if (objectState != null)
            //{
            //    objectState.LoadState();
            //}
        }
    }
    private static bool SaveToggleState()
    {
        toggle = GameObject.FindGameObjectWithTag("TOGGLE").GetComponent<Toggle>().isOn;
        return toggle;
    }

    private static void ResetScroll(SceneName scene)
    {
        if (scene == SceneName.FAUNA)
        {
            LoadExcelFloraFauna loadExcel = GameObject.FindObjectOfType<LoadExcelFloraFauna>();
            loadExcel.ResetScroll();
        }
        if (scene == SceneName.FLORA)
        {
            LoadExcelFloraFauna loadExcel = GameObject.FindObjectOfType<LoadExcelFloraFauna>();
            loadExcel.ResetScroll();
        }
    }


}

