using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager = GameManager.Instance;
    public GameObject obstaclePrefab;

    public Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;

    //private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (!gameManager.gameOver && !gameManager.freezeScene)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
