using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public float spawnTimeMin = 0.7f; //�÷��� ��������� �ּ� �ð�
    public float spawnTimeMax = 1.5f; //�÷��� ��������� �ִ� �ð�
    public float spawnPosMin = -1.5f; //�÷��� ���� ����
    public float spawnPosMax = 5.5f; //�÷��� �ִ� ����

    public PlatformSpawner instance; //�̱���

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
        numList = new List<int>() { 1, 2, 3, 4, 5 }; //����Ʈ �ʱ�ȭ

        spawnTime = 0f; //ù �÷��� ��������� �ð��� ����
        timer = 0f; //Ÿ�̸� �ʱ�ȭ
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime) // spawnTime�� �ð����� ���� ��������
        {
            timer = 0f;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax); //�ð� ��� �ʱ�ȭ
            spawnPos = Random.Range(spawnPosMin, spawnPosMax); //���� ���� ����

            platformNum = ListRandom();
            spawnPlatform = transform.GetChild(platformNum).gameObject; //platformNum�� �ڽ� ��������

            spawnPlatform.transform.position = new Vector3(0f, spawnPos, 0f);
            spawnPlatform.GetComponent<PlatformMover>().enabled = true;
            spawnPlatform.GetComponent<PlatformMover>().setPlatformNum(platformNum);
        }
        
    }

    //����Ʈ�� �� ��Ҹ� �����ؼ� ������ �޼ҵ�
    private int ListRandom()
    {
        int rand = Random.Range(1, numList.Count);
        int num = numList[rand];
        numList.RemoveAt(rand);
        return num;
    }

    public void AddList(int num)
    {
        print(num + "�� �߰�");
        numList.Add(num);
    }
}
