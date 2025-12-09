using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Vehicle Settings")]
    [Tooltip("Speed in points per frame")]
    public float speed = 5.0f;
    //public float maxSpeed = 5.0f;
    //public float acceleration = 2.0f;
    public float turnSpeed;

    //private float currentSpeed = 0f;
    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Acceleration; Still needs code for breaking
        //float targetSpeed = forwardInput * maxSpeed;
        //currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        //transform.Translate(Vector3.forward * Time.deltaTime  * currentSpeed);

        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
    }
}
