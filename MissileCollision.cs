using UnityEngine;

public class MissileCollision : MonoBehaviour
{
    private Missile shootMissileScript;//so that we can destroy the missile on impact
    private GameObject player; // to access the missile script from the player
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootMissileScript = player.GetComponent<Missile>();
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Clone")//missile has hit an astroid
        {
            Destroy(shootMissileScript.missileObject.gameObject);
            Debug.Log(shootMissileScript.missileObject);
            //destroying the object that we collided with rather than the target
            //because that we may accidently collide with a different obstacle
            Destroy(collision.collider.gameObject);
        }
    }
}
