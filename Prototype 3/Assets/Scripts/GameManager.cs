using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;

    public bool freezeScene = false;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.hitCount == player.maxHits)
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }

        freezeScene = gameOver || !player.canMove;
    }
}
