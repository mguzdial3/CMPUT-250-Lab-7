using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    //Current Spaceship Velocity
    private Vector3 velocity, acceleration, prevPosition;

    //Acceleration and max velocity (set by students)
    public float accelerationDelta =1, maxAcceleration=5, maxVelocity = 5, inertia = 1f;

    private float xBound = 12.7f, yBound = 7f, minVelocity = 0.2f, bounceBack = 10;



    //Get velocity, useful for camera size
    public Vector3 GetVelocity()
    {
        return velocity;
    }

    // Update is called once per frame
    void Update()
    {

        bool buttonPressed = false;
        Vector3 direction = Vector3.zero;

        //Going Up
        if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){
            direction +=Vector3.up;
            buttonPressed = true;
        } 
        //Going Down
        if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){
            direction +=Vector3.down;
            buttonPressed = true;
        } 
        //Going Left
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){
            direction +=Vector3.left;
            buttonPressed = true;
        } 
        //Going Down
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){
            direction +=Vector3.right;
            buttonPressed = true;
        } 

        if(!buttonPressed){
            //Decrease velocity if not actively pressed
            if(velocity.magnitude>0){
                velocity-=velocity.normalized*inertia*Time.deltaTime;
            }

            if (velocity.magnitude<minVelocity){
                velocity = Vector3.zero;
            }

            acceleration = Vector3.zero;
        }
        else{
            acceleration+=direction*accelerationDelta*Time.deltaTime;

            if(acceleration.magnitude>maxAcceleration){
                acceleration.Normalize();
                acceleration*=maxAcceleration;
            }

            //Increase Velocity according to acceleration
            velocity+=acceleration*Time.deltaTime;
        }

        //Ensure we stay within maxVelocity
        if(velocity.magnitude>maxVelocity){
            velocity.Normalize();
            velocity*=maxVelocity;
        }

        Vector3 newPosition = transform.position + Time.deltaTime*velocity;

        //Ensure we stay within bounds
        if (newPosition.x>xBound){
            newPosition.x = xBound;
            velocity.x*=-1 * bounceBack;//Bounce back      
        }
        else if (newPosition.x <-1*xBound){
            newPosition.x = -1*xBound;
            velocity.x*=-1 * bounceBack;//Bounce back
        }

        if (newPosition.y>yBound){
            newPosition.y = yBound;
            velocity.y*=-1 * bounceBack;//Bounce back      
        }
        else if (newPosition.y <-1*yBound){
            newPosition.y = -1*yBound;
            velocity.y*=-1* bounceBack;//Bounce back
        }

        //Set position
        transform.position = newPosition;
        prevPosition = transform.position;
    }
}
