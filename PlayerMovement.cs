using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody player;
    public ObstacleMovement obstacleMovement;
    public TimeManager timeManager;//for slow-motion

    //for computer movement
    private Vector3 rotation;
    public float sideForceComputer = 700f;

    //deltas
    public float deltaX;
    private float deltaY;
    private float slope;

    private float dragDistance;//for swipe check (check that the player didn't just tap)

    //positions of mobile input
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    //for rotation and velocity (mobile)
    public float sideForceMobile = 20f;
    private float rotationSpeed = 3f;
    private double angleToRotate;
    private float maxRotation = 45f;
    private float velocityForSwipe;

    //time
    private float startTime;
    private float endTime;

    private void Start()
    {
        dragDistance = Screen.height * 5 / 100;
    }
    void FixedUpdate()
    {
        if (GameManager.gameHasEnded != true)
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
        }
        //end of computer movement

        //touch input for mobile
        if (GameManager.gameHasEnded != true)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    timeManager.DoSlowmotion();//starts slow-motion

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
                    timeManager.shouldSlowMmotionStop = true;

                    endTime = Time.timeSinceLevelLoad;//to see the time that the swipe takes

                    //saves the last position for the last time
                    lastPosition = touch.position;

                    //calculating deltas for later use
                    deltaX = (lastPosition.x - firstPosition.x);
                    deltaY = Math.Abs(lastPosition.y - firstPosition.y);

                    //calculating the slope
                    slope = deltaY / deltaX;

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
    }
    private void RotateShip()
    {
        //calculating angleToRotate (converting Atan result from radians to degrees)
        angleToRotate = (90 - ((180 / Math.PI) * Math.Atan(Math.Abs(deltaY) / Math.Abs(deltaX))));

        if (angleToRotate > maxRotation) { angleToRotate = maxRotation; }//clamping to value

        if (player != null)//when the player explodes an error will appear unless we check for the player
        {
            if (deltaX > 0 && player.transform.rotation.eulerAngles.y < angleToRotate - 2 ||
                deltaX > 0 && player.transform.rotation.eulerAngles.y > 350 - maxRotation)
            {
                player.transform.Rotate(0, -rotationSpeed, 0);
            }
            if (deltaX < 0 && player.transform.rotation.eulerAngles.y < maxRotation + 10 ||
                deltaX < 0 && player.transform.rotation.eulerAngles.y > 362 - angleToRotate)
            {
                player.transform.Rotate(0, rotationSpeed, 0);
            }
        }
    }
    private void MoveShip()
    {
        CalculateVelocity();//calculating before rotating
        
        //adding the force according to CalculateVelocity method
        player.AddForce(velocityForSwipe * Time.fixedDeltaTime, 0, 0);
    }
    private void CalculateVelocity()
    {
        velocityForSwipe = sideForceMobile * sideForceMobile / slope;
        //clamping the value so that the player can't go too quickly
        velocityForSwipe = Mathf.Clamp(velocityForSwipe,
            -sideForceMobile *sideForceMobile, sideForceMobile * sideForceMobile);
    }
}
