using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    //text
    public GameObject movementText;
    public GameObject avoidWallsText;
    public GameObject avoidMeteorsText;
    public GameObject missileText;
    public GameObject missileIndicatorText;

    //for missile demo
    public GameObject circleHighlightForMissile;
    public GameObject player;
    private GameObject astroid;

    //next texts etc.
    private float timeForNextText;
    private float timeBetweenText = 4f;
    private GameObject[] nextText;
    private int textCounter;

    //scripts
    public RandomGeneratingObstacles obstacleGeneration;
    public ObstacleMovement obstacleMovment;
    public Missile missileScript;
    public MissileCollision missileCollisionScript;
    public SceneLoader sceneLoader;
    private GameMaster gameMaster;

    private bool hasTapped = false;//to check if the player tapped

    void Start()
    {
        obstacleGeneration.enabled = false;//so that we don't spawn obstacles
        missileCollisionScript.enabled = false;
        missileScript.enabled = false;

        movementText.SetActive(true);//first text to be displayed

        nextText = new GameObject[] { movementText, avoidWallsText,
            avoidMeteorsText, missileText, missileIndicatorText };//saving the order

        textCounter = 0;//for nextText
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void Update()
    {
        if (GameManager.gameHasEnded != true)
        {
            if (timeForNextText >= timeBetweenText)
            {
                try//try incase of out of bounds
                {
                    if (gameMaster.missileUpgradeValue == 0)
                    {
                        if (textCounter < 4)
                        {
                            nextText[textCounter].SetActive(true);
                        }
                    }
                    else if (gameMaster.missileUpgradeValue > 0)
                    {
                        nextText[textCounter].SetActive(true);
                    }
                }
                catch
                {
                    return;
                }
            }
            try//for index out of bounds
            {
                if (Input.touchCount > 0) hasTapped = true;

                if (hasTapped && nextText[textCounter].activeSelf ||
                    Input.GetKeyDown("c") && nextText[textCounter].activeSelf)
                {
                    obstacleMovment.enabled = true;//unfreeze time

                    timeForNextText = 0f;
                    try//try incase of out of bounds
                    {
                        nextText[textCounter].SetActive(false);//setting the one that's active to false
                        textCounter++;//adding to the counter
                    }
                    catch
                    {
                        return;
                    }
                }
                hasTapped = false;
            }
            catch
            {
                return;
            }
            if (avoidMeteorsText.activeSelf && obstacleGeneration.enabled == false)
            {
                obstacleGeneration.enabled = true;
            }
            if (missileText.activeSelf && missileScript.enabled == false)
            {
                missileScript.enabled = true;
                missileCollisionScript.enabled = true;

                astroid = Instantiate(obstacleGeneration.astroidPrefab,
                    player.GetComponent<Rigidbody>().position + new Vector3(0, 0, 10),//position
                    Quaternion.identity);//rotation
                
                circleHighlightForMissile.GetComponent<RectTransform>().position = 
                    Camera.main.WorldToScreenPoint(astroid.GetComponent<Rigidbody>().position);

                circleHighlightForMissile.SetActive(true);
            }else if (missileScript.enabled && astroid.gameObject != null)//if the previous if statement already happend
            {
                circleHighlightForMissile.GetComponent<RectTransform>().position =
                    Camera.main.WorldToScreenPoint(astroid.GetComponent<Rigidbody>().position);
            }else
            {
                circleHighlightForMissile.SetActive(false);
            }
            timeForNextText += Time.deltaTime;//adding time to time counter...
        }
        if (GameManager.gameHasEnded == true)
        {
            sceneLoader.LoadSceneByName("MainMenu", "Start");
            GameManager.gameHasEnded = false;
        }
    }
}
