using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;

    [HideInInspector] public bool isOnGround = true;
    [HideInInspector] public bool isOnObstacle = false;
    [HideInInspector] public bool isHitByObstacle = false;
    [HideInInspector] public bool canJump = true;
    [HideInInspector] public bool canMove = true;

    public float collisionOffset = .01f;
    //[HideInInspector]  public float jumpDiff;

    // Start is called before the first frame update
    void Start() 
    {
        gameManager = GameManager.Instance;
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        canJump = isOnGround || isOnObstacle || isHitByObstacle;
        canMove = !isHitByObstacle;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string collisionObjectTag = collision.gameObject.tag;

        switch (collisionObjectTag)
        {
            case "Ground":
                isOnGround = true;
                break;

            case "Obstacle":
                processObstaclecollision(collision);
                break;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Example: Continuously apply an effect or check a condition while touching
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            processObstaclecollision(collision);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        string collisionObjectTag = collision.gameObject.tag;

        switch (collisionObjectTag)
        {
            case "Ground":
                isOnGround = false;
                break;

            case "Obstacle":
                isOnObstacle = false;
                isHitByObstacle = false;
                gameManager.UnFreezeScene();
                break;
        }
    }

    public void processObstaclecollision(Collision collision)
    {
        ObstacleController obstacleControllerScript = collision.gameObject.GetComponent<ObstacleController>();
        bool isHit = obstacleControllerScript.obstacleHeight - transform.position.y > collisionOffset;

        if (isHit)
        {
            if (!obstacleControllerScript.isHit)
            {
                gameManager.LoseLife();
            }

            gameManager.FreezeScene();
            isHitByObstacle = true;
            obstacleControllerScript.isHit = true;
        }
        else
        {
            isOnObstacle = true;
        }
    }

}
