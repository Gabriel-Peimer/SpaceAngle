using UnityEngine;
using UnityEngine.UI;

public class Missile : MonoBehaviour
{   
    //will not be changed, info for the script to use
    public float missileSpeed;
    public float missileRotateSpeed = 200f;
    private bool isShotPossible;

    //time (will be changed with upgrades)
    private float[] timeBetweenShotsByUpgrade = { 0f, 4f, 3f, 3.5f, 2f };
    public float timeForPossibleShot = 1f;//the player will be able to shoot once this time passes
    public float timeBetweenPossibleShots = 2f;//the player will be able to shoot every x seconds

    //Constants for the script/ things that upgrades will not change
    //GameMaster
    private GameObject gameMasterObject;
    private GameMaster gameMaster;

    //missile
    private Rigidbody missileRigidbody;
    public GameObject missileObject;
    public GameObject missilePrefab;

    public Transform playerTransform;//so we can shoot the missile from the player location + offset
    public Vector3 offsetForMissileShot;//offset from the player

    //missile indicator
    public GameObject missileIndicator;
        //colors
    private Color32 missileShotIsPossibleColor = new Color32(100, 225, 215, 255);
    private Color32 missileShotIsNotPossibleColor = new Color32(230, 140, 90, 255);

    //target-astroid
    public Transform targetTransform;
    public GameObject targetObject;
    public ObstacleMovement obstacleMovementScript;
    public GameObject explosionEffect;//if the target is destroyed

    private void Start()
    {
        gameMasterObject = GameObject.Find("GameMaster");
        gameMaster = gameMasterObject.GetComponent<GameMaster>();

        timeBetweenPossibleShots = timeBetweenShotsByUpgrade[gameMaster.missileUpgradeValue];
        
        if (gameMaster.missileUpgradeValue > 0)//checking if to enable the indicator
        {
            missileIndicator.SetActive(true);
        }
        else
        {
            Debug.Log("False");
            missileIndicator.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (gameMaster.missileUpgradeValue > 0)//only if the upgrade has been bought at least once
        {
            MissileShootIndicator();
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
    private void MissileShootIndicator()
    { 
        if (isShotPossible)
        {
            missileIndicator.GetComponent<Image>().color = missileShotIsPossibleColor;
        }
        else if (isShotPossible == false)
        {
            missileIndicator.GetComponent<Image>().color = missileShotIsNotPossibleColor;
        }
    }
}
