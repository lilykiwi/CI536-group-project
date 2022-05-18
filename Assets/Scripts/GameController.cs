using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject[] blocks;

    private GameObject[] muds;

    private GameObject clickIndicator;

    public GameObject clickIndicatorPrefab;

    public Sprite canDigSprite;

    public Sprite cantDigSprite;

    private Scene scene;

    private UIController uiController;

    // integer to keep track of water needed to collect
    public int waterNeeded = 50;

    public int waterCollected = 0;

    // get slider for water needed
    private Slider waterNeededSlider;

    // integer to keep track of lives
    public int maxiumumLives = 10;

    public int lives;

    private float levelStartTime;

    // get slider for maximum lives
    private Slider maximumLivesSlider;

    public enum GameState
    {
        paused,
        playing,
        won,
        lost
    }

    public GameState gameState = GameState.paused;

    void Awake()
    {
        // instantiate the click indicator
        clickIndicator =
            Instantiate(clickIndicatorPrefab,
            Vector3.zero,
            Quaternion.identity);

        // get every block in the scene
        blocks = GameObject.FindGameObjectsWithTag("Block");

        // get the water slider in the ui canvas
        waterNeededSlider =
            GameObject
                .FindGameObjectWithTag("WaterSlider")
                .GetComponent<Slider>();

        // get the lives slider in the ui canvas
        maximumLivesSlider =
            GameObject
                .FindGameObjectWithTag("LivesSlider")
                .GetComponent<Slider>();

        // get ui controller
        uiController = GameObject.Find("Canvas").GetComponent<UIController>();

        lives = maxiumumLives;
        levelStartTime = Time.time;

        // set state to playing
        setGameState(GameState.playing);
    }

    public void levelWon()
    {
        setGameState(GameState.won);
    }

    public void levelFailed()
    {
        setGameState(GameState.lost);
    }

    public void togglePause()
    {
        if (gameState == GameState.playing)
        {
            setGameState(GameState.paused);
        }
        else if (gameState == GameState.paused)
        {
            setGameState(GameState.playing);
        }
    }

    public bool isPaused()
    {
        if (gameState == GameState.playing)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void restartLevel()
    {
        // reload the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        levelStartTime = Time.time;
    }

    public void nextLevel()
    {

        int levelNumber = SceneManager.GetActiveScene().buildIndex + 1;

        if (levelNumber == 10) {
            // game is finished! gg
            SceneManager.LoadScene("Menu");
        } else {
            // load the next level
            SceneManager.LoadScene("Level " + levelNumber);
        }
    }

    public void quitToMenu()
    {
        // load the menu
        SceneManager.LoadScene("Menu");
    }

    void setGameState(GameState state)
    {
        switch (state)
        {
            case GameState.paused:
                gameState = GameState.paused;
                Time.timeScale = 0;
                break;
            case GameState.playing:
                gameState = GameState.playing;
                Time.timeScale = 1;
                break;
            case GameState.won:
                gameState = GameState.won;
                Time.timeScale = 0;
                break;
            case GameState.lost:
                gameState = GameState.lost;
                Time.timeScale = 0;
                break;
        }
    }

    // method for converting mouse position to grid position
    Vector3 posToGrid(Vector3 Position)
    {
        return new Vector3(Mathf.Round(Position.x),
            Mathf.Round(Position.y - 0.5f) + 0.5f,
            0);
    }

    void PositionClickIndicator()
    {

        if (isPaused())
        {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // check if the mouse is over a block
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // set the click indicator to the mouse position
        clickIndicator.transform.position =
            posToGrid(mousePos) + new Vector3(0, 0, -5);

        // check hit for a block
        if (hit.collider != null)
        {
            // check if the mouse is over a mud or block
            if (hit.collider.tag == "Mud" || hit.collider.tag == "Block")
            {
                // set the sprite to can dig
                clickIndicator.GetComponent<SpriteRenderer>().sprite =
                    canDigSprite;
            }
            else
            {
                // set the sprite to cant dig
                clickIndicator.GetComponent<SpriteRenderer>().sprite =
                    cantDigSprite;
            }
        }
        else
        {
            // set the sprite to cant dig
            clickIndicator.GetComponent<SpriteRenderer>().sprite =
                cantDigSprite;
        }
    }

    public void collectWater(string isMuddy)
    {
        if (isMuddy == "Clean")
        {
            waterCollected += 1;
            waterNeededSlider.value =
                (waterNeeded - waterCollected * 1f) / waterNeeded;
        }
        else
        {
            lives -= 1;
            maximumLivesSlider.value = (lives * 1f) / maxiumumLives;
        }

        // update the slider value using the water collected and the water needed value
        if (waterCollected >= waterNeeded && lives > 0)
        {
            uiController.levelWon(Time.time - levelStartTime, getParScore());
        }
        else if (lives <= 0)
        {
            uiController.levelFailed();
        }
    }

    float getParScore()
    {
        // get level number
        int levelNumber = SceneManager.GetActiveScene().buildIndex;

        // debug log level number
        Debug.Log("Level Number: " + levelNumber);

        switch (levelNumber)
        {
            case 1:
                return 5f;
            case 2:
                return 5f;
            case 3:
                return 8f;
            case 4:
                return 10f;
            case 5:
                return 8f;
            case 6:
                return 10f;
            case 7:
                return 10f;
            case 8:
                return 6f;
            case 9:
                return 3f;
            case 10:
                return 15f;
            default:
                return -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PositionClickIndicator();

        // update ui timer
        uiController.setTimerValue(Time.time - levelStartTime);
    }
}
