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
        if (backgroundMusic.Length == 1)
        {
            music = this.GetComponent<AudioSource>();
            music.Play();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
