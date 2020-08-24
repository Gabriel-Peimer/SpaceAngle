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
    public float timeBetweenSpawns = 1f;
    public float timeToSpawn = 1f;

    //for score timing
    private float timeToAddScore = 2f;//for first time
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
        if (Time.timeSinceLevelLoad >= timeToSpawn)
        {
            SpawnBlocks();
            timeToSpawn += timeBetweenSpawns;
        }
        //checking to add score
        if(Time.timeSinceLevelLoad >= timeToAddScore)
        {
            ScoreUpdate();
            timeToAddScore += timeBetweenAddingScore;
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
