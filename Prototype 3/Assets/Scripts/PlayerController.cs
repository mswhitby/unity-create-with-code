using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;

    public float collisionOffset = .01f;
    public int hitCount;
    public int maxHits = 3;
    public float diff;

    [HideInInspector] public bool isOnGround = true;
    [HideInInspector] public bool isOnObstacle = false;
    [HideInInspector] public bool isHitByObstacle = false;
    [HideInInspector] public bool canJump = true;
    [HideInInspector] public bool freezeScroll = false;
    [HideInInspector] public bool gameOver;

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
        freezeScroll = gameOver || isHitByObstacle;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isOnGround = false;
        }

        if (hitCount == maxHits)
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    } 

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground")) {
        //    isOnGround = true;
        //}

        //if (collision.gameObject.CompareTag("Obstacle"))
        //{

        //    ObstacleController obstacleControllerScript = collision.gameObject.GetComponent<ObstacleController>();
        //    float jumpHeight = transform.position.y;
        //    diff = obstacleControllerScript.obstacleHeight - jumpHeight;

        //    if (diff > .01 && !obstacleControllerScript.isHit)
        //    {
        //        hitCount += 1;
        //        collision.gameObject.GetComponent<ObstacleController>().isHit = true;
        //    }

        //}

        string collisionObjectTag = collision.gameObject.tag;

        switch (collisionObjectTag)
        {
            case "Ground":
                isOnGround = true;
                break;

            case "Obstacle":

                ObstacleController obstacleControllerScript = collision.gameObject.GetComponent<ObstacleController>();
                float jumpHeight = transform.position.y;
                diff = obstacleControllerScript.obstacleHeight - jumpHeight;
                bool isHit = obstacleControllerScript.obstacleHeight - jumpHeight > collisionOffset;

                if (isHit && !obstacleControllerScript.isHit)
                {
                    hitCount += 1;
                    isHitByObstacle = true;
                    obstacleControllerScript.isHit = true;
                }
                else if (isHit)
                {
                    isHitByObstacle = true;
                }
                else
                {
                    isOnObstacle = true;
                }
                break;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Example: Continuously apply an effect or check a condition while touching
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //ObstacleController obstacleControllerScript = collision.gameObject.GetComponent<ObstacleController>();
            //diff = obstacleControllerScript.obstacleHeight - transform.position.y;

            //if (diff > .01)
            //{
            //    isHitByObstacle = true;
            //}
            //else
            //{
            //    isOnObstacle = true;
            //}

            ObstacleController obstacleControllerScript = collision.gameObject.GetComponent<ObstacleController>();
            bool isHit = obstacleControllerScript.obstacleHeight - transform.position.y > collisionOffset;

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

        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isOnGround = false;
        //}

        //if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    isOnObstacle = false;
        //    isHitByObstacle = false;
        //}

        string collisionObjectTag = collision.gameObject.tag;

        switch(collisionObjectTag)
        {
            case "Ground":
                isOnGround = false;
                break;

            case "Obstacle":
                isOnObstacle = false;
                isHitByObstacle = false;
                break;
        }
    }
}
