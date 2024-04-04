using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderMonster : MonoBehaviour
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

    // 멀티샷 패턴
    public float m_AttackSpd; // 총알 속도
    public int m_BulletNum; // 총알 수

    // 조준 패턴
    public float a_AttackSpd; // 총알 속도
    public int a_AttackNum; // 발사 수

    // 석화 패턴
    public GameObject faintAttackPrefab; // 기절 특수 총알
    public float f_AttackSpd; // 총알 속도
    public int f_AttackNum; // 발사 수

    // 레이저 패턴
    public float bu_AttackSpd; // 총알 속도
    public int bu_AttackNum; // 발사 수
    public int bu_BulletNum; // 총알 수
    public Vector3 bu_AttackAngles; // 발사 각도


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

        m_AttackSpd = 10f;
        m_BulletNum = 100;

        bu_AttackSpd = 15f;
        bu_AttackNum = 4;
        bu_BulletNum = 10;

        a_AttackSpd = 30f;
        a_AttackNum = 5;

        f_AttackSpd = 20f;
        f_AttackNum = 3;

        //InvokeRepeating("StartPattern", 1f, 7f); // 랜덤 패턴 실행
        InvokeRepeating("StartFaintAttack", 1f, 7f); // 랜덤 패턴 실행
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
        int randomPattern = Random.Range(0, 3);
        switch (randomPattern)
        {
            case 0:
                StartMultiAttack();
                break;
            case 1:
                StartAimingAttack();
                break;
            case 2:
                StartEatingAttack();
                break;
        }
    }

    private void StartMultiAttack()
    {
        StartCoroutine(MultiAttacks());
    }

    IEnumerator MultiAttacks()
    {
        anim.SetBool("Multi", true);
        for (int j = 0; j < m_BulletNum; j++)
        {
            float m_AttackAngle = Random.Range(135, 226); // 탄환의 각도 계산                                             
            Vector3 direction = Quaternion.Euler(0, m_AttackAngle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "MultiShotAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * m_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 3f);

            yield return new WaitForSeconds(0.05f);
        }
        anim.SetBool("Multi", false);
    }

    private void StartAimingAttack()
    {
        StartCoroutine(AimingAttacks());
    }

    IEnumerator AimingAttacks()
    {
        for (int i = 0; i < a_AttackNum; i++)
        {
            anim.SetTrigger("Aiming");
            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;                                            
            GameObject bullet = Instantiate(baseAttackPrefab, gameObject.transform.position, Quaternion.identity); // 총알 생성
            bullet.name = "AimingAttack"; // 총알 이름 변경
            Vector3 direction = (playerPosition - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * a_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 2f);

            yield return new WaitForSeconds(1f);
        }
    }

    private void StartFaintAttack()
    {
        StartCoroutine(FaintAttacks());
    }

    IEnumerator FaintAttacks()
    {
        for (int i = 0; i < f_AttackNum; i++)
        {
            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;
            GameObject bullet = Instantiate(faintAttackPrefab, gameObject.transform.position, Quaternion.identity); // 총알 생성
            bullet.name = "AimingAttack"; // 총알 이름 변경
            Vector3 direction = (playerPosition - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * f_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 3f);

            yield return new WaitForSeconds(2f);
        }
    }

    public void StartEatingAttack()
    {
        StartCoroutine(EatingAttack());
    }

    IEnumerator EatingAttack()
    {
        for (int j = 0; j < bu_BulletNum; j++)
        {
            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;
            GameObject bullet = Instantiate(baseAttackPrefab, gameObject.transform.position, Quaternion.identity); // 총알 생성
            bullet.name = "ButtFireAttack";
            Vector3 direction = (playerPosition - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd;
            Destroy(bullet, 2.5f);
            yield return new WaitForSeconds(0.2f);
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
