using System;
using UnityEngine;
using UnityEngine.UI;

public class RandomGeneratingObstacles : MonoBehaviour
{
    public Transform spawnCube;
    public GameObject astroidPrefab;

    public float timeBetweenSpawns = 1f;
    public float timeToSpawn = 1f;
    private float timeToAddScore = 2f;
    private float timeBetweenAddingScore = 2f;

    public Text scoreText;
    public int score;
    float randomNumber;
    private void Update()
    {
        if (Time.timeSinceLevelLoad >= timeToSpawn)
        {
            SpawnBlocks();
            timeToSpawn += timeBetweenSpawns;
        }else if(Time.timeSinceLevelLoad >= timeToAddScore)
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
