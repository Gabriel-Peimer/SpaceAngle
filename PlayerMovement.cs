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

    private float dragDistance;//other... for later... maybe...

    //positions of mobile input
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    //for rotation and velocity (mobile)
    public float sideForceMobile = 200f;
    private float rotationSpeed = 5f;
    private double angleToRotate;
    private float maxRotation = 45f;

    //time
    private float startTime;
    private float endTime;
    private float swipeTime;

    private void Start()
    {
        dragDistance = Screen.height * 5 / 100;

        GameObject gameMasterObject = GameObject.Find("GameMaster");
        GameMaster gameMaster = gameMasterObject.GetComponent<GameMaster>();

        GameManager.LoadProgress(gameMaster);
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
                timeManager.DoSlowmotion();//starts slow-motion

                startTime = Time.time;//saving to find the length (in time) of the swipe
                
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

                endTime = Time.time;//to see the time that the swipe takes
                swipeTime = endTime - startTime;//finding the length in time of the swipe

                //saves the last position for the last time
                lastPosition = touch.position;

                //calculating deltas for later use
                deltaX = (lastPosition.x - firstPosition.x);//TODO -- fix the deltas
                deltaY = (lastPosition.z - firstPosition.z);
            }
        }
        //moving the ship
        if (firstPosition.x < lastPosition.x)//right swipe
        {
            player.AddForce(sideForceMobile * Time.fixedDeltaTime, 0, 0);
        }
        if (firstPosition.x > lastPosition.x)//it's a left swipe
        {
            //we add -sideForce in the next line so that the player will move right
            player.AddForce(-sideForceMobile * Time.fixedDeltaTime, 0, 0);
        }

        //rotating the ship
        Rotate();

        /*
            if (deltaX > 0 && player.transform.rotation.eulerAngles.y < angleToRotate)//right turn and still needs rotating
        {
            player.transform.Rotate(0, -rotationSpeed, 0);
        }
        //the nect code will be run only if it's a left turn and it didn't pass the angleToTurn
        if (deltaX < 0 && player.transform.rotation.eulerAngles.y > 360 - angleToRotate || 
            deltaX < 0 && player.transform.rotation.eulerAngles.y > 0)
        {
            player.transform.Rotate(0, rotationSpeed, 0);
        }*/
    }
    private void Rotate()
    {
        angleToRotate = 90 - Math.Atan(Math.Abs(deltaY) / Math.Abs(deltaX));

        if (angleToRotate > maxRotation) { angleToRotate = maxRotation; }//clamping to value

        if (player != null)//when the player explodes an error will appear unless we check for the player
        {
            if (player.transform.rotation.eulerAngles.y >= 0 &&
                player.transform.rotation.eulerAngles.y < angleToRotate ||
                player.transform.rotation.eulerAngles.y < 0 &&
                player.transform.rotation.eulerAngles.y > -angleToRotate)
            {
                if (deltaX > 0)
                {
                    player.transform.Rotate(0, -rotationSpeed, 0);
                }
                if (deltaX < 0)
                {
                    player.transform.Rotate(0, rotationSpeed, 0);
                }
            }
        }
    }
}
