using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bucket : MonoBehaviour
{
    private GameObject sceneCamera;

    private GameController gameController;

    private AudioSource bucketAudio;

    private string otherTag;

    void Start()
    {
        // get scene camera
        Camera sceneCamera = Camera.main;

        // get scenecontroller from scene camera
        gameController = sceneCamera.GetComponent<GameController>();

        // get audio source
        bucketAudio = this.GetComponent<AudioSource>();
    }

    //Checking bucket collisions
    void OnTriggerEnter2D(Collider2D other)
    {
        bucketAudio.Play();

        if (other.gameObject.CompareTag("MuddyWater"))
        {
            // we collected muddy water, so subtract a life
            gameController.collectWater("Muddy");
            Destroy(other.gameObject);
        }
        else
        {
            // we have collected a water, so call the function on the game controller
            gameController.collectWater("Clean");

            // delete the particle
            Destroy(other.gameObject);
        }
    }
}
