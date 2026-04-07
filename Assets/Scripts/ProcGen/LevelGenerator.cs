using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
   [SerializeField] CameraController cameraController;
   [SerializeField] GameObject[] chunkPrefabs;
   [SerializeField] GameObject checkpointChunkPrefab;
   [SerializeField] Transform chunkParent;
   [SerializeField] ScoreManager scoreManager;

   [Header("Level Settings")][Tooltip("Number of chunks to spawn at the start of the game")]
    [SerializeField] int numberOfChunks = 12;
    [SerializeField] int checkpointChunkInterval = 8;
   [Tooltip("Do Not change chunk length value unless chunk prefab size is changed")]
   [SerializeField] float chunkLength = 10f;
   [SerializeField] float moveSpeed = 8f;
   [SerializeField] float minMoveSpeed = 2f;
   [SerializeField] float maxMoveSpeed = 20f;
   [SerializeField] float minGravityZ = -22f;
   [SerializeField] float maxGravityZ = -2f;

   List<GameObject>chunks = new List<GameObject>();
   int chunksSpawned = 0;

    void Start()
    {
        SpawnChunks();
    }
    void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float newMoveSpeed = Mathf.Clamp(moveSpeed + speedAmount, minMoveSpeed, maxMoveSpeed);

        if(newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;
            
            float newGravityZ = Mathf.Clamp(Physics.gravity.z - speedAmount, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
            cameraController.ChangeCameraFOV(speedAmount);
        } 

    }

    private void SpawnChunks()
    {
        chunks.Clear();
        for (int i = 0; i < numberOfChunks; i++)
        {
            AddNewChunk(i);
        }
    }

    private void AddNewChunk(int positionIndex)
    {
       float spawnPositionZ = positionIndex == 0 ? transform.position.z : chunks[chunks.Count -1].transform.position.z + chunkLength;
        GameObject chunkToSpawn = ChooseChunkToSpawn();

        GameObject newChunk = Instantiate(chunkToSpawn, new Vector3(transform.position.x, transform.position.y, spawnPositionZ), Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
        Chunk newChunkComponent = newChunk.GetComponent<Chunk>();
        newChunkComponent.Init(this, scoreManager);

        chunksSpawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;
        
        if(chunksSpawned % checkpointChunkInterval == 0 && chunksSpawned != 0)
        {
            chunkToSpawn = checkpointChunkPrefab;
        }
        else
        {
            chunkToSpawn = chunkPrefabs[UnityEngine.Random.Range(0, chunkPrefabs.Length)];
        }
        return chunkToSpawn;
    }

    void MoveChunks()
    {
         for(int i=0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(Vector3.back * (moveSpeed * Time.deltaTime));
            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                AddNewChunk(chunks.Count-1);
            }
        }        
    }

}
