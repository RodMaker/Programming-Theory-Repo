using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;

    private float spawnRangeX = 10.0f;
    private float spawnPosX;
    private float spawnPosY = 0.5f;
    private float spawnPosZ = -30.0f;
    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", startDelay, spawnInterval);
    }

    void SpawnEnemies()
    {
        spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
        Instantiate(enemiesPrefabs[0], spawnPos, enemiesPrefabs[0].transform.rotation);
        Instantiate(enemiesPrefabs[1], spawnPos, enemiesPrefabs[1].transform.rotation);
        Instantiate(enemiesPrefabs[2], spawnPos, enemiesPrefabs[2].transform.rotation);
    }
}
