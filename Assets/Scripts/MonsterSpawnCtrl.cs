using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnCtrl : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private float spawnTerm=0.5f;
    [Header("Monsters")]
    [SerializeField]
    private List<GameObject> monsters;
    [SerializeField]
    private int maxMonsterNum=10;
    [Header("Reference")]
    [SerializeField]
    private GameObject parentObj;
    [SerializeField]
    private Transform spawnPos_UP;
    [SerializeField]
    private Transform spawnPos_DOWN;
    [SerializeField]
    private Transform spawnPos_LEFT;
    [SerializeField]
    private Transform spawnPos_RIGHT;

   
    private int monsterSpawnNum=0;
   static private List<GameObject> tempMonsterList=new List<GameObject>();
    private GameObject currentMonster;
    private void Awake()
    {
        MonsterListReset();
    }
   
    private void Update()
    {
       // SpawnPosUpdate();
    }
    private void Start()
    {
        
    }
    public void MonsterSpawnStart()
    {
        GetRandomMonster();
        StartCoroutine(SpawningMonster());  //게임 종료시 코루틴 정지 이벤트 필요-완료
    }
    private void MonsterListReset()
    {
        foreach (var monster in monsters)
        {
            tempMonsterList.Add(monster);
        }
    }

    IEnumerator SpawningMonster()
    {
        yield return new WaitForSecondsRealtime(GetRandSpawnTerm());
        while (true)
        {
            if (GameManager.isGameRunning)
            {
                Spawn();
                yield return new WaitForSecondsRealtime(GetRandSpawnTerm());
            }
            yield return null;
        }
    }
    private void GetRandomMonster()
    {

        int randNum = Random.Range(0, tempMonsterList.Count);
        currentMonster = tempMonsterList[randNum];
        tempMonsterList.RemoveAt(randNum);

        if (tempMonsterList.Count <= 0)
        {
            MonsterListReset();
        }
    }
    
    private float GetRandSpawnTerm()
    {
        int rand = Random.Range(1, 4); //random int 1 ~ 3
        float randomTerm = spawnTerm * rand;
        return randomTerm;
    }
    private void Spawn()
    {
        int randVal = Random.Range(0, 4);
        if (monsterSpawnNum > maxMonsterNum)
        {
            GetRandomMonster();
            monsterSpawnNum = 0;
        }
        monsterSpawnNum++;
        switch (randVal)
        {
            case 0:
                    Instantiate(currentMonster, spawnPos_UP.position, Quaternion.identity, parentObj.transform);
                break;
            case 1:
                     Instantiate(currentMonster, spawnPos_DOWN.position, Quaternion.identity, parentObj.transform);
                break;
            case 2:
                    Instantiate(currentMonster, spawnPos_RIGHT.position, Quaternion.identity, parentObj.transform);
                break;
            case 3:
                    Instantiate(currentMonster, spawnPos_LEFT.position, Quaternion.identity, parentObj.transform);
                break;
        }
    }

}
