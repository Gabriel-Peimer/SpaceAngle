﻿using System.Security.Cryptography;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject crackedShipPrefab;
    public GameObject player;
    public Rigidbody playerRigidbody;
    public GameManager gameManager;

    public GameObject explosionEffect;

    public float explosionForce = 300f;
    public float radius = 2f;

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Obstacle" ||
            collision.collider.tag == "Clone" ||
            collision.collider.tag == "WallClone")
        {
            ChangeShip();
            Explode();

            gameManager.GameOver();
            gameManager.PlayerLost();
        }
    }
    void ChangeShip()
    {
        float xRotation = player.transform.rotation.eulerAngles.x + crackedShipPrefab.transform.rotation.eulerAngles.x;
        float yRotation = player.transform.rotation.eulerAngles.y + crackedShipPrefab.transform.rotation.eulerAngles.y;
        float zRotation = crackedShipPrefab.transform.rotation.eulerAngles.z;
        Instantiate(crackedShipPrefab, playerRigidbody.transform.position, Quaternion.Euler(xRotation, yRotation, zRotation));
        player.SetActive(false);
    }
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
    }
}