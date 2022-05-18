using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private SceneLoader instance;

    private int sceneNumber = 0;

    private bool menuLoaded = false;

    public void Awake()
    {
        instance = this;

        // async load menu

        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
        menuLoaded = true;
    }

    public void UnloadMenu()
    {
        SceneManager.UnloadSceneAsync("Menu");
    }

    public void NextLevel()
    {
        if (menuLoaded)
        {
            UnloadMenu();
            menuLoaded = false;
        }

        // unload current level
        SceneManager.UnloadSceneAsync("Level " + sceneNumber);

        // load next level
        SceneManager
            .LoadSceneAsync("Level " + sceneNumber, LoadSceneMode.Additive);
        sceneNumber++;
    }
}
