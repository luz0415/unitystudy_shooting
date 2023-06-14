using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private Vector3 resetPos;
    private PlatformSpawner spawner;
    private int platformNum = -1;

    public float moveSpeed = 8f;
    public float resetX = -30f;

    private void Start()
    {
        resetPos = new Vector3(-15f, -15f, 0f);
        spawner = transform.parent.GetComponent<PlatformSpawner>();
    }
    void Update()
    {
        if(transform.position.x < resetX)
        {
            if(platformNum != -1)
                spawner.AddList(platformNum);

            transform.position = resetPos;
            this.enabled = false;
        }
        transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f));
    }
    public void setPlatformNum(int num)
    {
        platformNum = num;
    }
}
