using System.Security.Cryptography;
using UnityEngine;

public class Collision : MonoBehaviour
{
    //constant scripts
    public GameManager gameManager;
    private CameraShake cameraShake;
    private AudioManager audioManager;

    //player
    public GameObject player;

    //prefabs
    public GameObject crackedAstroidPrefab;//for ChangeAstroid
    public GameObject crackedShipPrefab;

    //for explosion
    public GameObject explosionEffect;
    public float explosionForce = 300f;
    public float searchRadius = 2f;
    private float explosionForceForAstroid = 50;

    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Obstacle" ||
            collision.collider.tag == "Clone")
        {
            cameraShake.shouldShake = true;//shaking the camera
            ChangeShip();//swapes ship with cracked mesh
            Explode();//explodes ship
            ChangeAstroid(collision.collider.gameObject);//swapes astroid with cracked mesh
            ExplodeAstroid();//explodes astroid

            gameManager.GameOver();
        }
        else if (collision.collider.tag == "WallClone")
        {
            cameraShake.shouldShake = true;//shaking the camera

            ChangeShip();//swapes ship with cracked mesh
            Explode();//explodes ship

            gameManager.GameOver();
        }
    }
    private void ChangeShip()
    {
        float xRotation = player.transform.rotation.eulerAngles.x +
            crackedShipPrefab.transform.rotation.eulerAngles.x;
        float yRotation = player.transform.rotation.eulerAngles.y +
            crackedShipPrefab.transform.rotation.eulerAngles.y;
        float zRotation = crackedShipPrefab.transform.rotation.eulerAngles.z;
        
        Instantiate(crackedShipPrefab, player.transform.position,
            Quaternion.Euler(xRotation, yRotation, zRotation));

        player.SetActive(false);
    }
    private void Explode()
    {
        audioManager.PlayAudio("Explosion");//playing explosion sound effect

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
    private void ExplodeAstroid()
    {
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, searchRadius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForceForAstroid, transform.position, searchRadius);
            }
        }
    }
    private void ChangeAstroid(GameObject collider)
    {
        Instantiate(crackedAstroidPrefab, collider.transform.position, collider.transform.rotation);
        Destroy(collider);
    }
}
