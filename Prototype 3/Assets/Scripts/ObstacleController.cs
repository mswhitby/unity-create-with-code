using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float obstacleHeight;
    public bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        obstacleHeight = GetComponent<BoxCollider>().size.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
