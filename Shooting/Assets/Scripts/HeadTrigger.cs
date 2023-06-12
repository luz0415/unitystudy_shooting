using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D playerCollider;

    private void Start()
    {
        player = transform.parent.gameObject;
        playerCollider = player.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCollider.isTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCollider.isTrigger = false;
    }
}
