using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float sideForce = 500f;
    public Rigidbody rb;
    public ObstacleMovement obstacleMovement;
    public TimeManager timeManager;

    private Vector3 firstPosition;
    private Vector3 lastPosition;
    private Vector3 movement;
    private Vector3 rotation;

    private float dragDistance;
    public float deltaX;
    private float deltaY;

    public bool isRightTurn;
    public bool isLeftTurn;
    public bool hasTouchEnded = true;

    private void Start()
    {
        dragDistance = Screen.height * 5 / 100;
    }
    void FixedUpdate()
    {
        if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            rb.AddForce(sideForce * Time.fixedDeltaTime, 0, 0);
            if (transform.rotation.eulerAngles.y < 38 || transform.rotation.eulerAngles.y > 319)// || transform.rotation.z > 90)
            {
                rotation.y = -100f;
                transform.Rotate(rotation * Time.fixedDeltaTime);
            }
        }
        if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            rb.AddForce(-sideForce * Time.fixedDeltaTime, 0, 0);
            if (transform.rotation.eulerAngles.y > 322 || transform.rotation.eulerAngles.y < 41)//|| rb.rotation.z < 90)
            {
                rotation.y = 100f;
                transform.Rotate(rotation * Time.fixedDeltaTime);
            }
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

                firstPosition = touch.position;
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                hasTouchEnded = true;

                //saves the last position for the last time
                lastPosition = touch.position;

                //if (Mathf.Abs(lastPosition.x) - Mathf.Abs(firstPosition.x) >= dragDistance)//It's a swipe
                //{

                    deltaX = (lastPosition.x - firstPosition.x);
                    deltaY = (lastPosition.y - firstPosition.y);

                    //movement.x = deltaX;//Making the Vector3(movement).x = DeltaX

                    if (firstPosition.x < lastPosition.x && // It's a right turn (DeltaX is positive)
                    Mathf.Abs(lastPosition.x) - Mathf.Abs(firstPosition.x) < deltaX)//The player didn't get to the destination yet
                {
                        isLeftTurn = false;    
                        isRightTurn = true;                        
                        rb.AddForce(sideForce * Time.fixedDeltaTime, 0, 0);
                    }
                    else 
                    if (firstPosition.x > lastPosition.x && // It's a left trurn
                    Mathf.Abs(firstPosition.x) - Mathf.Abs(lastPosition.x) < deltaX)//The player didn't get to the destination yet
                    {
                        isRightTurn = false;
                        isLeftTurn = true;                        
                        rb.AddForce(-sideForce * Time.fixedDeltaTime, 0, 0);
                    }
                    else//The player has arrived, turning player kinematic in order to loose all force
                    {
                        deltaX = 0;
                        rb.isKinematic = true;
                        rb.isKinematic = false;

                        isRightTurn = false;
                        isLeftTurn = false;
                    }
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
