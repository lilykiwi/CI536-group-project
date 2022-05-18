using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject[] blocks;
    private GameObject[] muds;
    public GameObject blockPrefab;
    private GameObject clickIndicator;
    public GameObject clickIndicatorPrefab;
    public Sprite canDigSprite;
    public Sprite cantDigSprite;
    private Scene scene;

    // integer to keep track of water needed to collect
    public int waterNeeded = 50;
    public int waterCollected = 0;

    // get slider for water needed
    private Slider waterNeededSlider;

    // integer to keep track of lives
    public int maxiumumLives = 10;
    public int hitsTaken = 0;

    // get slider for maximum lives
    private Slider maximumLivesSlider;

    // method for converting mouse position to grid position
    Vector3 posToGrid(Vector3 Position)
    {
        return new Vector3(Mathf.Round(Position.x),
            Mathf.Round(Position.y - 0.5f) + 0.5f,
            0);
    }

    void PositionClickIndicator()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // check if the mouse is over a block
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // set the click indicator to the mouse position
        clickIndicator.transform.position =
            posToGrid(mousePos) + new Vector3(0, 0, -5);

        // check hit for a block
        if (hit.collider != null)
        {
            // get every mud in the scene
            muds = GameObject.FindGameObjectsWithTag("Mud");

            // check if the block is in the set of blocks
            if ((System.Array.IndexOf(blocks, hit.collider.gameObject) != -1) || (System.Array.IndexOf(muds, hit.collider.gameObject) != -1))
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

    public void collectWater()
    {
        waterCollected += 1;

        // update the slider value using the water collected and the water needed value
        waterNeededSlider.value = (waterNeeded - waterCollected * 1f) / waterNeeded * 1f;
        if (waterCollected >= waterNeeded)
        {
            scene = SceneManager.GetActiveScene();
            if(scene.name == "Level 1"){
                SceneManager.LoadScene("Level 2");
            }
            else if (scene.name == "Level 2"){
                SceneManager.LoadScene("Level 3");
            }
        }
    }

    public void minusLives()
    {
        hitsTaken += 1;

        // update the slider value using the water collected and the water needed value
        maximumLivesSlider.value = (maxiumumLives - hitsTaken * 1f) / maxiumumLives * 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        // instantiate the click indicator
        clickIndicator =
            Instantiate(clickIndicatorPrefab,
            Vector3.zero,
            Quaternion.identity);

        // get every block in the scene
        blocks = GameObject.FindGameObjectsWithTag("Block");


        // get the water slider in the ui canvas
        waterNeededSlider = GameObject.FindGameObjectWithTag("WaterSlider").GetComponent<Slider>();

        // get the lives slider in the ui canvas
        maximumLivesSlider = GameObject.FindGameObjectWithTag("LivesSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        PositionClickIndicator();
    }

}
