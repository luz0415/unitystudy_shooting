using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float moveSpeed = 3.2f;

    private float resetPos = 18f;
    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.x < -resetPos)
        {
            transform.position = new Vector3(resetPos, 0, 0);
        }

        transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
    }
}
