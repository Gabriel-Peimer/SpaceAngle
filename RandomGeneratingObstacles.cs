using System;
using UnityEngine;
using UnityEngine.UI;

public class RandomGeneratingObstacles : MonoBehaviour
{
    public GameManager GameManager;
    public Transform[] spawnPoints;
    public GameObject blockPrefab;

    public float timeBetweenSpawns = 1f;
    public float timeToSpawn = 1f;
    private float timeToAddScore = 2f;
    private float timeBetweenAddingScore = 2f;

    public Text scoreText;
    public int score;
    int randomNumber = -1;
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
        randomNumber = UnityEngine.Random.Range(0, spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (randomNumber == i)
            {
                float randomX = UnityEngine.Random.Range(0, 359);
                float randomY = UnityEngine.Random.Range(0, 359);
                float randomZ = UnityEngine.Random.Range(0, 359);
                Instantiate(blockPrefab, spawnPoints[i].position, Quaternion.identity);
                GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
                foreach(GameObject clone in clones)
                {
                    if (clone.transform.rotation.x == 0 &&
                        clone.transform.rotation.y == 0 &&
                        clone.transform.rotation.z == 0)
                    {
                        clone.transform.Rotate(randomX, randomY, randomZ);
                    }
                    if (clone.transform.position.z <= -10)
                    {
                        Destroy(clone);
                    }
                }
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
