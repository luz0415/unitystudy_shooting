using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    private float shootTimer;
    private float spriteTimer;

    private Rigidbody2D rigidbody; 
    private Animator animator;
    private AudioSource audioSource;
    private GameObject shootPoint; //�ڽ����� ��ϵ� shootPoint
    private SpriteRenderer shootSprite; //shootPoint�� SpriteRenderer 

    public float jumpSpeed = 7f;
    public float shootOffset = 0.5f;
    public float spriteOffset = 0.1f;
    public float bulletSpeed = 80f;

    public GameObject bulletPrefab;

    void Start()
    {
        isGrounded = true; //������ �� ���� ��Ҵٰ� ó��
        shootTimer = 0f; //Ÿ�̸� �ʱ�ȭ
        spriteTimer = 0f; //Ÿ�̸� �ʱ�ȭ

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        shootPoint = transform.GetChild(0).gameObject;
        shootSprite = shootPoint.GetComponent<SpriteRenderer>();

        shootSprite.enabled = false;
    }

    void Update()
    {
        shootTimer += Time.deltaTime; //�߻� Ÿ�̸� �ʼ���
        spriteTimer += Time.deltaTime; //��������Ʈ Ÿ�̸� �ʼ���

        if (rigidbody != null)
        {
            Jump();
            Shoot();
        }
    }

    //���� ó��
    private void Jump()
    {
        //�����̽��� ������ ���� ���� �ƴ϶��
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody.velocity = Vector3.zero; //���� ������ �ӵ� �ʱ�ȭ

            isGrounded = false;

            rigidbody.velocity = new Vector2(0f, jumpSpeed);
        }
        //�����̽��� ���� �� ���� ���� ���̾��ٸ�
        else if (Input.GetKeyUp(KeyCode.Space) && rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y * 0.5f); //�ӵ� �������� ����
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

            audioSource.Play(); //�߻� �Ҹ� ���

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���콺�� ���� ������
            Vector2 direction = ((Vector2)mousePos - (Vector2)shootPoint.transform.position).normalized; //��� ������ ����ȭ ����

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //direction�� ������ ����
            Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward); //z���� �������� ȸ���� ����

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, bulletRotation); //���콺 �������� �Ѿ� ����
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
            isGrounded = true;
        }
    }
}
