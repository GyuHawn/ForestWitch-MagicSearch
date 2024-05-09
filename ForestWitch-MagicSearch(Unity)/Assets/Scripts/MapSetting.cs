using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Diagnostics.Tracing;

public class MapSetting : MonoBehaviour
{
    public int stage; // 현재 스테이지
    public int adventLevel; // 모험 레벨;

    public GameObject[] tiles; // 셋팅할 타일

    public List<GameObject> settingTile = new List<GameObject>(); // 생성한 타일 오브젝트

    public int tileNum; // 타일 선택

    public GameObject[] randomFloorPos; // 랜덤 타일 위치
    public GameObject[] empyFloorPos; // 고정 빈 타일 위치
    public GameObject[] restFloorPos; // 고정 휴식 타일 위치
    public GameObject bossFloorPos; // 보스 타일 위치

    public GameObject empyFloorPrefab;
    public GameObject restFloorPrefab; // 휴식 타일 프리팹
    public GameObject itemFloorPrefab; // 아이템 타일 프리팹
    public GameObject eventFloorPrefab; // 이벤트 타일 프리팹
    public GameObject shopFloorPrefab; // 상점 타일 프리팹
    public GameObject boss1FloorPrefab; // 1스테이지 보스 타일 프리팹
    public GameObject boss2FloorPrefab; // 2스테이지 보스 타일 프리팹
    public GameObject[] monster1FloorPrefab; // 1스테이지 몬스터 타일 프리팹
    public GameObject[] monster2FloorPrefab; // 2스테이지 몬스터 타일 프리팹

    public int empyFloorNum; // 빈 타일 개수
    public int restFloorNum; // 휴식 타일 개수
    public int itemFloorNum; // 아이템 타일 개수
    public int eventFloorNum; // 이벤트 타일 개수
    public int shopFloorNum; // 상점 타일 개수
    public int monsterFloorNum; // 몬스터 타일 개수

    void Start()
    {
        stage = 1;
        adventLevel = PlayerPrefs.GetInt("AdventLevel", 1);
        StageMapSetting();
    }

    public void StageMapSetting()
    {
        // tileNum을 0부터 4까지 랜덤으로 선택
        tileNum = Random.Range(0, 5);

        // 선택된 tileNum에 따라 각 타일 종류의 개수 설정
        SelectTile(tileNum);

        // 고정 빈 타일 설치
        foreach (GameObject position in empyFloorPos)
        {
            Vector3 pos = new Vector3(position.transform.position.x, 1.2f, position.transform.position.z);
            GameObject empy = Instantiate(empyFloorPrefab, pos, Quaternion.identity, position.transform);
            settingTile.Add(empy);
        }

        // 고정 휴식 타일 설치
        foreach (GameObject position in restFloorPos)
        {
            GameObject rest = Instantiate(restFloorPrefab, position.transform.position, Quaternion.Euler(0, 220, 0), position.transform);
            settingTile.Add(rest);
        }
        // 사용된 위치를 저장하는 리스트
        List<int> usedIndexes = new List<int>();

        // 빈 타일 설치
        for (int i = 0; i < empyFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            GameObject empy = Instantiate(empyFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
            settingTile.Add(empy);
        }

        // 아이템 타일 설치
        for (int i = 0; i < itemFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            GameObject item = Instantiate(itemFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
            settingTile.Add(item);
        }

        // 휴식 타일 설치
        for (int i = 0; i < restFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            GameObject rest = Instantiate(restFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 220, 0), randomFloorPos[randomIndex].transform);
            settingTile.Add(rest);
        }

        // 이벤트 타일 설치
        for (int i = 0; i < eventFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Vector3 spawnPosition = randomFloorPos[randomIndex].transform.position + new Vector3(0, 0.5f, 0);
            GameObject events = Instantiate(eventFloorPrefab, spawnPosition, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
            settingTile.Add(events);
        }

        // 상점 타일 설치
        for (int i = 0; i < shopFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Vector3 spawnPosition = randomFloorPos[randomIndex].transform.position + new Vector3(0, 0.5f, 0);
            GameObject shop = Instantiate(shopFloorPrefab, spawnPosition, Quaternion.Euler(90, 180, 0), randomFloorPos[randomIndex].transform);
            settingTile.Add(shop);
        }

        // 보스 & 몬스터 타일 설치
        if (stage == 1)
        {
            Instantiate(boss1FloorPrefab, bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

            for (int i = 0; i < monsterFloorNum; i++)
            {
                int randomIndex = GetUniqueRandomIndex(usedIndexes);
                int randomMonster = Random.Range(0, monster1FloorPrefab.Length);
                GameObject monster = Instantiate(monster1FloorPrefab[randomMonster], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
                settingTile.Add(monster);
            }
        }
        else if (stage == 2)
        {
            Instantiate(boss2FloorPrefab, bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

            for (int i = 0; i < monsterFloorNum; i++)
            {
                int randomIndex = GetUniqueRandomIndex(usedIndexes);
                int randomMonster = Random.Range(0, monster2FloorPrefab.Length);
                GameObject monster = Instantiate(monster2FloorPrefab[randomMonster], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
                settingTile.Add(monster);
            }
        }
    }

    public void MapReset()
    {
        foreach (GameObject tileObj in settingTile)
        {
            Destroy(tileObj);
        }

        foreach (GameObject tile in tiles)
        {
            Vector3 currentPos = tile.transform.position;

            tile.transform.position = new Vector3(currentPos.x, -10, currentPos.z);
        }

    }

    // 중복되지 않는 랜덤 인덱스 가져오기
    int GetUniqueRandomIndex(List<int> usedIndexes)
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, randomFloorPos.Length);
        } while (usedIndexes.Contains(randomIndex));

        usedIndexes.Add(randomIndex);
        return randomIndex;
    }

    void SelectTile(int num) // 타일 선택
    {
        switch (num)
        {
            case 0: // 총 50개
                empyFloorNum = 10;
                restFloorNum = 5;
                itemFloorNum = 5;
                eventFloorNum = 8;
                shopFloorNum = 3;
                monsterFloorNum = 19;
                break;
            case 1:
                empyFloorNum = 15;
                restFloorNum = 4;
                itemFloorNum = 5;
                eventFloorNum = 6;
                shopFloorNum = 3;
                monsterFloorNum = 17;
                break;
            case 2:
                empyFloorNum = 10;
                restFloorNum = 6;
                itemFloorNum = 5;
                eventFloorNum = 9;
                shopFloorNum = 3;
                monsterFloorNum = 17;
                break;
            case 3:
                empyFloorNum = 12;
                restFloorNum = 4;
                itemFloorNum = 4;
                eventFloorNum = 10;
                shopFloorNum = 3;
                monsterFloorNum = 17;
                break;
            case 4:
                empyFloorNum = 10;
                restFloorNum = 7;
                itemFloorNum = 5;
                eventFloorNum = 7;
                shopFloorNum = 4;
                monsterFloorNum = 17;
                break;
            default:
                break;
        }
    }
}