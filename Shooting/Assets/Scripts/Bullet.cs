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
    private bool isHit = false; //�� ������Ʈ�� �浹�� ������ �Ǵ�
    private float deadTimer = 0f; //���Ľð� ��� Ÿ�̸�
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
        //�ε��� ����� �±װ� Enemy��� �Ѵ� Destory
        if(collision.gameObject.tag == "Enemy")
        {
            isHit = true;
            GameManager.instance.AddScore();
            //�Ѿ� �¾��� �� �Ҹ�
            Destroy(collision.gameObject);
        }
    }
    private void Dead()
    {
        //�浹�ߴٸ�
        if (isHit)
        {
            Destroy(rigid);
            Destroy(spriteRenderer);

            int combo = GameManager.instance.GetCombo();
            comboText.text = combo.ToString();
            StartCoroutine(textFade.FadeTextToZero(comboText, comboFadeTime)); //text fadeout
            Destroy(gameObject, comboFadeTime);
        }
        //�ð��ʰ����
        else
        {
            player.Miss();
            GameManager.instance.ResetCombo();
            Destroy(gameObject);
        }
    }
}
