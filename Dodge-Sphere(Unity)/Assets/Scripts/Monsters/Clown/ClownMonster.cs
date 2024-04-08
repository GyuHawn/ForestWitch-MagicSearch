using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownMonster : MonoBehaviour
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

    // 밀기 패턴
    public GameObject[] pushPos1;
    public GameObject[] pushPos2;
    public float p_AttackSpd; // 총알 속도
    public int p_AttackNum; // 공격 수

    // 발사 패턴
    public float a_AttackSpd; // 총알 속도
    public int a_AttackNum; // 발사 수

    // 댄스 패턴
    public float bu_AttackSpd; // 총알 속도
    public int bu_AttackNum; // 발사 수
    public float bu_AttackAngles; // 발사 각도

    // 파괴 패턴
    public GameObject faintAttackPrefab; // 기절 특수 총알
    public float f_AttackSpd; // 총알 속도
    public int f_AttackNum; // 발사 수

    // 소환 패턴
    public int e_EatingNum;
    public bool e_Eating; // 먹기 패턴 중
    public GameObject[] e_EatingPos; // 패턴 생성 위치
    public GameObject[] e_EatingPrefabs;

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

        p_AttackSpd = 10f;
        p_AttackNum = 10;

        bu_AttackSpd = 15f;
        bu_AttackNum = 4;

        a_AttackSpd = 30f;
        a_AttackNum = 5;

       // InvokeRepeating("StartPattern", 1f, 7f); // 랜덤 패턴 실행
        //InvokeRepeating("StartFaintAttack", 3f, 8f); // 랜덤 패턴 실행
        InvokeRepeating("StartPushAttack", 1f, 20f); // 랜덤 패턴 실행
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
                StartPushAttack();
                break;
            case 1:
                StartAimingAttack();
                break;
            case 2:
                StartLaserAttack();
                break;
        }
    }

    private void StartPushAttack()
    {
        StartCoroutine(PushAttacks());
    }

    IEnumerator PushAttacks()
    {
        int pushNum = 0;
        Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // 각도에 따른 방향 계산

        for (int i = 0; i < p_AttackNum; i++)
        {
            anim.SetTrigger("Push");
            if (pushNum == 0)
            {
                pushNum++;
                for (int j = 0; j < pushPos1.Length; j++)
                {
                    Vector3 bulletPos = new Vector3(pushPos1[j].transform.position.x, 2f, pushPos1[j].transform.position.z); // 총알 위치 설정
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                    bullet.name = "PushAttack"; // 총알 이름 변경         
                    bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // 탄환 방향 설정
                    Destroy(bullet, 3f);
                }
            }
            else if (pushNum == 1)
            {
                pushNum--;
                for (int j = 0; j < pushPos2.Length; j++)
                {
                    Vector3 bulletPos = new Vector3(pushPos2[j].transform.position.x, 2f, pushPos2[j].transform.position.z); // 총알 위치 설정
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                    bullet.name = "PushAttack"; // 총알 이름 변경         
                    bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // 탄환 방향 설정
                    Destroy(bullet, 3f);
                }
            }

            yield return new WaitForSeconds(0.7f);
        }
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
            yield return new WaitForSeconds(0.5f);
            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;

            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "AimingAttack"; // 총알 이름 변경

            Vector3 direction = (playerPosition - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * a_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 2f);
        }
    }

    private void StartFaintAttack() // 기절 패턴
    {
        Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
        GameObject bullet = Instantiate(faintAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
        bullet.name = "AimingAttack"; // 총알 이름 변경
    }

    public void StartLaserAttack()
    {
        StartCoroutine(LaserAttack());
    }

    IEnumerator LaserAttack()
    {
        anim.SetBool("Laser", true);
        int num = Random.Range(0, 2);
        bool start = true;

        if (num == 0)
        {
            bu_AttackAngles = 130f; // 총알 시작 각도

            while (start)
            {
                if (bu_AttackAngles >= 220) // 부동소수점 비교를 위해 변경
                {
                    start = false;
                }

                Vector3 direction = Quaternion.Euler(0, bu_AttackAngles, 0) * Vector3.forward;
                Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                bullet.name = "LaserAttack";
                bullet.GetComponent<Rigidbody>().velocity = direction * a_AttackSpd;

                transform.rotation = Quaternion.Euler(0, bu_AttackAngles, 0); // 자신의 회전값 수정

                Destroy(bullet, 2f);

                bu_AttackAngles += 2; // 각도 업데이트

                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (num == 1)
        {
            bu_AttackAngles = 220f; // 총알 시작 각도

            while (start)
            {
                if (bu_AttackAngles <= 130) // 부동소수점 비교를 위해 변경
                {
                    start = false;
                }

                Vector3 direction = Quaternion.Euler(0, bu_AttackAngles, 0) * Vector3.forward;
                Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                bullet.name = "LaserAttack";
                bullet.GetComponent<Rigidbody>().velocity = direction * a_AttackSpd;

                transform.rotation = Quaternion.Euler(0, bu_AttackAngles, 0); // 자신의 회전값 수정

                Destroy(bullet, 2f);

                bu_AttackAngles -= 2; // 각도 업데이트

                yield return new WaitForSeconds(0.1f);
            }
        }

        yield return new WaitForSeconds(0.5f);

        transform.rotation = Quaternion.Euler(0, 180, 0); // 표준 자신의 회전값 적용
        anim.SetBool("Laser", false);
    }

    void EatingMonster()
    {
        e_EatingNum = Random.Range(0, 2);

        if (e_EatingNum == 0)
        {
            GameObject e_Moneter = Instantiate(e_EatingPrefabs[e_EatingNum], e_EatingPos[e_EatingNum].transform.position, Quaternion.Euler(0, 90, 0)); // 패턴 생성
            e_Moneter.name = "EatingMonster";
        }
        if (e_EatingNum == 1)
        {
            GameObject e_Moneter = Instantiate(e_EatingPrefabs[e_EatingNum], e_EatingPos[e_EatingNum].transform.position, Quaternion.Euler(0, -90, 0)); // 패턴 생성
            e_Moneter.name = "EatingMonster";
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
