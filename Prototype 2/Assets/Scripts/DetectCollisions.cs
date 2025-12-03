using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);

            if (gameManager.lives <= 0)
            {
                Destroy(other.gameObject);
                Debug.Log("Game Over"); 
            }

        } else if (other.CompareTag("Food"))
        {
            Debug.Log("Feed Animal"); 
            Destroy(other.gameObject);

            gameObject.GetComponent<AnimalHunger>().FeedAnimal(1);
            // Destroy(gameObject);        
        }
    }
}
