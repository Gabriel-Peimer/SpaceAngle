using UnityEngine;

public class MissileCollision : MonoBehaviour
{
    public GameObject target;
    public Missile missileScript;

    private void Update()
    {
        target = missileScript.targetObject;
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Clone")
        {
            Destroy(gameObject);
            Destroy(target);
        }
    }
}
