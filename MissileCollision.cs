using System.Security.Cryptography;
using UnityEngine;

public class MissileCollision : MonoBehaviour
{
    private Missile shootMissileScript;//so that we can destroy the missile on impact
    private GameObject player; // to access the missile script from the player
    
    //for exploding the astroid
    private float searchRadius = 2f;
    public GameObject explosionEffect;
    public float explosionForce = 300f;
    public GameObject crackedAstroidPrefab;
    private Transform ObstacleTransform;
    private GameObject crackedAstroidObject;
    private float crackedAstroidStartTime;//to see in how long we need to destroy remnents
    private float timeForCrackedAstroidDestruction = 3f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootMissileScript = player.GetComponent<Missile>();
    }
    private void Update()
    {
        if (shootMissileScript.missileObject.transform.position.y >= 4f || 
            shootMissileScript.missileObject.transform.position.y <= -2.5f ||
            shootMissileScript.missileObject.transform.position.x >= 13f ||
            shootMissileScript.missileObject.transform.position.x <= -13f ||
            shootMissileScript.missileObject.transform.position.z >= 7.5f ||
            shootMissileScript.missileObject.transform.position.z <= -7f)
        {
            Destroy(shootMissileScript.missileObject.gameObject);
        }
        if (crackedAstroidStartTime + timeForCrackedAstroidDestruction < Time.timeSinceLevelLoad)
        {
            Destroy(crackedAstroidObject.gameObject);
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Clone")//missile has hit an astroid
        {
            ObstacleTransform = collision.collider.transform;//saving for use in ChangeAstroid function

            //destroying the object that we collided with rather than the target
            //because that we may accidently collide with a different obstacle
            ChangeAstroid();
            Destroy(collision.collider.gameObject);
            
            ExplodeAstroid();
            Destroy(shootMissileScript.missileObject.gameObject);
        }
    }
    private void ChangeAstroid()//changes the astroid with the cracked prefab
    {
        crackedAstroidObject = Instantiate(crackedAstroidPrefab, ObstacleTransform.position,
            ObstacleTransform.rotation);
        Debug.Log(crackedAstroidObject);
        crackedAstroidStartTime = Time.timeSinceLevelLoad;
    }
    private void ExplodeAstroid()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, searchRadius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, searchRadius);
            }
        }
    }
}
