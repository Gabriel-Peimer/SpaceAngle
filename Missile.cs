using UnityEngine;

public class Missile : MonoBehaviour
{
    //May want to change with upgrades --- TODO ---
    
    public int missileStrength = 0; // will be used to see what size obstacle the missile can break
    public float missileSpeed;
    public float missileRotateSpeed = 200f;
    
    //time (still may be changed with upgrades)
    //public float timeBetweenSpawns = 2f;
    //public float timeToSpawn = 1f;
    

    //Constants for the script/ things that upgrades will not change
    //missile
    private Rigidbody missileRigidbody;
    public GameObject missilePrefab;
    public GameObject missileObject;
    public Vector3 offsetForMissileShot;//offset from the player
    public Transform playerTransform;//so that we can shoot the missile from the player location
    //target-astroid
    public Transform targetTransform;
    public GameObject targetObject;
    public ObstacleMovement obstacleMovementScript;

    private void FixedUpdate()
    {
        int missileCount = GameObject.FindGameObjectsWithTag("Missile").Length;

        if (missileObject != null && targetObject != null)
        {
            Vector3 direction = targetTransform.position - missileObject.transform.position;
            direction.Normalize();//so that we can cross the direction and the missile position

            float rotateAmount = Vector3.Cross(direction, missileObject.transform.forward).y;//crossing

            missileRigidbody.angularVelocity = new Vector3(0, -rotateAmount * missileRotateSpeed, 0);

            missileRigidbody.velocity = missileObject.transform.forward * missileSpeed;
        }
        else if (targetObject != null && missileCount == 0)
        {
            ShootTheMissile();
        }
    }

    /*private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Clone")//missile has hit an astroid
        {
            Destroy(missileObject.gameObject);
            Debug.Log(missileObject);
            //destroying the object that we collided with rather than the target
            //because that we may accidently collide with a different obstacle
            //Destroy(collision.collider);
            Destroy(collision.collider.gameObject);
        }
    }*/
    void ShootTheMissile()
    {
        missileObject = Instantiate(missilePrefab, playerTransform.position + offsetForMissileShot,
            Quaternion.identity);
        missileRigidbody = missileObject.GetComponent<Rigidbody>();
    }
}
