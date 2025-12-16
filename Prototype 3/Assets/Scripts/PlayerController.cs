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

    private Animator playerAnim;
    
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;


    // Start is called before the first frame update
    void Start() 
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
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
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        if (lives <= 0)
        {
            gameOver = true;
            Debug.Log("Game Over!");

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    } 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();

            ObstacleController obstacleControllerScript = collision.gameObject.GetComponent<ObstacleController>();
            float jumpHeight = transform.position.y;
            diff = obstacleControllerScript.obstacleHeight - jumpHeight;

            if (diff > .01 && !obstacleControllerScript.isHit)
            {
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound, 1.0f);
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
