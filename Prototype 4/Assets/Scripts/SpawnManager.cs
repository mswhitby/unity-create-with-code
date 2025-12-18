using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemy2Prefab;
    public GameObject powerupPrefab;

    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

        //Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        //SpawnEnemy();
        //SpawnEnemyWave();
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
            SpawnEnemy(enemyPrefab);
        }

        if (Input.GetKeyDown("2"))
        {
            SpawnEnemy(enemy2Prefab);
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

    void SpawnEnemy(GameObject enemyType)
    {
        //float spawnPosX = Random.Range(-spawnRange, spawnRange);
        //float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        //Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        Instantiate(enemyType, GenerateSpawnPosition(), enemyType.transform.rotation);
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
            SpawnEnemy(enemyPrefab);
        }

        for (int i = 0; i < numEnemy2; i++)
        {
            SpawnEnemy(enemy2Prefab);
        }
    }

    

    //float CalculateApothem()
    //{
    //    GameObject island = GameObject.Find("Island");
    //    MeshFilter meshFilter = island.GetComponent<MeshFilter>();

    //    if (meshFilter != null)
    //    {
    //        // Get the size of the bounding box in world units
    //        Vector3 objectDimensions = meshFilter.sharedMesh.bounds.size;
    //        Vector3 objectScale = island.transform.localScale;

    //        float width = objectDimensions.x * objectScale.x;
    //        float height = objectDimensions.y * objectScale.y; // For 2D or vertical extent in 3D
    //        float depth = objectDimensions.z * objectScale.z; // For 3D depth

    //        float apothem = Mathf.Min(width, depth) / 2f;
    //        float radius = apothem / 0.866025f;

    //        Debug.Log($"Hexagon Width: {width}, Height: {height}, Depth: {depth}");
    //        Debug.Log($"Hexagon Scale: {objectScale}");
    //        Debug.Log($"Hexagon Apothem: {apothem}");
    //        Debug.Log($"Hexagon Radius: {radius}");

    //        return apothem;
    //    }
    //    else
    //    {
    //        Debug.LogError("No Renderer component found on the GameObject.");
    //        return 5f;
    //    }

    //}
}
