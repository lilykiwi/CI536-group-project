using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject mainCamera;

    private GameController gameController;

    private GameObject pauseMenu;

    private GameObject failedMenu;

    private GameObject winMenu;

    // objects for buttons - unused atm
    //private GameObject pauseMenuRestartButton;
    //private GameObject pauseMenuMenuButton;
    //private GameObject failedMenuRestartButton;
    //private GameObject failedMenuMenuButton;
    //private GameObject winMenuNextButton;
    //private GameObject winMenuMenuButton;
    private GameObject pauseButton;

    private Text timer;

    private Text timerFG;

    private Text winMenuParText;

    private Text winMenuTimeText;

    private Text winMenuPointsText;

    void Start()
    {
        // get main camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // slow but not a huge deal
        pauseMenu = GameObject.Find("PauseMenu");
        failedMenu = GameObject.Find("LoseMenu");
        winMenu = GameObject.Find("WinMenu");

        winMenuTimeText =
            GameObject.Find("WinTimeReadout").GetComponent<Text>();
        winMenuParText = GameObject.Find("WinParReadout").GetComponent<Text>();
        winMenuPointsText =
            GameObject.Find("WinPointsReadout").GetComponent<Text>();

        timer = GameObject.Find("Timer").GetComponent<Text>();
        timerFG = GameObject.Find("TimerFG").GetComponent<Text>();

        pauseMenu.SetActive(false);
        failedMenu.SetActive(false);
        winMenu.SetActive(false);

        // get the game controller component from this object
        gameController = mainCamera.GetComponent<GameController>();
    }

    public void setTimerValue(float timeStamp)
    {
        // convert value to a string with format 00:00.00
        string timeString =
            string
                .Format("{0:00}:{1:00}.{2:00}",
                Mathf.Floor(timeStamp / 60),
                Mathf.Floor(timeStamp) % 60,
                Mathf.Floor((timeStamp * 100) % 100));

        // set the value of the slider
        timer.text = timeString;
        timerFG.text = timeString;
    }

    // called by the hamburger in the top left & the x in the menu
    public void togglePause()
    {
        // pause the game
        gameController.togglePause();

        // toggle the pause menu
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void levelFailed()
    {
        // show the failed menu
        failedMenu.SetActive(true);
        gameController.levelFailed();
    }

    public void levelWon(float timeStamp, float timePar)
    {
        // show the win menu
        winMenu.SetActive(true);

        winMenuTimeText.text =
            string
                .Format("{0:00}:{1:00}.{2:00}",
                Mathf.Floor(timeStamp / 60),
                Mathf.Floor(timeStamp) % 60,
                Mathf.Floor((timeStamp * 100) % 100));

        winMenuParText.text =
            string
                .Format("{0:00}:{1:00}.{2:00}",
                Mathf.Floor(timePar / 60),
                Mathf.Floor(timePar) % 60,
                Mathf.Floor((timePar * 100) % 100));

        // calc score from delta
        float score = Mathf.Floor((timePar - timeStamp) * 10000);
        winMenuPointsText.text = string.Format("{0}", score);

        gameController.levelWon();
    }

    public void nextLevel()
    {
        // load the next level
        gameController.nextLevel();
    }

    public void restartLevel()
    {
        // reload the current level
        gameController.restartLevel();
    }

    public void quitToMenu()
    {
        // reload the menu
        gameController.quitToMenu();
    }
}
