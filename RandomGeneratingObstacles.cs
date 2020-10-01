using System;
using UnityEngine;
using UnityEngine.UI;

public class RandomGeneratingObstacles : MonoBehaviour
{
    //spawning obstacles
    public Transform spawnCube;//spawning location
    public GameObject astroidPrefab;
    private float previousNumber;//to save the last meteors spawn location (so that we can spawn meteors faster)
    private float randomNumber;//picking a random spot in spawnCube
    //for spawn timing
    public float[] timeBetweenSpawns = { 2f, 1.8f, 1.5f, 1.3f, 1f };
    private int scoreIndex = 0;//to check how quickly to add score & spawn obstacles
    private float timeToSpawn = 1f;
    private bool firstMeteorSpawned = false;

    //for score timing
    private float timeToAddScore = 0f;
    private float timeBetweenAddingScore = 2f;
    private float[] timeBetweenAddingScoreByUpgrade = { 2f, 1.75f, 1.5f, 1.25f, 1f };
    //score
    public Text scoreText;
    public int score;
    private GameMaster gameMaster;//for upgrade value

    private void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

        timeBetweenAddingScore = timeBetweenAddingScoreByUpgrade[gameMaster.scoreSpeedUpgradeValue];
    }

    private void Update()
    {
        //checking to spawn obstacles
        if (timeToSpawn >= timeBetweenSpawns[scoreIndex])
        {
            SpawnObstacles();
            timeToSpawn = 0;
        }
        if (timeToAddScore >= timeBetweenAddingScore)
        {
            ScoreUpdate();
            timeToAddScore = 0;
        }
        timeToSpawn += Time.deltaTime;
        timeToAddScore += Time.deltaTime;

        //start adding speed to spawn rate over time
        if (score / 10 == scoreIndex + 1 && scoreIndex < timeBetweenSpawns.Length - 1)
        {
            scoreIndex++;
        }
    }

    private void SpawnObstacles()
    {
        if (firstMeteorSpawned)//only when it's not the first meteor
        {
            previousNumber = randomNumber;//saving the previous number
        }
        randomNumber = UnityEngine.Random.Range(-spawnCube.lossyScale.x / 2, spawnCube.lossyScale.x / 2);

        if (firstMeteorSpawned)//only when it's not the first meteor
        {
            //the second random location is in the first location
            while (randomNumber > previousNumber - 2.5f && randomNumber < previousNumber + 2.5f)
            {
                //pick a new location
                randomNumber = UnityEngine.Random.Range(-spawnCube.lossyScale.x / 2, spawnCube.lossyScale.x / 2);
            }
        }

        float randomX = UnityEngine.Random.Range(0, 359);
        float randomY = UnityEngine.Random.Range(0, 359);
        float randomZ = UnityEngine.Random.Range(0, 359);

        //instantiates with random rotation and position
        Instantiate(astroidPrefab, spawnCube.position + new Vector3(randomNumber, 0, 0), 
            Quaternion.Euler(randomX, randomY, randomZ));
        

        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");

        foreach (GameObject clone in clones)
        {
            if (clone.transform.position.z <= -10)
            {
                Destroy(clone);
            }
        }
        if (firstMeteorSpawned == false) firstMeteorSpawned = true;//the first meteor has spawned
    }
    void ScoreUpdate()
    {
        score = Convert.ToInt32(scoreText.text);
        score++;
        scoreText.text = Convert.ToString(score);
    }
}
