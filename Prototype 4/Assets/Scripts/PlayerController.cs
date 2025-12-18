//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    private GameObject focalPoint;

    public Rigidbody playerRb;
    public float speed = 5.0f;

    public bool hasPowerup;
    public float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    public float powerupY;

    private Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        Debug.Log($"Time scale: {Time.timeScale}");

        gamepad = Gamepad.current;
        if (gamepad != null) { Debug.Log("Gamepad found"); }

        if (Gamepad.all.Count > 0)
        {
            foreach (var pad in Gamepad.all)
            {
                Debug.Log($"Found gamepad: {pad.name} {pad.enabled}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        //playerRb.AddForce(Vector3.forward * speed * forwardInput);
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -.5f, 0);

        KeyboardInputs();
        if (gamepad != null) { GamepadInputs(); }

        if (transform.position.y < -.15f && hasPowerup)
        {
            transform.position = new Vector3(transform.position.x, -.15f, transform.position.z);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupY = other.transform.position.y;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            //powerupIndicator.gameObject.SetActive(true);
            //powerupIndicator.SetActive(true); // Shortcut
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            GameObject enemy = collision.gameObject;
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = enemy.transform.position - transform.position;

            Debug.Log("Collided with " + enemy.name);
            Debug.Log("Powerup set to " + hasPowerup);

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine() 
    {
        hasPowerup = true;
        powerupIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        //powerupIndicator.SetActive(false); // Shortcut
    }

    void KeyboardInputs()
    {
        if (Input.GetKeyDown("space"))
        {
            Stop();
        }

        if (Input.GetKeyDown("r"))
        {
            Restart();
        }

        if (Input.GetKeyDown("p"))
        {
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    void GamepadInputs()
    {
        if (gamepad.leftTrigger.ReadValue() > 0.1f)
        {
            float breakPower = gamepad.leftTrigger.ReadValue();
            playerRb.velocity = playerRb.velocity * (1 - breakPower);
            Debug.Log($"Break Power: {breakPower}");
        }

        if (gamepad.leftShoulder.wasPressedThisFrame)
        {
            Restart();
            Debug.Log("leftShoulder pressed");
        }
           
        if (gamepad.rightShoulder.wasPressedThisFrame)
        {
            StartCoroutine(PowerupCountdownRoutine());
        }
            
    }



    void Stop()
    {
        playerRb.velocity = Vector3.zero;
        //playerRb.angularVelocity = Vector3.zero; // Also stop rotation
    }

    void Restart()
    {
        playerRb.MovePosition(Vector3.zero);
        Stop();
    }

    
}
