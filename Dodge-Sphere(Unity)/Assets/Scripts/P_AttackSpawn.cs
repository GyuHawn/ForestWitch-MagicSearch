using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackSpawn : MonoBehaviour
{
    private MonsterMap monsterMap;
    private MapSetting mapSetting;

    // Stage1
    public GameObject Stage1SpawnPoint;
    public Vector3 Stage1BoxSize;
    // Stage2
    public GameObject Stage2SpawnPoint;
    public Vector3 Stage2BoxSize;
    public Vector3 Stage2ExceptSize;

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
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
    }

    void Start()
    {
        spawned = false;
        currentAttack = null;
    }

    void Update()
    {
        if (monsterMap.fireMoved || monsterMap.cactusMoved || monsterMap.mushMoved)
        {
            spawnPoint = Stage1SpawnPoint;
            boxSize = Stage1BoxSize;
            spawned = true;
        }
        else if (monsterMap.chsetMoved)
        {
            spawnPoint = Stage2SpawnPoint;
            boxSize = Stage2BoxSize;
            spawned = true;
        }

        if (spawned)
        {
            if(currentAttack == null)
            {
                if(mapSetting.stage == 1)
                {
                    Stage1SpawnAttack();
                }
                else if (mapSetting.stage == 2)
                {
                    Stage2SpawnAttack();
                }
            }
        }
    }
    void Stage1SpawnAttack()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-boxSize.x / 2, boxSize.x / 2), 0, Random.Range(-boxSize.z / 2, boxSize.z / 2)) + spawnPoint.transform.position;
        currentAttack = Instantiate(p_AttackPrefab, spawnPosition, Quaternion.identity);
        currentAttack.name = "PlayerAttack";

        Destroy(currentAttack, 5f);
    }

    void Stage2SpawnAttack()
    {
        bool isValidPosition = false;
        Vector3 spawnPosition = Vector3.zero;

        // 최대 시도 횟수를 정해 무한 루프에 빠지지 않도록 합니다.
        int maxAttempts = 100;
        int attempts = 0;

        while (!isValidPosition && attempts < maxAttempts)
        {
            attempts++;
            // Stage2BoxSize 안에서 랜덤한 위치 생성
            spawnPosition = new Vector3(Random.Range(-boxSize.x / 2, boxSize.x / 2), 0, Random.Range(-boxSize.z / 2, boxSize.z / 2)) + spawnPoint.transform.position;

            // 생성된 위치가 Stage2ExceptSize 안에 있는지 확인
            if (!IsInExclusionZone(spawnPosition))
            {
                isValidPosition = true;
            }
        }

        if (isValidPosition)
        {
            currentAttack = Instantiate(p_AttackPrefab, spawnPosition, Quaternion.identity);
            currentAttack.name = "PlayerAttack";

            Destroy(currentAttack, 5f); // 5초 후에 공격 오브젝트 파괴
        }
    }

    // 생성된 위치가 제외 구역 안에 있는지 판별하는 함수
    bool IsInExclusionZone(Vector3 position)
    {
        // Stage2ExceptSize를 기준으로 중심점을 계산합니다.
        Vector3 exclusionCenter = spawnPoint.transform.position + new Vector3(0, 0, Stage2ExceptSize.z / 2);

        // 제외 구역의 경계를 계산합니다.
        float minX = exclusionCenter.x - Stage2ExceptSize.x / 2;
        float maxX = exclusionCenter.x + Stage2ExceptSize.x / 2;
        float minZ = exclusionCenter.z - Stage2ExceptSize.z / 2;
        float maxZ = exclusionCenter.z + Stage2ExceptSize.z / 2;

        // 위치가 제외 구역 안에 있는지 확인합니다.
        return position.x >= minX && position.x <= maxX && position.z >= minZ && position.z <= maxZ;
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
