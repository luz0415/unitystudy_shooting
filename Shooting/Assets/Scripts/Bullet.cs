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
        //부딪힌 상대의 태그가 Enemy라면 둘다 Destory
        if(collision.gameObject.tag == "Enemy")
        {
            //총알 맞았을 때 소리
            //점수 추가
            //콤보 추가
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
