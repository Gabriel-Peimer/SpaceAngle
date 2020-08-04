using UnityEngine;

public class ShootMissile : MonoBehaviour
{
    //Constants for the script
    //missile
    public GameObject missile;//public so that it can be accessed by Missile script
    public GameObject missilePrefab;
    public Vector3 offsetForMissileShot;
    private Transform playerTransform;
    
    //time
    //public float timeBetweenSpawns = 2f;
    //public float timeToSpawn = 1f;


    private void Start()
    {
        playerTransform = gameObject.transform;
    }
    public void Shoot()
    {
        missile = Instantiate(missilePrefab, playerTransform.position + offsetForMissileShot,
            Quaternion.identity);
        
        //storing the missiles rigidbody for later use
        /*missileRigidbody = missile.GetComponent<Rigidbody>();
         *
        if (targetObject == null)
        {
            Vector3 direction = gameObject.transform.position - missileRigidbody.position;
            
            direction.Normalize();//normalizes so that we can cross the two vectors

            float rotateAmount = Vector3.Cross(direction, missile.transform.forward).y;

            missileRigidbody.angularVelocity = new Vector3(0, rotateAmount * rotateSpeed, 0);

            missileRigidbody.velocity = missile.transform.forward * speed;
        }
        else
        {
            
        }*/
    }
}