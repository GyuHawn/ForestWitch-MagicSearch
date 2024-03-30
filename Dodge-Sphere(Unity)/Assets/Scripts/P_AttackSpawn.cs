using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackSpawn : MonoBehaviour
{
    private MonsterMap monsterMap;

    // 불 몬스터
    public GameObject f_SpawnPoint;
    public Vector3 f_BoxSize;

    // 공격 생성 위치
    public GameObject p_AttackPrefab; // 플레이어 공격 프리팹
    public GameObject spawnPoint; // 공격 생성 위치
    public Vector3 boxSize; // 생성 범위

    // 아이템 생성 유무
    public bool spawned;

    public GameObject currentAttack; // 현재 생성된 p_Attack의 레퍼런스

    private void Awake()
    {
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
    }

    void Start()
    {
        spawned = false;
        currentAttack = null;
    }

    void Update()
    {
        if (monsterMap.fireMoved || monsterMap.cactusMoved)
        {
            spawnPoint = f_SpawnPoint;
            boxSize = f_BoxSize;
            spawned = true;
        }

        if (spawned)
        {
            if(currentAttack == null)
            {
                SpawnAttack();
            }
        }
    }
    void SpawnAttack()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-boxSize.x / 2, boxSize.x / 2), 0, Random.Range(-boxSize.z / 2, boxSize.z / 2)) + spawnPoint.transform.position;
        currentAttack = Instantiate(p_AttackPrefab, spawnPosition, Quaternion.identity);
        currentAttack.name = "PlayerAttack";

        Destroy(currentAttack, 5f);
    }

    /*private void OnDrawGizmos()
    {
        if(spawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(spawnPoint.transform.position, boxSize);
        }
    }*/
}
