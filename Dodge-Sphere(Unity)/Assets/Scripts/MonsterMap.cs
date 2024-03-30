using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMap : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameSetting gameSetting;

    public GameObject player;
    public bool fireMoved; // 불 몬스터 전투
    public bool cactusMoved; // 선인장 몬스터 전투
    public bool mushMoved; // 버섯 몬스터 전투
    public int monsterNum; // 몬스터 수

    public GameObject monster; // 현재 몬스터

    // 불 & 선인장 몬스터
    public GameObject playerNomalMapSpawnPos; // 몬스터 맵 플레이어 스폰 포인트 
    public GameObject nomalMonsterSpawnPos; // 몬스터 스폰 포인트

    public GameObject fireMonsterPrefab; // 불 몬스터 프리팹
    public GameObject cactusMonsterPrefab; // 선인장 몬스터 프리팹
    public GameObject[] mushMonsterPrefab; // 버섯 몬스터 프리팹


    // 대포
    public GameObject[] cannonPoints; // 대포 설치 위치
    public List<GameObject> cannons;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameSetting = GameObject.Find("Manager").GetComponent<GameSetting>();
    }

    void Start()
    {     
        fireMoved = false;
        cactusMoved = false;
        mushMoved = false;
        monsterNum = 1;
    }
  
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        // 전투 맵 이동 준비
        if (playerMovement.currentTile == 5.1f) // 불 몬스터
        {
            fireMoved = true;
        }
        else if (playerMovement.currentTile == 5.2f) // 선인장 몬스터
        {
            cactusMoved = true;
        }
        else if (playerMovement.currentTile == 5.3f) // 버섯 몬스터
        {
            mushMoved = true;
        }

        // 맵 이동 & 몬스터 소환
        if (fireMoved && monsterNum > 0) // 불 몬스터
        {
            playerMovement.OnGame();
            StartCoroutine(MoveFireMonsterMap());
            InstallationCannons();   
        }
        else if (cactusMoved && monsterNum > 0) // 선인장 몬스터
        {
            playerMovement.OnGame();
            StartCoroutine(MoveCactusMonsterMap());
            InstallationCannons();
        }
        else if (mushMoved && monsterNum > 0) // 버섯 몬스터
        {
            playerMovement.OnGame();
            StartCoroutine(MoveMushMonsterMap());
            InstallationCannons();
        }
    }

    IEnumerator MoveFireMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        player.transform.position = playerNomalMapSpawnPos.transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos.transform.position.x, 0f, nomalMonsterSpawnPos.transform.position.z);
        monster = Instantiate(fireMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "FireMonster";
    }
    IEnumerator MoveCactusMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        player.transform.position = playerNomalMapSpawnPos.transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos.transform.position.x, 0f, nomalMonsterSpawnPos.transform.position.z);
        monster = Instantiate(cactusMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "CactusMonster";
    }
    IEnumerator MoveMushMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        player.transform.position = playerNomalMapSpawnPos.transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos.transform.position.x, 0f, nomalMonsterSpawnPos.transform.position.z);
        monster = Instantiate(fireMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "FireMonster";
    }


    void InstallationCannons()
    {
        int cannonIndex = 0;
        int cannonY = 0;
        foreach (GameObject cannon in gameSetting.cannons)
        {
            Vector3 cannonPos = new Vector3(cannonPoints[cannonIndex].transform.position.x, 2.2f, cannonPoints[0].transform.position.z);
            if(cannonIndex == 0)
            {
                cannonY = 65; 
            }
            else if(cannonIndex == 1)
            {
                cannonY = 115; 
            }
            GameObject p_cannon = Instantiate(cannon, cannonPos, Quaternion.Euler(0, cannonY, 0));
            cannons.Add(p_cannon);
            p_cannon.name = "PlayerCannon";
            cannonIndex++;
        }       
    }

    public void DeleteCannon()
    {
        foreach(GameObject cannon in cannons)
        {
            Destroy(cannon);
        }
    }
}
