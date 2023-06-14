using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public GameObject player;
    public float speed = 7f;


    void Update()
    {
        if (GameManager.instance.isGameover) return;
        if(player != null) //spawner���� player�� ����� �Ҵ�Ǿ��ٸ�
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position , speed * Time.deltaTime);
    }
}
