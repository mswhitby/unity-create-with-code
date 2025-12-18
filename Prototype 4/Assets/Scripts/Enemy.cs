using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody enemyRb;
    GameObject player;
    public float speed;
    public float enemyCollisionStrength = 2f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -5) {
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject collisionGameObject = collision.gameObject;
            Rigidbody collisionRigidbody = collisionGameObject.GetComponent<Rigidbody>();
            Vector3 awayFromCurrent = collisionGameObject.transform.position - transform.position;

            collisionRigidbody.AddForce(awayFromCurrent * enemyCollisionStrength, ForceMode.Impulse);
        }
    }
}
