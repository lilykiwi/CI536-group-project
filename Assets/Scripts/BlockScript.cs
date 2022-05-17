using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite mudSprite;
    private Collider2D objectCollider;
    private Transform objectTransform;

    // Start is called before the first frame update
    void Start(){
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnMouseOver() //Checks if mouse is over the object
    {
        //Checks if mouse button 1 is pressed
        if (Input.GetMouseButtonDown(0)){ 
            //Code could be put here to spawn another object for mud or whatever
            if (this.gameObject.CompareTag("Mud")){
                spriteRenderer.sprite = mudSprite;
                this.gameObject.tag = "MudActive";
                objectTransform = this.gameObject.transform;
                objectTransform.position += new Vector3(0,0,5);
                objectCollider = this.gameObject.GetComponent<Collider2D>();
                objectCollider.isTrigger = true;
            }
            else if(!this.gameObject.CompareTag("MudActive")){
                Destroy(this.gameObject); //Destroys the square
            }
        }
    }
    //Changing fresh water to muddy water
    void OnTriggerEnter2D(Collider2D other){
        if (this.gameObject.CompareTag("MudActive")){
            if (other.gameObject.CompareTag("Water")){
                other.gameObject.tag = "MuddyWater";
                other.gameObject.layer = 9;
            }
        }
    }
}
