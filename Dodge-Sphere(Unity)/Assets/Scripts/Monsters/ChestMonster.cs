using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ChestMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private GetMoney getMoney;
    private MapSetting mapSetting;

    // 기본 스탯
    public int maxHealth;
    public int currentHealth;
    public int money;

    public GameObject baseAttackPrefab; // 총알 프리팹

    // 물기 패턴
    public GameObject[] b_AttackPrefab; // 총알 프리팹
    public float b_AttackSpd; // 총알 속도
    public int b_AttackNum; // 발사 수
    public int b_AttackAngle; // 발사 각도


    // 박치기 패턴
    public float bu_AttackSpd; // 총알 속도
    public int bu_AttackNum; // 발사 수
    public int[] bu_AttackAngles1; // 발사 각도
    public float[] bu_AttackAngles2; // 발사 각도

    // 먹기 패턴
    public float e_AttackSpd = 20f; // 총알 속도
    public int e_AttackNum = 10; // 발사 수
    public int e_BulletNum = 10; // 총알 수

    private Animator anim;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        p_AttackSpawn = GameObject.Find("Manager").GetComponent<P_AttackSpawn>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        getMoney = GameObject.Find("Manager").GetComponent<GetMoney>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 1;
        //maxHealth = 10;
        currentHealth = maxHealth;
        money = 300;

        b_AttackSpd = 10f;
        b_AttackNum = 50;

        bu_AttackSpd = 10f;
        bu_AttackNum = 4;
        bu_AttackAngles1 = new int[] { 135, 180, 225 };
        bu_AttackAngles2 = new float[] { 157.5f, 202.5f };

        e_AttackSpd = 10f;
        e_AttackNum = 10;
        e_BulletNum = 6;

        InvokeRepeating("StartBiteAttack", 1f, 10f); // 패턴 확인용
        //InvokeRepeating("StartPattern", 1f, 7f); // 랜덤 패턴 실행
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(1.5f);

        getMoney.getMoney = money;
        getMoney.PickUpMoney();

        playerMovement.OnTile();
        playerMovement.moveNum = 1;
        playerMovement.currentTile = 0;
        playerMovement.PostionReset(); // 플레이어 위치 초기화

        monsterMap.fireMoved = false;
        p_AttackSpawn.spawned = false;

        cameraMovement.fix = true;

        monsterMap.DeleteCannon();

        mapSetting.MapReset(); // 보스 클리어시 맵 초기화
        mapSetting.StageMapSetting(); // 맵 셋팅

        Destroy(gameObject);
    }

    void StartPattern() // 랜덤 패턴 선택
    {
        int randomPattern = Random.Range(0, 4); // 0 ~ 3 랜덤

        switch (randomPattern)
        {
            case 0:
                StartBiteAttack();
                break;
            case 1:
                StartCryAttack();
                break;
            case 2:
                StartJumpAttack();
                break;
        }
    }

    private void StartBiteAttack()
    {
        anim.SetTrigger("Bite");
        StartCoroutine(BiteAttacks());
    }

    IEnumerator BiteAttacks()
    {
        yield return new WaitForSeconds(0.3f);

        int num = 0;
        bool pos = true;

        for (int j = 0; j < b_AttackNum; j++)
        {
            num = pos ? 1 : 0;
            b_AttackAngle = Random.Range(135, 226); // 탄환의 각도 계산                                             
            Vector3 direction = Quaternion.Euler(0, b_AttackAngle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(b_AttackPrefab[num], bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "BiteAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd; // 탄환 방향 설정
            pos = pos ? false : true;

            Destroy(bullet, 5f); // 2.5초 후 총알 제거

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void StartCryAttack()
    {
        anim.SetTrigger("Cry");
        StartCoroutine(CryAttacks());
    }

    IEnumerator CryAttacks() // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int i = 0; i < 4; i++) // 총 2번 발사
        {
            for (int j = 0; j < 3; j++)
            {
                float angle = bu_AttackAngles1[j]; // 탄환의 각도 계산                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // 총알 위치 설정
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                bullet.name = "CryFireAttack"; // 총알 이름 변경         
                bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd; // 탄환 방향 설정

                Destroy(bullet, 2.5f); // 4초 후 총알 제거
            }

            yield return new WaitForSeconds(0.75f); // 1초 대기

            for (int k = 0; k < 2; k++)
            {
                float angle = bu_AttackAngles2[k]; // 탄환의 각도 계산                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // 총알 위치 설정
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                bullet.name = "CryFireAttack"; // 총알 이름 변경         
                bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd; // 탄환 방향 설정

                Destroy(bullet, 2.5f);
            }
        }
    }

    public void StartJumpAttack()
    {
        StartCoroutine(JumpAttack());
    }

    IEnumerator JumpAttack()
    {
        anim.SetTrigger("Jump");
        for (int j = 0; j < e_AttackNum; j++)
        {
            float angle = Random.Range(135, 225);
            StartCoroutine(Jumpbullet(angle));
        }

        yield return new WaitForSeconds(3);
        anim.SetTrigger("Jump");
        for (int j = 0; j < e_AttackNum; j++)
        {
            float angle = Random.Range(135, 225);
            StartCoroutine(Jumpbullet(angle));
        }
    }

    IEnumerator Jumpbullet(float angle) // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int j = 0; j < e_BulletNum; j++)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "JumpFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * e_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 2.5f);

            yield return new WaitForSeconds(0.1f);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBullet"))
        {
            Bullet bulletComponent = collision.gameObject.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                currentHealth -= bulletComponent.damage;
                anim.SetTrigger("Hit");
            }
            Debug.Log(currentHealth);
            Destroy(collision.gameObject);
        }
    }

}
