using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject powerupPrefab;

    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            SpawnPowerup();
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }

        if (Input.GetKeyDown("1"))
        {
            SpawnEnemy(0);
        }

        if (Input.GetKeyDown("2"))
        {
            SpawnEnemy(1);
        }

        if (Input.GetKeyDown("0"))
        {
            SpawnPowerup();
        }

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    void SpawnEnemy(int enemyType)
    {
        Vector3 spawnPos = GenerateSpawnPosition();
        Instantiate(enemyPrefabs[enemyType], spawnPos, enemyPrefabs[enemyType].transform.rotation);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        int numEnemy1;
        int numEnemy2;

        if (enemiesToSpawn < 3)
        {
            numEnemy1 = enemiesToSpawn;
            numEnemy2 = 0;
        }

        else if (enemiesToSpawn < 8)
        {
            int randomInt = Random.Range(0, enemiesToSpawn);
            numEnemy1 = Mathf.Max(randomInt, (enemiesToSpawn - randomInt));
            numEnemy2 = enemiesToSpawn - numEnemy1; ;
        }

        else
        {
            numEnemy1 = Random.Range(0, enemiesToSpawn);
            numEnemy2 = enemiesToSpawn - numEnemy1; ;
        }

        Debug.Log($"Enemy1: {numEnemy1}, Enemy2: {numEnemy2}");

        for (int i = 0; i < numEnemy1; i++)
        {
            SpawnEnemy(0);
        }

        for (int i = 0; i < numEnemy2; i++)
        {
            SpawnEnemy(1);
        }
    }
}
