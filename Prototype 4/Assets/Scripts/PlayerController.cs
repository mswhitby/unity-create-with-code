//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public float speed = 5.0f;

    private GameObject focalPoint;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        //playerRb.AddForce(Vector3.forward * speed * forwardInput);
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput);

        if (Input.GetKeyDown("space"))
        {
            Stop();
        }

        if (Input.GetKeyDown("r"))
        {
            Restart();
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
