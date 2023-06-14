using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead;
    private bool isGrounded;
    private float shootTimer;
    private float spriteTimer;
    private int jumpCount;

    private Rigidbody2D rigid; 
    private Animator animator;
    private AudioSource jumpAudio; //Player�� AudioSource
    private TextFade textFade;

    private GameObject shootPoint; //�ڽ����� ��ϵ� shootPoint
    private SpriteRenderer shootSprite; //shootPoint�� SpriteRenderer 
    private AudioSource shootAudio; //shootPoint�� AudioSource
    private TextMeshPro missText;

    public float missFadeTime = 1.0f;
    public float jumpSpeed = 7f;
    public float shootOffset = 0.5f;
    public float spriteOffset = 0.1f;
    public float bulletSpeed = 80f;
    public int maxJump = 2;

    public GameObject bulletPrefab;

    private void Start()
    {
        isDead = false; //�� ���� ���·� ����
        isGrounded = true; //������ �� ���� ��Ҵٰ� ó��
        shootTimer = 0f; //Ÿ�̸� �ʱ�ȭ
        spriteTimer = 0f; //Ÿ�̸� �ʱ�ȭ

        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
        textFade = GetComponent<TextFade>();

        shootPoint = transform.GetChild(0).gameObject; //�ڽ� shootPoint ��������
        shootSprite = shootPoint.GetComponent<SpriteRenderer>();
        shootAudio = shootPoint.GetComponent<AudioSource>();
        missText = transform.GetChild(1).gameObject.GetComponent<TextMeshPro>(); //�ڽ� Miss Text�� TextMeshPro��������

        shootSprite.enabled = false;
    }

    private void Update()
    {
        if(isDead) return; //�׾��ٸ� �޼ҵ� ��ȯ

        if (transform.position.y < -5.5) Die();

        shootTimer += Time.deltaTime; //�߻� Ÿ�̸� �ʼ���
        spriteTimer += Time.deltaTime; //��������Ʈ Ÿ�̸� �ʼ���

        Jump();
        Shoot();
    }

    //���� ó��
    private void Jump()
    {
        //�����̽��� ������ ���� ���� �ƴ϶��
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJump)
        {
            jumpCount++;
            jumpAudio.Play(); //���� �Ҹ� ���
            rigid.velocity = Vector3.zero; //���� ������ �ӵ� �ʱ�ȭ

            isGrounded = false;


            rigid.velocity = new Vector2(0f, jumpSpeed);
        }
        //�����̽��� ���� �� ���� ���� ���̾��ٸ�
        else if (Input.GetKeyUp(KeyCode.Space) && rigid.velocity.y > 0f)
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y * 0.5f); //�ӵ� �������� ����
        }

        animator.SetBool("Grounded", isGrounded); //�ִϸ����� ����
    }

    //�߻� ó��
    private void Shoot()
    {
        //���� ���콺 ������ shootTimer�� shootOffset�� ������ ��
        if (Input.GetMouseButtonDown(0) && shootTimer > shootOffset)
        {
            shootTimer = 0f;
            spriteTimer = 0f;

            shootSprite.enabled = true; //�߻� ��������Ʈ Ȱ��ȭ
            shootAudio.Play(); //�߻� �Ҹ� ���

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���콺�� ���� ������
            Vector2 direction = ((Vector2)mousePos - (Vector2)shootPoint.transform.position).normalized; //��� ������ ����ȭ ����

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //direction�� ������ ����
            Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward); //z���� �������� ȸ���� ����

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, bulletRotation); //���콺 �������� �Ѿ� ����
            bullet.GetComponent<Bullet>().player = this;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed); //�Ѿ˿� �ӵ� �ο�
        }
        //spriteTimer�� spriteOffset�� ������ ��
        else if(spriteTimer > spriteOffset)
        {
            shootSprite.enabled = false; //�߻� ��������Ʈ ��Ȱ��ȭ
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f) //�浹ü�� ������ ���� ���ϰ� �ִٸ�
        {
            jumpCount = 0;
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    public void Miss()
    {
        StartCoroutine(textFade.FadeTextToZero(missText, missFadeTime));
    }

    public void Die()
    {
        isDead = true;
        rigid.velocity = Vector3.zero;

        GameManager.instance.OnPlayerDead();
    }

}
