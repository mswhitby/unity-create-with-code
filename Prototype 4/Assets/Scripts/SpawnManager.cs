using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        //Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
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
