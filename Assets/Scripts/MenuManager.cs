using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        // fetch mainMenu object
        mainMenu = GameObject.Find("MainMenu");
        // fetch levelSelect object
        levelSelect = GameObject.Find("LevelSelect");

        // hide levelSelect
        levelSelect.SetActive(false);

        // get scene loader from main camera
        SceneLoader sceneLoader = Camera.main.GetComponent<SceneLoader>();
    }

    public void showLevelSelect()
    {
        // show levelSelect
        levelSelect.SetActive(true);
        // hide mainMenu
        mainMenu.SetActive(false);
    }

    public void showMainMenu()
    {
        // show mainMenu
        mainMenu.SetActive(true);
        // hide levelSelect
        levelSelect.SetActive(false);
    }

    public void playButton(int level)
    {
        // load level
        // this can easily have a load screen by adjusting the string!
        // make sure that the level is in the build settings
        SceneManager.LoadScene("Level " + level);
    }

    // ty to miles for this, git blame won't show it <3
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player Quit Game");
    }
}
