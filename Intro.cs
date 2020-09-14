using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    //text
    public GameObject swipeText;
    public GameObject avoidWallsText;
    public GameObject avoidMeteorsText;
    //next texts etc.
    private float timeForNextText;
    private GameObject[] nextText;
    private int textCounter;

    //scripts
    public RandomGeneratingObstacles obstacleGeneration;
    public SceneLoader sceneLoader;

    private Rigidbody[] rigidbodyArray;//to "stop time" kind of effect

    //for avoid meteors text
    private Vector3 offsetForAstroid = new Vector3(0f, 0f, 17f);
    private bool astroisNotSpawned = true;
    public Transform playerTransform;

    Touch touch;

    void Start()
    {
        obstacleGeneration.enabled = false;//so that we don't spawn obstacles

        swipeText.SetActive(true);//first text to be displayed

        rigidbodyArray = FindObjectsOfType<Rigidbody>();
        MakeGameObjectsKinematic();//so that the player can read peacefully

        nextText = new GameObject[] { swipeText, avoidWallsText, avoidMeteorsText };//saving the order
        textCounter = 0;//for nextText
    }

    void Update()
    {
        if (GameManager.gameHasEnded != true)
        {
            if (timeForNextText >= 4f && timeForNextText < 6f)
            {
                if (Input.touchCount > 0) { touch = Input.GetTouch(0); }

                try//try incase of out of bounds
                {
                    nextText[textCounter].SetActive(true);
                    MakeGameObjectsKinematic();//kinda freeze time
                }
                catch
                {
                    if (astroisNotSpawned)
                    {
                        astroisNotSpawned = false;
                        Instantiate(obstacleGeneration.astroidPrefab,
                            playerTransform.position + offsetForAstroid, Quaternion.identity);
                    }
                    return;
                }
            }
            if (timeForNextText >= 6f && touch.phase == TouchPhase.Began || Input.GetKeyDown("c"))
            {
                GameObjectKinematicUndo();//kinda unfreeze time

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
            /*if (textCounter == 2 && astroisNotSpawned)//it's the avoid meteors
            {
                astroisNotSpawned = false;
                Instantiate(obstacleGeneration.astroidPrefab,
                    transform.position + offsetForAstroid, Quaternion.identity);
            }*/
            timeForNextText += Time.deltaTime;
        }
        if (GameManager.gameHasEnded == true)
        {
            sceneLoader.LoadSceneByName("MainMenu", "Start");
            GameManager.gameHasEnded = false;
        }
    }
    void MakeGameObjectsKinematic()
    {
        for (int i = 0; i < rigidbodyArray.Length; i++)
        {
            rigidbodyArray[i].isKinematic = true;
        }
    }
    void GameObjectKinematicUndo()
    {
        for (int i = 0; i < rigidbodyArray.Length; i++)
        {
            rigidbodyArray[i].isKinematic = false;
        }
    }
}
