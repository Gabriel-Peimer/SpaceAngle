using System.Security.Cryptography;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject crackedShipPrefab;
    public GameObject player;
    public Rigidbody playerRigidbody;
    public GameManager gameManager;

    public GameObject explosionEffect;

    public float explosionForce = 300f;
    public float searchRadius = 2f;

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Obstacle" ||
            collision.collider.tag == "Clone")
        {
            ChangeShip();
            Explode();

            gameManager.GameOver();
            gameManager.PlayerLost();
        }
    }
    private void ChangeShip()
    {
        float xRotation = player.transform.rotation.eulerAngles.x +
            crackedShipPrefab.transform.rotation.eulerAngles.x;
        float yRotation = player.transform.rotation.eulerAngles.y +
            crackedShipPrefab.transform.rotation.eulerAngles.y;
        float zRotation = crackedShipPrefab.transform.rotation.eulerAngles.z;
        
        Instantiate(crackedShipPrefab, playerRigidbody.transform.position,
            Quaternion.Euler(xRotation, yRotation, zRotation));

        player.SetActive(false);
    }
    private void Explode()
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
