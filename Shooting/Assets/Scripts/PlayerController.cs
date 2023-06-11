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
    private GameObject shootPoint; //자식으로 등록된 shootPoint
    private SpriteRenderer shootSprite; //shootPoint의 SpriteRenderer 

    public float jumpSpeed = 7f;
    public float shootOffset = 0.5f;
    public float spriteOffset = 0.1f;
    public float bulletSpeed = 80f;

    public GameObject bulletPrefab;

    void Start()
    {
        isGrounded = true; //시작할 때 땅에 닿았다고 처리
        shootTimer = 0f; //타이머 초기화
        spriteTimer = 0f; //타이머 초기화

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        shootPoint = transform.GetChild(0).gameObject;
        shootSprite = shootPoint.GetComponent<SpriteRenderer>();

        shootSprite.enabled = false;
    }

    void Update()
    {
        shootTimer += Time.deltaTime; //발사 타이머 초세기
        spriteTimer += Time.deltaTime; //스프라이트 타이머 초세기

        if (rigidbody != null)
        {
            Jump();
            Shoot();
        }
    }

    //점프 처리
    private void Jump()
    {
        //스페이스를 누르고 점프 중이 아니라면
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody.velocity = Vector3.zero; //점프 직전에 속도 초기화

            isGrounded = false;

            rigidbody.velocity = new Vector2(0f, jumpSpeed);
        }
        //스페이스를 뗐을 때 아직 점프 중이었다면
        else if (Input.GetKeyUp(KeyCode.Space) && rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y * 0.5f); //속도 절반으로 감소
        }

        animator.SetBool("Grounded", isGrounded); //애니메이터 조작
    }

    //발사 처리
    private void Shoot()
    {
        //왼쪽 마우스 누르고 shootTimer가 shootOffset을 지났을 때
        if (Input.GetMouseButtonDown(0) && shootTimer > shootOffset)
        {
            shootTimer = 0f;
            spriteTimer = 0f;

            shootSprite.enabled = true; //발사 스프라이트 활성화

            audioSource.Play(); //발사 소리 재생

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스의 월드 포지션
            Vector2 direction = ((Vector2)mousePos - (Vector2)shootPoint.transform.position).normalized; //쏘는 방향의 정규화 벡터

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //direction의 각도를 구함
            Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward); //z축을 기준으로 회전을 돌림

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, bulletRotation); //마우스 방향으로 총알 생성
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed); //총알에 속도 부여
        }
        //spriteTimer가 spriteOffset을 지났을 때
        else if(spriteTimer > spriteOffset)
        {
            shootSprite.enabled = false; //발사 스프라이트 비활성화
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f) //충돌체의 방향이 위를 향하고 있다면
        {
            isGrounded = true;
        }
    }
}
