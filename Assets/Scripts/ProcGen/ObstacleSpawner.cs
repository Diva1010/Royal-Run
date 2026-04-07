using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform obstacleParent;

    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float spawnWidth = 4f;

    [SerializeField] float minObstacleSpawnTime = 0.2f;

    void Start()
    {
        StartCoroutine(SpawnObstaclesRoutine());
    }

    public void DecreaseObstacleSpawnTime(float amount)
    {
        spawnInterval -= amount;
        
        if(spawnInterval < minObstacleSpawnTime)
        {
            spawnInterval = minObstacleSpawnTime;
        }
    }

    private IEnumerator SpawnObstaclesRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
            
        }
    }
}

