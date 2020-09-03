using System.Security.Cryptography;
using UnityEngine;

public class MissileCollision : MonoBehaviour
{
    private Missile shootMissileScript;//so that we can destroy the missile on impact
    private GameObject player; // to access the missile script from the player
    private CameraShake cameraShake;
    private AudioManager audioManager;
    
    //for exploding the astroid
    private float searchRadius = 2f;
    public GameObject explosionEffect;
    public float explosionForce = 300f;
    //obstacle
    public GameObject crackedAstroidPrefab;
    private Transform ObstacleTransform;
    private GameObject crackedAstroidObject;
    private float timeForCrackedAstroidDestruction = 3f;//to be used for destroying cracked astroid after x time

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootMissileScript = player.GetComponent<Missile>();

        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
            
            Destroy(gameObject);//so that we don't create another explosion in the Missile script
            audioManager.StopAudio("FlyingRocket");//stopping once the missile exploded

            ExplodeAstroid();

            cameraShake.shouldShake = true;//shaking the camera
        }
    }
    public void ChangeAstroid()//changes the astroid with the cracked prefab
    {
        crackedAstroidObject = Instantiate(crackedAstroidPrefab, ObstacleTransform.position,
            ObstacleTransform.rotation);
        Destroy(crackedAstroidObject, timeForCrackedAstroidDestruction);
    }
    public void ExplodeAstroid()//public so it can be used in Missile script
    {
        audioManager.PlayAudio("Explosion");//playing explosion sound effect

        Instantiate(explosionEffect, shootMissileScript.targetTransform.position, Quaternion.identity);

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
