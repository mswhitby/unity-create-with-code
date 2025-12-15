using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce;
    public float gravityModifier;

    [HideInInspector] public bool isOnGround = true;
    [HideInInspector] public bool isOnObstacle = false;
    [HideInInspector] public bool isHitByObstacle = false;
    [HideInInspector] public bool canJump = true;
    //[HideInInspector] public bool canMove = true;

    public float collisionOffset = .01f;
    //[HideInInspector]  public float jumpDiff;
    //public float jumpDiff;

    // Start is called before the first frame update
    void Start() 
    {
        gameManager = GameManager.Instance;

        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        canJump = (isOnGround || isOnObstacle || isHitByObstacle) && !gameManager.gameOver;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();
        }

        if (gameManager.gameOver)
        {
            dirtParticle.Stop();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string collisionObjectTag = collision.gameObject.tag;

        switch (collisionObjectTag)
        {
            case "Ground":
                isOnGround = true;
                dirtParticle.Play();
                break;

            case "Obstacle":
                dirtParticle.Stop();
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
        //jumpDiff = obstacleControllerScript.obstacleHeight - transform.position.y;

        if (isHit)
        {
            gameManager.FreezeScene();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            isHitByObstacle = true;

            if (!obstacleControllerScript.isHit)
            {
                explosionParticle.Play();
                gameManager.LoseLife();
                obstacleControllerScript.isHit = true;
            }
        }
        else
        {
            isOnObstacle = true;
        }
    }

}
