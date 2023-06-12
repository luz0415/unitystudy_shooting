using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float deadTime = 2f;

    void Start()
    {
        Destroy(gameObject, deadTime);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ε��� ����� �±װ� Enemy��� �Ѵ� Destory
        if(collision.gameObject.tag == "Enemy")
        {
            //�Ѿ� �¾��� �� �Ҹ�
            //���� �߰�
            //�޺� �߰�
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
