using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Reference to player
    public Spaceship player;

    //Percentage of the screen that causes the camera to move
    public float xBoundary = 0.4f, yBoundary = 0.3f;

    //Speed
    public float speed = 5;

    //Max position of camera
    public float xMax = 9.8f, yMax = 4.8f;

    //Camera reference
    private Camera cam;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(player.transform.position);
            Vector3 newPosition = transform.position;

            //Left side of screen
            if (player.GetVelocity().x<0 && screenPos.x <= Screen.width * xBoundary && newPosition.x > xMax*-1)
            {
                newPosition.x += player.GetVelocity().x*Time.deltaTime;
            }//Right side of screen
            else if (player.GetVelocity().x > 0 && screenPos.x >= (Screen.width - Screen.width * xBoundary) && newPosition.x < xMax)
            {
                newPosition.x += player.GetVelocity().x * Time.deltaTime;
            }

            if (player.GetVelocity().magnitude < player.maxVelocity)
            {
                //float playerSpeedRatio = (player.GetVelocity().magnitude / player.maxVelocity);
                //cam.orthographicSize = 2 + 0.2f*(playerSpeedRatio* playerSpeedRatio);
            }

            //Final position update
            transform.position = newPosition;
        } 
    }
}
