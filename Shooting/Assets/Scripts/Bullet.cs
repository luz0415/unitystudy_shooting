using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float comboFadeTime = 1.0f;
    public float deadTime = 2f;
    public PlayerController player;

    private bool isDead = false;
    private TextMeshPro comboText;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private bool isHit = false; //적 오브젝트와 충돌한 것인지 판단
    private float deadTimer = 0f; //폭파시간 재는 타이머
    private TextFade textFade;

    void Start()
    {
        textFade = GetComponent<TextFade>();
        comboText = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;
        deadTimer += Time.deltaTime;
        
        if(deadTimer > deadTime || isHit)
        {
            isDead = true;
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //부딪힌 상대의 태그가 Enemy라면 둘다 Destory
        if(collision.gameObject.tag == "Enemy")
        {
            isHit = true;
            GameManager.instance.AddScore();
            //총알 맞았을 때 소리
            Destroy(collision.gameObject);
        }
    }
    private void Dead()
    {
        //충돌했다면
        if (isHit)
        {
            Destroy(rigid);
            Destroy(spriteRenderer);

            int combo = GameManager.instance.GetCombo();
            comboText.text = combo.ToString();
            StartCoroutine(textFade.FadeTextToZero(comboText, comboFadeTime)); //text fadeout
            Destroy(gameObject, comboFadeTime);
        }
        //시간초과라면
        else
        {
            player.Miss();
            GameManager.instance.ResetCombo();
            Destroy(gameObject);
        }
    }
}
