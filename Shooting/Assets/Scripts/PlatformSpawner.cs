using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public float spawnTimeMin = 0.7f; //플랫폼 만들어지는 최소 시간
    public float spawnTimeMax = 1.5f; //플랫폼 만들어지는 최대 시간
    public float spawnPosMin = -1.5f; //플랫폼 최저 높이
    public float spawnPosMax = 5.5f; //플랫폼 최대 높이

    public PlatformSpawner instance; //싱글톤

    private float spawnTime;
    private float spawnPos;
    private float timer;
    private int platformNum;
    private List<int> numList;
    private GameObject spawnPlatform;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        numList = new List<int>() { 1, 2, 3, 4, 5 }; //리스트 초기화

        spawnTime = 0f; //첫 플랫폼 만들어지는 시간은 고정
        timer = 0f; //타이머 초기화
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime) // spawnTime의 시간보다 많이 지났으면
        {
            timer = 0f;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax); //시간 계산 초기화
            spawnPos = Random.Range(spawnPosMin, spawnPosMax); //높이 랜덤 지정

            platformNum = ListRandom();
            spawnPlatform = transform.GetChild(platformNum).gameObject; //platformNum번 자식 가져오기

            spawnPlatform.transform.position = new Vector3(0f, spawnPos, 0f);
            spawnPlatform.GetComponent<PlatformMover>().enabled = true;
            spawnPlatform.GetComponent<PlatformMover>().setPlatformNum(platformNum);
        }
        
    }

    //리스트의 한 요소를 랜덤해서 빼오는 메소드
    private int ListRandom()
    {
        int rand = Random.Range(1, numList.Count);
        int num = numList[rand];
        numList.RemoveAt(rand);
        return num;
    }

    public void AddList(int num)
    {
        print(num + "번 추가");
        numList.Add(num);
    }
}
