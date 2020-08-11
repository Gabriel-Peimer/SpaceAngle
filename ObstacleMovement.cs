using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public Rigidbody obstacle;
    public float movementForce;
    private Missile missileScript;

    //for missile script
    public Transform targetTransform;
    public GameObject targetObject;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        missileScript = player.GetComponent<Missile>();
    }
    void FixedUpdate()
    {
        obstacle.AddForce(0, 0, -movementForce * Time.fixedDeltaTime);
    }
    private void OnMouseDown()
    {
        missileScript.targetTransform = gameObject.transform;
        missileScript.targetObject = gameObject;

        targetObject = gameObject;
    }
}
