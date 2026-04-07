using System;
using UnityEngine;
using UnityEngine.AI;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float timeIncreaseAmount = 5f;
    [SerializeField] float obstacleSpawnTimeDecreaseAmount = 0.2f;
    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;

    const string playerString = "Player";

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerString))
        {
            gameManager.IncreaseTimer(timeIncreaseAmount);
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleSpawnTimeDecreaseAmount);
        }
    }
}
