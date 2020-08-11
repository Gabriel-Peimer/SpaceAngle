using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;//for loading data


    public float sideForce = 500f;
    public Rigidbody player;
    public ObstacleMovement obstacleMovement;
    public TimeManager timeManager;

    private Vector3 firstPosition;
    private Vector3 lastPosition;
    private Vector3 rotation;

    private float dragDistance;
    public float deltaX;
    private float deltaY;
    public bool isRightTurn;
    public bool isLeftTurn;
    public bool hasTouchEnded = true;

    private float velocityForSwipe;
    //time
    private float startTime;
    private float endTime;
    private float swipeTime;

    private void Start()
    {
        dragDistance = Screen.height * 5 / 100;
        gameManager.LoadProgress();
    }
    void FixedUpdate()
    {
        if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            player.AddForce(sideForce * Time.fixedDeltaTime, 0, 0);
            if (transform.rotation.eulerAngles.y < 38 || transform.rotation.eulerAngles.y > 319)// || transform.rotation.z > 90)
            {
                rotation.y = -100f;
                transform.Rotate(rotation * Time.fixedDeltaTime);
            }
        }
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            player.AddForce(-sideForce * Time.fixedDeltaTime, 0, 0);
            if (transform.rotation.eulerAngles.y > 322 || transform.rotation.eulerAngles.y < 41)//|| rb.rotation.z < 90)
            {
                rotation.y = 100f;
                transform.Rotate(rotation * Time.fixedDeltaTime);
            }
        }
        if (Input.GetKey("m"))
        {
            timeManager.DoSlowmotion();
        }
        

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isRightTurn = false;
                isLeftTurn = false;
                //Starts the slowmotion
                timeManager.DoSlowmotion();

                hasTouchEnded = false;

                //updated
                startTime = Time.time;//saving to find the time for the swipe
                
                firstPosition = touch.position;
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //updated
                endTime = Time.time;//to see the time that the swipe takes
                swipeTime = endTime - startTime;//finding the length in time of the swipe

                //saves the last position for the last time
                lastPosition = touch.position;
                //calculating deltas for later use
                deltaX = (lastPosition.x - firstPosition.x);
                deltaY = (lastPosition.y - firstPosition.y);

                if (firstPosition.x < lastPosition.x &&         //it's a right swipe
                    player.transform.position.x < lastPosition.x)//the player didn't reach the end of the swipe yet
                {
                    velocityForSwipe = deltaX / swipeTime; // Velocity = Distance / Time
                    player.AddForce(velocityForSwipe * Time.fixedDeltaTime, 0, 0);
                }
                if (firstPosition.x > lastPosition.x &&         //it's a left swipe
                    player.transform.position.x > lastPosition.x)//the player didn't reach the end of the swipe yet
                {
                    velocityForSwipe = deltaX / swipeTime; // Velocity = Distance / Time
                    //we add -velocityForSwipe in the next line because that otherwise it would go right
                    player.AddForce(-velocityForSwipe * Time.fixedDeltaTime, 0, 0);
                }
                else
                {
                    player.isKinematic = true;
                    player.isKinematic = false;
                }
                //no longer in updated zone//////////////////////////////////
                /*
                hasTouchEnded = true;

                    if (firstPosition.x < lastPosition.x && // It's a right turn (DeltaX is positive)
                    Mathf.Abs(lastPosition.x) - Mathf.Abs(firstPosition.x) < deltaX)//The player didn't get to the destination yet
                {
                        isLeftTurn = false;    
                        isRightTurn = true;                        
                        player.AddForce(sideForce * Time.fixedDeltaTime, 0, 0);
                    }
                    else 
                    if (firstPosition.x > lastPosition.x && // It's a left trurn
                    Mathf.Abs(firstPosition.x) - Mathf.Abs(lastPosition.x) < deltaX)//The player didn't get to the destination yet
                    {
                        isRightTurn = false;
                        isLeftTurn = true;                        
                        player.AddForce(-sideForce * Time.fixedDeltaTime, 0, 0);
                    }
                    else//The player has arrived, turning player kinematic in order to loose all force
                    {
                        deltaX = 0;
                        player.isKinematic = true;
                        player.isKinematic = false;

                        isRightTurn = false;
                        isLeftTurn = false;
                    }*/


                    //was already commented:

                    /*
                    if (curveY > 0 && movement.x > 100)
                    {
                        obstacleMovement.movementForce += curveY / 10;
                    }
                    else
                    {
                        obstacleMovement.movementForce = 500;
                    }*/
                /*}
                else//It's a tap
                {
                    //Check if tapping on an astroid
                    //if true- check if astroid is small enough
                    //if true- explode astroid
                }*/
            }
        }
    }
}
