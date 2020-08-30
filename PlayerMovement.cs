using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody player;
    public ObstacleMovement obstacleMovement;
    public TimeManager timeManager;//for slow-motion

    //for computer movement
    private Vector3 rotation;
    public float sideForceComputer = 500f;

    //deltas
    public float deltaX;
    private float deltaY;
    private float slope;

    private float dragDistance;//other... for later... maybe...

    //positions of mobile input
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    //for rotation and velocity (mobile)
    public float sideForceMobile = 50f;
    private float rotationSpeed = 5f;
    private double angleToRotate;
    private float maxRotation = 45f;
    private float velocityForSwipe;

    //time
    private float startTime;
    private float endTime;
    private float swipeTime;

    private void Start()
    {
        dragDistance = Screen.height * 5 / 100;
    }
    void FixedUpdate()
    {
        //Computer movement for testing etc.
        if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            player.AddForce(sideForceComputer * Time.fixedDeltaTime, 0, 0);

            if (transform.rotation.eulerAngles.y < 38 || transform.rotation.eulerAngles.y > 319)// || transform.rotation.z > 90)
            {
                rotation.y = -100f;
                transform.Rotate(rotation * Time.fixedDeltaTime);
            }
        }
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            player.AddForce(-sideForceComputer * Time.fixedDeltaTime, 0, 0);

            if (transform.rotation.eulerAngles.y > 322 || transform.rotation.eulerAngles.y < 41)//|| rb.rotation.z < 90)
            {
                rotation.y = 100f;
                transform.Rotate(rotation * Time.fixedDeltaTime);
            }
        }
        if (Input.GetKey("w"))
        {
            timeManager.DoSlowmotion();
        }
        //end of computer movement
        
        //touch input for mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //resetting the speed
                player.velocity = Vector3.zero;
                //timeManager.DoSlowmotion();//starts slow-motion

                startTime = Time.timeSinceLevelLoad;//saving to find the length (in time) of the swipe

                firstPosition = touch.position;
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //timeManager.shouldSlowMmotionStop = true;

                endTime = Time.timeSinceLevelLoad;//to see the time that the swipe takes
                swipeTime = endTime - startTime;//finding the length in time of the swipe

                //saves the last position for the last time
                lastPosition = touch.position;

                //calculating deltas for later use
                deltaX = (lastPosition.x - firstPosition.x);
                deltaY = (lastPosition.y - firstPosition.y);

                //calculating the slope
                slope = deltaY / deltaX;
                
                if (deltaY < 0)
                {
                    return;
                }
                //simple constraints
                slope = Mathf.Clamp(slope, -5f, 5f);
            }
        }
        if (Math.Abs(deltaY) > dragDistance || Math.Abs(deltaX) > dragDistance)//only if it's not a tap
        {
            //moving the ship
            MoveShip();
            //rotating the ship
            RotateShip();
        }
    }
    private void RotateShip()
    {
        //calculating angleToRotate (converting Atan result from radians to degrees)
        angleToRotate = (90 - ((180 / Math.PI) * Math.Atan(Math.Abs(deltaY) / Math.Abs(deltaX))));

        if (angleToRotate > maxRotation) { angleToRotate = maxRotation; }//clamping to value

        if (player != null)//when the player explodes an error will appear unless we check for the player
        {
            if (deltaX > 0 && player.transform.rotation.eulerAngles.y < angleToRotate ||
                deltaX > 0 && player.transform.rotation.eulerAngles.y > 361 - maxRotation)
            {
                player.transform.Rotate(0, -rotationSpeed, 0);
            }
            if (deltaX < 0 && player.transform.rotation.eulerAngles.y < maxRotation + 1 ||
                deltaX < 0 && player.transform.rotation.eulerAngles.y > 360 - angleToRotate)
            {
                player.transform.Rotate(0, rotationSpeed, 0);
            }
        }
    }
    private void MoveShip()
    {
        CalculateVelocity();//calculating before rotating

        if (deltaX > 0)//it's a right swipe
        {
            player.AddForce(velocityForSwipe * Time.fixedDeltaTime, 0, 0);
        }
        if (deltaX < 0)//it's a left swipe
        {
            //we don't add -sideForce in the next line because 
            //that if we swipe left the velocityForSwipe is negative
            player.AddForce(velocityForSwipe * Time.fixedDeltaTime, 0, 0);
        }
    }
    private void CalculateVelocity()
    {
        velocityForSwipe = sideForceMobile * sideForceMobile / slope;
        Debug.Log(slope + " S");
        //clamping the value so that the player can't go too quickly
        velocityForSwipe = Mathf.Clamp(velocityForSwipe,
            -sideForceMobile *sideForceMobile, sideForceMobile * sideForceMobile);

        Debug.Log(velocityForSwipe + " V");
    }
}
