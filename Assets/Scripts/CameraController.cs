using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // store mouse position
    private Vector3 mousePos;

    private Vector2 mouseDelta;

    // vector 4 to clamp min/max x, min/max y
    public Vector4 bounds;

    public bool isMoving;

    // Update is called once per frame
    void LateUpdate()
    {
        // compare current mouse position with previous mouse position
        // if mouse has moved, move camera
        if (Input.GetMouseButton(0))
        {
            mouseDelta = Input.mousePosition - mousePos;

            // if mouseDelta magnitude is greater than a threshold, move camera

            if (mouseDelta.magnitude > 5f || isMoving == true)
            {
                isMoving = true;

                // move camera
                transform
                    .Translate(-mouseDelta.x * Time.deltaTime,
                    -mouseDelta.y * Time.deltaTime,
                    0);

                // clamp camera position
                transform.position =
                    new Vector3(Mathf
                            .Clamp(transform.position.x, bounds.z, bounds.x),
                        Mathf.Clamp(transform.position.y, bounds.w, bounds.y),
                        transform.position.z);
            }
        }
        else
        {
            isMoving = false;
        }

        // get mouse position and set it to mousePOs
        mousePos = Input.mousePosition;
    }
}
