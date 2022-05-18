using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite mudSprite;

    public Sprite brokenSprite;

    private Collider2D objectCollider;

    private CameraController cameraController;

    private AudioSource blockAudio;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        blockAudio = this.gameObject.GetComponent<AudioSource>();
        cameraController = Camera.main.GetComponent<CameraController>();
        gameController = Camera.main.GetComponent<GameController>();
    }

    void OnMouseOver() //Checks if mouse is over the object
    {
        //Checks if mouse button 1 is (un)pressed
        if (
            Input.GetMouseButtonUp(0) &&
            cameraController.isMoving == false &&
            gameController.isPaused() == false
        )
        {
            blockAudio.Play();

            //Code could be put here to spawn another object for mud or whatever
            if (this.gameObject.CompareTag("Mud"))
            {
                spriteRenderer.sprite = mudSprite;
                this.gameObject.tag = "MudActive";
                transform.Translate(0, 0, 5);
                this.gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
            else if (this.gameObject.CompareTag("Block"))
            {
                spriteRenderer.sprite = brokenSprite;
                transform.Translate(0, 0, 5);
                this.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    //Changing fresh water to muddy water
    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.CompareTag("MudActive"))
        {
            if (other.gameObject.CompareTag("Water"))
            {
                other.gameObject.tag = "MuddyWater";
                other.gameObject.layer = 9;
            }
        }
        if (this.gameObject.CompareTag("Filter"))
        {
            if (other.gameObject.CompareTag("MuddyWater"))
            {
                other.gameObject.tag = "Water";
                other.gameObject.layer = 8;
            }
        }
    }
}
