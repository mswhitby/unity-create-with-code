using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;

    public float collisionOffset = .01f;
    public int lives = 3;
    public float diff;

    [HideInInspector] public bool isOnGround = true;
    [HideInInspector] public bool isOnObstacle = false;
    [HideInInspector] public bool isHitByObstacle = false;
    [HideInInspector] public bool canJump = true;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool gameOver = false;

    // Start is called before the first frame update
    void Start() 
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        canJump = isOnGround || isOnObstacle || isHitByObstacle;
        canMove = !gameOver && !isHitByObstacle;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isOnGround = false;
        }

        if (lives <= 0)
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    } 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {

            ObstacleController obstacleControllerScript = collision.gameObject.GetComponent<ObstacleController>();
            float jumpHeight = transform.position.y;
            diff = obstacleControllerScript.obstacleHeight - jumpHeight;

            if (diff > .01 && !obstacleControllerScript.isHit)
            {
                lives--;
                collision.gameObject.GetComponent<ObstacleController>().isHit = true;
            }

        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Example: Continuously apply an effect or check a condition while touching
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            ObstacleController obstacleControllerScript = collision.gameObject.GetComponent<ObstacleController>();
            diff = obstacleControllerScript.obstacleHeight - transform.position.y;
            bool isHit = diff > collisionOffset;

            if (isHit)
            {
                isHitByObstacle = true;
            }

            else
            {
                isOnObstacle = true;
            }
        }

    }

    void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isOnObstacle = false;
            isHitByObstacle = false;
        }
    }

}
