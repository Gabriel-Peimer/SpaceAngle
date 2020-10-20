using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody player;
    public ObstacleMovement obstacleMovement;
    public TimeManager timeManager;//for slow-motion
    //for upgrades
    private GameMaster gameMaster;
    private float[] scoreSpeedUpgrades = { 150f, 200f, 300f, 400f, 500f };

    //for computer movement
    private Vector3 rotation;
    public float sideForceComputer = 700f;

    //deltas
    public float deltaX;
    private float deltaY;
    private float slope;
    private float swipeDeltaX;
    private float swipeDeltaY;

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
    //joystick
    public Joystick joystick;
    public float sideForceMobileJoystick = 300f;
    private float rotationSpeedJoystick = 2f;

    private void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        //for checking if the touch is a swipe
        dragDistance = Screen.height * 5 / 100;
        //slope
        deltaX = 0;
        deltaY = 0;
        slope = 0;
        //to change speed
        sideForceMobileJoystick = scoreSpeedUpgrades[gameMaster.shipSpeedUpgradeValue];
    }
    void FixedUpdate()
    {
        if (GameManager.gameHasEnded != true)
        {
            CheckComputerInput();
        }
        //end of computer movement

        //touch input for mobile
        if (GameManager.gameHasEnded != true)
        {
            if (gameMaster.isJoystickActive == false)//not in joystick mode
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        timeManager.DoSlowmotion();//starts slow-motion

                        firstPosition = touch.position;
                        lastPosition = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        lastPosition = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        timeManager.shouldSlowMotionStop = true;

                        //saves the last position for the last time
                        lastPosition = touch.position;

                        //calculating deltas
                        deltaX = (lastPosition.x - firstPosition.x);
                        deltaY = Math.Abs(lastPosition.y - firstPosition.y);

                        //calculating the slope
                        slope = deltaY / deltaX;
                        //simple constraints
                        slope = Mathf.Clamp(slope, -5f, 5f);
                    }
                }
                if (Math.Abs(deltaX) > dragDistance || Math.Abs(deltaY) > dragDistance)
                {
                    //moving the ship
                    MoveShip();
                    //rotating the ship
                    RotateShip();
                }
            }
            else if (gameMaster.isJoystickActive == true)//joystick mode
            {
                //calculating the velocity
                if (joystick != null)
                {
                    velocityForSwipe = joystick.Horizontal * sideForceMobileJoystick;
                    slope = joystick.Horizontal;

                    deltaX = velocityForSwipe;//for the rotation method (not used anywhere else)

                    MoveShip();
                    RotateShip();
                }
            }
        }
    }
    private void CheckComputerInput()
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
    private void RotateShip()
    {
        if (gameMaster.isJoystickActive == false)
        {
            //calculating angleToRotate (converting Atan result from radians to degrees)
            angleToRotate = (90 - ((180 / Math.PI) * Math.Atan(slope)));

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
        else//using the joystick
        {
            angleToRotate = slope * 45;

            if (angleToRotate != 0)
            {
                float yRotation = player.transform.rotation.eulerAngles.y;

                if (yRotation <= angleToRotate && angleToRotate >= 0 ||
                    yRotation >= 360 + angleToRotate && angleToRotate <= 0 ||
                    yRotation == 0)
                {
                    player.transform.Rotate(0, -rotationSpeedJoystick * Math.Sign((decimal)slope), 0);
                }
                //the following code is for checking for the case that the player 
                //rotated a bit more than he was supposed to
                else if (angleToRotate < 0 && yRotation < maxRotation + 5)
                {
                    player.transform.Rotate(0, rotationSpeedJoystick, 0);
                }
                else if (angleToRotate > 0 && yRotation > 355 - maxRotation)
                {
                    player.transform.Rotate(0, -rotationSpeedJoystick, 0);
                }
            }
        }
    }
    private void MoveShip()
    {
        if (gameMaster.isJoystickActive == false)
        {
            CalculateVelocity();//calculating before rotating
        }
        
        //adding the force according to CalculateVelocity method
        player.AddForce(velocityForSwipe * Time.fixedDeltaTime, 0, 0);
    }
    private void CalculateVelocity()
    {
        if (gameMaster.isJoystickActive == false)
        {
            velocityForSwipe = sideForceMobile * sideForceMobile / slope;
            //clamping the value so that the player can't go too quickly
            velocityForSwipe = Mathf.Clamp(velocityForSwipe,
                -sideForceMobile * sideForceMobile, sideForceMobile * sideForceMobile);
        }
    }
}
