using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float movementForce;
    void FixedUpdate()
    {
        rb.AddForce(0, 0, -movementForce * Time.fixedDeltaTime);
    }
}
