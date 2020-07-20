using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Missile : MonoBehaviour
{
    //May want to change with upgrades
    public int missileStrength = 0;
    private float radius = 5f;
    public float speed;
    public float rotateSpeed = 200f;
    
    //Constants for the script
    //missile
    public GameObject missile;
    public GameObject missilePrefab;
    private Rigidbody missileRigidbody;
    //target-astroid
    private Transform targetTransform;
    public GameObject targetObject;
    //time
    public float timeBetweenSpawns = 2f;
    public float timeToSpawn = 1f;


    private void Start()
    {
        FindTarget();
        ShootMissile();
    }
    private void FixedUpdate()
    {
        if (missile != null && targetObject != null)
        {
            Vector3 direction = targetTransform.position - missile.transform.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, missile.transform.forward).y;

            missileRigidbody.angularVelocity = new Vector3(0, -rotateAmount * rotateSpeed, 0);

            missileRigidbody.velocity = missile.transform.forward * speed;
        }
        else if (missile == null)
        {
            if (Time.timeSinceLevelLoad >= timeToSpawn && missile == null)
            {
                FindTarget();
                ShootMissile();
                timeToSpawn += timeBetweenSpawns;
            }
        }
        else if (targetObject == null)
        {
            FindTarget();
        }
    }
    public void ShootMissile()
    {
        Vector3 playerTransformForMissile = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y, gameObject.transform.position.z + 3f);

        missile = Instantiate(missilePrefab, playerTransformForMissile, Quaternion.identity);
        missileRigidbody = missile.GetComponent<Rigidbody>();

        if (targetObject == null)
        {
            Vector2 direction1 = (Vector2)gameObject.transform.position - (Vector2)missileRigidbody.position;

            direction1.Normalize();

            float rotateAmount = Vector3.Cross(direction1, missile.transform.forward).y;

            missileRigidbody.angularVelocity = new Vector3(0, rotateAmount * rotateSpeed, 0);

            missileRigidbody.velocity = missile.transform.forward * speed;
        }
    }
    public void FindTarget()
    {
        GameObject[] astroidsToPickFrom = GameObject.FindGameObjectsWithTag("Clone");
        if (astroidsToPickFrom.Length > 0)
        {
            int targetRandom = UnityEngine.Random.Range(0, astroidsToPickFrom.Length);

            targetTransform = astroidsToPickFrom[targetRandom].transform;
            targetObject = astroidsToPickFrom[targetRandom];
        }
    }
}
