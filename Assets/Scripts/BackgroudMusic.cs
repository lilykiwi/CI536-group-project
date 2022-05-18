using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroudMusic : MonoBehaviour
{
    private AudioSource music;
    private GameObject[] backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        if (backgroundMusic.Length != 0){
            music = this.GetComponent<AudioSource>();
            music.Play();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
