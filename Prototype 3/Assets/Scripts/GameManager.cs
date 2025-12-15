using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance { get; private set; }

    public int score = 0;
    public int lives = 3;

    public bool freezeScene = false;
    public bool gameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLife()
    {
        lives--;
        if ( lives <= 0 )
        {
            GameOver();
            
        }
    }

    public void GameOver() 
    {
        gameOver = true;
        Debug.Log("Game Over!");
    }

    public void FreezeScene()
    {
        freezeScene = true;
    }

    public void UnFreezeScene()
    {
        if (!gameOver)
        {
            freezeScene = false;
        }
        
    }
}
