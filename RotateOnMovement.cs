using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RotateOnMovement : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Rigidbody player;
    private Vector3 rotation;
    //private float rotationAmount = 150;
    /*
    void FixedUpdate()
    {
        if (playerMovement.isRightTurn == true && transform.rotation.eulerAngles.y < 38 ||
            playerMovement.isRightTurn == true && transform.rotation.eulerAngles.y > 319)
        {
            rotation.y = -rotationAmount;
            transform.Rotate(rotation * Time.fixedDeltaTime);
        }
        else
        if (playerMovement.isLeftTurn == true && transform.rotation.eulerAngles.y > 322 ||
            playerMovement.isLeftTurn == true && transform.rotation.eulerAngles.y < 41)
        {
            rotation.y = rotationAmount;
            transform.Rotate(rotation * Time.fixedDeltaTime);
        }



        //already commented before
        else
        if (playerMovement.isLeftTurn == false && playerMovement.isRightTurn == false && 
            transform.rotation.eulerAngles.y > 0.1)
        {
            rotation.y = rotationAmount;
            transform.Rotate(rotation * Time.fixedDeltaTime);
        }
        else
        if (playerMovement.isLeftTurn == false && playerMovement.isRightTurn == false &&
            transform.rotation.eulerAngles.y < 359.9)
        {
            rotation.y = -rotationAmount;
            transform.Rotate(rotation * Time.fixedDeltaTime);
        }



    }*/
}
