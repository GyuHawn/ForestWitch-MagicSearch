using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MushRoomMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private MonsterGetMoney monsterGetMoney;
    private HpBarScript hpBarScript;
    private ClearInfor clearInfor;
    private AudioManager audioManager;

    public GameObject monster;

    // 기본 스탯
    public int maxHealth;
    public int currentHealth;
    public int money;

    public GameObject baseAttackPrefab; // 총알 프리팹

    // 박치기 패턴
    public float b_AttackSpd; // 총알 속도
    public int b_BulletNum; // 발사 수
    private int b_CurrentNumIndex; // 현재 총알 인덱스

    // 돌기 패턴
    public float s_AttackSpd; // 총알 속도
    public int s_BulletNum; // 발사 수
    private int[] s_BulletNums; // 총알 개수 배열
    private int s_CurrentNumIndex; // 현재 총알 인덱스

    // 올려치기 패턴
    public float u_AttackSpd = 20f; // 총알 속도
    public int u_AttackNum = 10; // 발사 수
    public int u_BulletNum = 10; // 총알 수

    public bool smlie; // smlie - true, angry - false (몬스터 확인)

    private Animator anim;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        p_AttackSpawn = GameObject.Find("Manager").GetComponent<P_AttackSpawn>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        monsterGetMoney = GameObject.Find("Manager").GetComponent<MonsterGetMoney>();
        hpBarScript = GameObject.Find("MosterHP").GetComponent<HpBarScript>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 6;
        currentHealth = maxHealth;
        money = 150;
        
        b_AttackSpd = 10;
        b_BulletNum = 3;

        s_AttackSpd = 8f;
        s_BulletNums = new int[] { 15, 14, 15, 14, 15 };

        u_AttackSpd = 10f;
        u_AttackNum = 5;
        u_BulletNum = 3;

        InvokeRepeating("StartPattern", 3f, 7f); // 랜덤 패턴 실행

        hpBarScript.MoveToYStart(10, 0.5f);
    }

    void Update()
    {
        if (!smlie && monster == null)
        {
            monster = GameObject.Find("SmileMonster");
        }
        else if(smlie && monster == null)
        {
            monster = GameObject.Find("AngryMonster");
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {       
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(1f);

        if (monster != null)
        {
            hpBarScript.ResetHealthBar();
        }
        else
        {
            monsterGetMoney.getMoney = money;
            monsterGetMoney.PickUpMoney();

            hpBarScript.MoveToYStart(150, 0.1f);
            hpBarScript.ResetHealthBar();

            playerMovement.OnTile();
            playerMovement.MoveFinalPosition();
            playerMovement.moveNum = 1;
            playerMovement.currentTile = 0;

            monsterMap.mushMoved = false;
            p_AttackSpawn.spawned = false;

            cameraMovement.fix = true;

            monsterMap.DeleteCannon();

            clearInfor.killedMonster++;
        }
        Destroy(gameObject);
    }

    void StartPattern() // 랜덤 패턴 선택
    {
        int randomPattern = Random.Range(0, 3); // 0 ~ 2 랜덤

        switch (randomPattern)
        {
            case 0:
                StartButtAttack();
                break;
            case 1:
                StartSpinAttack();
                break;
            case 2:
                StartUperAttack();
                break;
        }
    }

    private void StartButtAttack()
    {        
        StartCoroutine(ButtAttacks());
    }

    IEnumerator ButtAttacks()
    {
        for (int i = 0; i < 2; i++)
        {
            audioManager.M_ButtAudio();
            anim.SetTrigger("Butt");

            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                // Player 위치를 시작 방향으로
                Vector3 targetDirection = (player.transform.position - transform.position).normalized;
                Quaternion baseRotation = Quaternion.LookRotation(targetDirection);

                // Player 위치에서 -10, 0, +10도로 각도 조정하여 발사
                for (int j = -1; j <= 1; j++)
                {
                    Quaternion rotation = Quaternion.Euler(0, baseRotation.eulerAngles.y + (10 * j), 0);
                    Vector3 direction = rotation * Vector3.forward;
                    Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                    bullet.name = "ButtAttack";
                    bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd;

                    Destroy(bullet, 2.5f); // 2.5초 후 총알 제거
                }
            }

            yield return new WaitForSeconds(2f); // 다음 발사 대기
        }
    }



    private void StartSpinAttack()
    {
        anim.SetTrigger("Spin");
        StartCoroutine(SpinAttacks());
    }

    IEnumerator SpinAttacks()
    {
        audioManager.M_SpinAudio();
        for (int i = 0; i < 5; i++)
        {
            s_BulletNum = s_BulletNums[b_CurrentNumIndex]; // 다음 총알 개수 가져오기
            StartCoroutine(SpinBullet());
            s_CurrentNumIndex = (s_CurrentNumIndex + 1) % s_BulletNums.Length; // 다음 총알 인덱스 설정
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpinBullet() // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int i = 0; i < s_BulletNum; i++)
        {
            float angle = i * (360f / s_BulletNum); // 탄환의 각도 계산                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "BaseFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * s_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 3f); 

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartUperAttack()
    {
        StartCoroutine(UperAttack());
    }

    IEnumerator UperAttack()
    {
        for (int i = 0; i < 2; i++)
        {
            audioManager.M_UperAudio();
            anim.SetTrigger("Uper");

            GameObject player = GameObject.Find("Player");  // 플레이어 위치 찾기

            if (player != null)
            {
                // 플레이어를 향한 기본 방향 설정
                Vector3 targetDirection = (player.transform.position - transform.position).normalized;
                Quaternion baseRotation = Quaternion.LookRotation(targetDirection);

                for (int j = 0; j < u_AttackNum; j++)
                {
                    float angle = baseRotation.eulerAngles.y + Random.Range(-30, 31);  // 플레이어 위치를 기준으로 -30에서 +30도 사이의 각도
                    StartCoroutine(Uperbullet(angle));
                }
            }
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator Uperbullet(float angle)
    {
        for (int j = 0; j < u_BulletNum; j++)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;  // 수정된 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);  // 총알 위치 설정
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);  // 총알 생성
            bullet.name = "UperFireAttack";  // 총알 이름 변경
            bullet.GetComponent<Rigidbody>().velocity = direction * u_AttackSpd;  // 탄환 방향 설정

            Destroy(bullet, 2.5f);

            yield return new WaitForSeconds(0.2f);  // 다음 총알 발사까지 대기
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBullet"))
        {
            Bullet bulletComponent = collision.gameObject.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                audioManager.HitMonsterAudio();
                currentHealth -= bulletComponent.damage;
                hpBarScript.UpdateHP(currentHealth, maxHealth);
                anim.SetTrigger("Hit");
            }
            Destroy(collision.gameObject);
        } 
    }

}