using UnityEngine;

public class WallObstacleGeneration : MonoBehaviour
{
    /*public Transform[] spawnPoints;
    public Transform[] spawnPointsForAfterStart;
    public GameObject obstaclePrefab;

    public float timeBetweenSpawns = 0.5f;
    public float timeToSpawn = 0.5f;

    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(obstaclePrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= timeToSpawn)
        {
            SpawnObstacles();
            timeToSpawn += timeBetweenSpawns;
        }
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < spawnPointsForAfterStart.Length; i++)
        {
            Instantiate(obstaclePrefab, spawnPointsForAfterStart[i].position, Quaternion.identity);
        }

        GameObject[] wallClones = GameObject.FindGameObjectsWithTag("WallClone");
        foreach (GameObject clone in wallClones)
        {
            float randomX = UnityEngine.Random.Range(0, 359);
            float randomY = UnityEngine.Random.Range(0, 359);
            float randomZ = UnityEngine.Random.Range(0, 359);

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
    }*/
    public GameObject obstaclePrefab;
    public GameObject spawnCube;
    public Transform[] spawnPoints;

    public float timeBetweenSpawns = 0.1f;
    public float timeToSpawn = 0.5f;

    private float xLength;
    private float yLength;
    private float zLength;
    private Vector3 positionToSpawn;

    private void Start()
    {
        //for (int i = 0; i < spawnPoints.Length; i++)
        //{
          //  Instantiate(obstaclePrefab, spawnPoints[i].position, Quaternion.identity);
        //}
        xLength = spawnCube.GetComponent<MeshRenderer>().bounds.size.x;
        yLength = spawnCube.GetComponent<MeshRenderer>().bounds.size.y;
        zLength = spawnCube.GetComponent<MeshRenderer>().bounds.size.z;
    }
    private void Update()
    {
        if (Time.timeSinceLevelLoad >= timeToSpawn)
        {
            SpawnObstacles();
            timeToSpawn += timeBetweenSpawns;
        }
    }
    void SpawnObstacles()
    {
        float randomX = UnityEngine.Random.Range(0, xLength);
        float randomZ = UnityEngine.Random.Range(0, zLength);

        positionToSpawn.x = randomX + spawnCube.transform.position.x;
        positionToSpawn.y = yLength;
        positionToSpawn.z = randomZ + spawnCube.transform.position.z;
        Instantiate(obstaclePrefab, positionToSpawn, Quaternion.identity);

        GameObject[] wallClones = GameObject.FindGameObjectsWithTag("WallClone");
        foreach (GameObject clone in wallClones)
        {
            if (clone.transform.position.z <= -10 || clone.transform.position.z >= 36)
            {
                Destroy(clone);
            }
        }
    }
}
