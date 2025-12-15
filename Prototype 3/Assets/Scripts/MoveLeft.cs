using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30;
    private float leftBound = -10;
    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.canMove)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (gameObject.CompareTag("Obstacle"))
        {

            if (transform.position.x < leftBound)
            {
                Destroy(gameObject);
            }
        }
    }
}
