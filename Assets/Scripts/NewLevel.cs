using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewLevel : MonoBehaviour
{

    private GameObject sceneCamera;
    private GameController gameController;

    void Start() {
        // get scene camera
        Camera sceneCamera = Camera.main;
        // get scenecontroller from scene camera
        gameController = sceneCamera.GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // we have collected a water, so call the function on the game controller
        gameController.collectWater();
        // delete the particle
        Destroy(other.gameObject);
    }
}
