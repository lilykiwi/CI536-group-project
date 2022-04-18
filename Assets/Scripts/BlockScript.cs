using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() //Checks if mouse is over the object
    {
        if (Input.GetMouseButtonDown(0)) //Checks if mouse button 1 is pressed
        {
            //Code could be put here to spawn another object for mud or whatever

            UnityEngine.Object p__n = this.gameObject;
            Destroy(p__n); //Destroys the square
        }
    }
}
