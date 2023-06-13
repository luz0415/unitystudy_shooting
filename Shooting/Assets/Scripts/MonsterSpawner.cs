using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<GameObject> MonsterPrefabs = new List<GameObject>(); //0 bee 1 fly
    public GameObject player;
    public float monsterSpawnTimeMin = 1.2f;
    public float monsterSpawnTimeMax = 1.7f;
    public float monsterSpawnPosMin = -6.0f;
    public float monsterSpawnPosMax = 6f;

    private float monsterSpawnPos;
    private float monsterSpawnTime;
    private float timer;
    private float randomMonsterSelector;

    private GameObject monster;

    void Start()
    {
        if(MonsterPrefabs.Count < 2)
        {
            gameObject.SetActive(false);
        }
        timer = 0f;
        monsterSpawnTime = Random.Range(monsterSpawnTimeMin, monsterSpawnTimeMax);
        monsterSpawnPos = Random.Range(monsterSpawnPosMin, monsterSpawnPosMax);
        randomMonsterSelector = Random.value;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if(timer > monsterSpawnTime)
        {

            if(randomMonsterSelector >= 0.7) // bee
            {
                monster = Instantiate(MonsterPrefabs[0], new Vector3(11f, monsterSpawnPos, 0f), Quaternion.identity);
            }
            else //fly
            {
                monster = Instantiate(MonsterPrefabs[1], new Vector3(11f, monsterSpawnPos, 0f), Quaternion.identity);
            }
            monster.GetComponent<MonsterController>().player = player; //복제된 몬스터의 player를 미리 설정해둔 player로 설정해줌

            timer = 0f;
            monsterSpawnTime = Random.Range(monsterSpawnTimeMin, monsterSpawnTimeMax);
            monsterSpawnPos = Random.Range(monsterSpawnPosMin, monsterSpawnPosMax);
            randomMonsterSelector = Random.value;
        }
    }
}
