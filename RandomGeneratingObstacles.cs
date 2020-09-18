using System;
using UnityEngine;
using UnityEngine.UI;

public class RandomGeneratingObstacles : MonoBehaviour
{
    //spawning obstacles
    public Transform spawnCube;//spawning location
    public GameObject astroidPrefab;
    private float randomNumber;//picking a random spot in spawnCube
    //for spawn timing
    public float[] timeBetweenSpawns = { 2f, 1.8f, 1.6f, 1.4f, 1.3f };
    private int scoreIndex = 0;
    public float timeToSpawn = 1f;

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
            SpawnBlocks();
            timeToSpawn = 0;
        }
        if (timeToAddScore >= timeBetweenAddingScore)
        {
            ScoreUpdate();
            timeToAddScore = 0;
        }
        timeToSpawn += Time.deltaTime;
        timeToAddScore += Time.deltaTime;
        /*if (Time.timeSinceLevelLoad >= timeToSpawn)
        {
            SpawnBlocks();
            timeToSpawn += timeBetweenSpawns[scoreIndex];
        }
        //checking to add score
        if(Time.timeSinceLevelLoad >= timeToAddScore)
        {
            ScoreUpdate();
            timeToAddScore += timeBetweenAddingScore;
        }*/
        //start adding speed to spawn rate over time
        if (score / 10 == scoreIndex + 1 && scoreIndex < timeBetweenSpawns.Length - 1)
        {
            scoreIndex++;
        }
    }

    private void SpawnBlocks()
    {

        randomNumber = UnityEngine.Random.Range(-spawnCube.lossyScale.x / 2, spawnCube.lossyScale.x / 2);

        float randomX = UnityEngine.Random.Range(0, 359);
        float randomY = UnityEngine.Random.Range(0, 359);
        float randomZ = UnityEngine.Random.Range(0, 359);

        Instantiate(astroidPrefab, spawnCube.position + new Vector3(randomNumber, 0, 0),
            Quaternion.Euler(randomX, randomY, randomZ));//instantiates with random rotation (at random position)

        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");

        foreach (GameObject clone in clones)
        {
            if (clone.transform.position.z <= -10)
            {
                Destroy(clone);
            }
        }
    }
    void ScoreUpdate()
    {
        score = Convert.ToInt32(scoreText.text);
        score++;
        scoreText.text = Convert.ToString(score);
    }
}
