using UnityEngine;

public class Missile : MonoBehaviour
{
    //May want to change with upgrades --- TODO ---
    
    public int missileStrength = 0; // will be used to see what size obstacle the missile can break
    public float missileSpeed;
    public float missileRotateSpeed = 200f;
    
    //time (still may be changed with upgrades)
    public float timeForPossibleShot = 1f;//the player will be able to shoot once this time passes
    public float timeBetweenPossibleShots = 2f;//the player will be able to shoot every x seconds
    private bool isShotPossible;


    //Constants for the script/ things that upgrades will not change
    //GameMaster
    private GameObject gameMasterObject;
    private GameMaster gameMaster;
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
    public GameObject explosionEffect;//if the target is destroyed

    private void Start()
    {
        gameMasterObject = GameObject.Find("GameMaster");
        gameMaster = gameMasterObject.GetComponent<GameMaster>();
    }
    private void FixedUpdate()
    {
        if (gameMaster.missileUpgradeValue > 0)
        {
            if (timeForPossibleShot < Time.timeSinceLevelLoad)
            {
                isShotPossible = true;
            }
            int missileCount = GameObject.FindGameObjectsWithTag("Missile").Length;

            if (missileObject != null && targetObject != null)
            {
                Vector3 direction = targetTransform.position - missileObject.transform.position;
                
                direction.Normalize();//so that we can cross the direction and the missile position

                float rotateAmount = Vector3.Cross(direction, missileObject.transform.forward).y;//crossing

                missileRigidbody.angularVelocity = new Vector3(0, -rotateAmount * missileRotateSpeed, 0);

                if (targetTransform.position.z < missileObject.transform.position.z)
                {
                    missileSpeed = 15;
                }
                else
                {
                    missileSpeed = 7;
                }
                missileRigidbody.velocity = missileObject.transform.forward * missileSpeed;
            }

            if (targetObject != null && missileCount == 0 && isShotPossible)//player may now shoot
            {
                ShootTheMissile();
                isShotPossible = false;
                timeForPossibleShot = Time.timeSinceLevelLoad + timeBetweenPossibleShots;
            }
            else if (targetObject != null && missileCount == 0 && isShotPossible == false)//not time to shoot yet
            {
                targetObject = null;
                targetTransform = null;
            }
            else if (targetObject == null && missileObject != null)
            {
                Instantiate(explosionEffect, missileObject.transform.position, Quaternion.identity);
                Destroy(missileObject.gameObject);
            }
        }
    }
    void ShootTheMissile()
    {
        missileObject = Instantiate(missilePrefab, playerTransform.position + offsetForMissileShot,
            Quaternion.identity);
        missileRigidbody = missileObject.GetComponent<Rigidbody>();
    }
}
