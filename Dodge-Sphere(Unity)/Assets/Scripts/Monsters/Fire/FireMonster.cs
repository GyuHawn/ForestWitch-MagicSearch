using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private MonsterGetMoney monsterGetMoney;
    private MapSetting mapSetting;
    private HpBarScript hpBarScript;
    private ClearInfor clearInfor;
    private Ability ability;
    private MapConvert mapConvert;
    private AudioManager audioManager;

    // 기본 스탯
    public int maxHealth;
    public int currentHealth;
    public int money;

    // 기본 패턴
    public GameObject b_AttackPrefab; // 총알 프리팹
    public float b_AttackSpd; // 총알 속도
    public int b_BulletNum; // 발사 수
    private int[] b_BulletNums; // 총알 개수 배열
    private int b_CurrentNumIndex; // 현재 총알 인덱스

    // 울기 패턴
    public GameObject c_AttackPrefab; // 총알 프리팹
    public float c_AttackSpd; // 총알 속도
    public int c_AttackNum; // 발사 수
    public int[] c_AttackAngles1; // 발사 각도
    public float[] c_AttackAngles2; // 발사 각도

    // 점프 패턴
    public GameObject j_AttackPrefab; // 총알 프리팹
    public float j_AttackSpd = 20f; // 총알 속도
    public int j_AttackNum = 10; // 발사 수
    public int j_BulletNum = 10; // 총알 수

    // 구르기 패턴
    public GameObject r_AttackPrefab; // 총알 프리팹
    public GameObject[] r_AttackPos;
    public float r_AttackSpd; // 총알 속도
    public int r_AttackNum; // 발사 수

    public GameObject hitEffectPos; // 이펙트 위치
    public GameObject hitEffect; // 피격 이펙트

    private Animator anim;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        p_AttackSpawn = GameObject.Find("Manager").GetComponent<P_AttackSpawn>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        monsterGetMoney = GameObject.Find("Manager").GetComponent<MonsterGetMoney>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
        hpBarScript = GameObject.Find("MosterHP").GetComponent<HpBarScript>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        ability = GameObject.Find("Manager").GetComponent<Ability>();
        mapConvert = GameObject.Find("Manager").GetComponent<MapConvert>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Spawn");

        maxHealth = 12 + (mapSetting.adventLevel * 3);
        currentHealth = maxHealth;
        money = 300;

        b_AttackSpd = 10f;
        b_BulletNums = new int[] { 30, 29, 30, 29, 30 };

        c_AttackSpd = 10f;
        c_AttackAngles1 = new int[] { 135, 180, 225 };
        c_AttackAngles2 = new float[] { 157.5f, 202.5f };

        j_AttackSpd = 10f;
        j_AttackNum = 10;
        j_BulletNum = 6;

        r_AttackSpd = 8f;
        r_AttackNum = 3;
        
        //InvokeRepeating("StartRollAttack", 1f, 10f); // 패턴 확인용
        InvokeRepeating("StartPattern", 3f, 7f); // 랜덤 패턴 실행

        hpBarScript.MoveToYStart(10, 0.5f);
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

        hpBarScript.MoveToYStart(150, 0.1f);
        hpBarScript.ResetHealthBar();

        GetMoney();

        mapConvert.ConvertLoading(mapConvert.stageLoading, 2f, 5); // 보스 클리어시 스테이지 전환 이미지 출력

        playerMovement.nextStage = true; // 스테이지 변환 중
        playerMovement.OnTile();
        playerMovement.moveNum = 1;
        playerMovement.currentTile = 0;
        playerMovement.bulletNum = 0;
        playerMovement.PostionReset(); // 플레이어 위치 초기화

        monsterMap.fireMoved = false;
        p_AttackSpawn.spawned = false;

        cameraMovement.fix = true;

        monsterMap.DeleteCannon();

        clearInfor.killedBoss++;

        mapSetting.stage++; // 스테이지 보스 클리어시 스테이지로 1증가
        mapSetting.MapReset(); // 보스 클리어시 맵 초기화
        mapSetting.StageMapSetting(); // 맵 셋팅

        Destroy(gameObject);
    }
    void GetMoney()
    {
        // 능력 3-1이 활성화
        if (ability.ability3Num == 1)
        {
            money = ability.GamblingCoin(money); // 능력 3-1에 따라 코인 획득을 조절
        }
        // 능력 3-2가 활성화
        else if (ability.ability3Num == 2)
        {
            money = ability.PlusCoin(money); // 능력 3-2에 따라 코인 획득을 조절
        }

        monsterGetMoney.getMoney = money;
        monsterGetMoney.PickUpMoney();
    }

    void StartPattern() // 랜덤 패턴 선택
    {
        int randomPattern = Random.Range(0, 4); // 0 ~ 3 랜덤

        switch (randomPattern)
        {
            case 0:
                StartBaseAttack();
                break;
            case 1:
                StartCryAttack();
                break;
            case 2:
                StartJumpAttack();
                break;
            case 3:
                StartRollAttack();
                break;
        }
    }

    private void StartBaseAttack()
    {
        anim.SetTrigger("Base");
        StartCoroutine(BaseAttacks());
    }

    IEnumerator BaseAttacks()
    {
        audioManager.F_BaseAudio();
        for (int i = 0; i < 5; i++)
        {
            b_BulletNum = b_BulletNums[b_CurrentNumIndex]; // 다음 총알 개수 가져오기
            StartCoroutine(BaseBullet());
            b_CurrentNumIndex = (b_CurrentNumIndex + 1) % b_BulletNums.Length; // 다음 총알 인덱스 설정
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator BaseBullet() // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int i = 0; i < b_BulletNum; i++)
        {
            float angle = i * (360f / b_BulletNum); // 탄환의 각도 계산                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(b_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "BaseFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 2.5f);

            yield return new WaitForSeconds(0.05f);
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
            audioManager.F_CryAudio();
            for (int j = 0; j < 3; j++)
            {
                float angle = c_AttackAngles1[j]; // 탄환의 각도 계산                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // 총알 위치 설정
                GameObject bullet = Instantiate(c_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                bullet.name = "CryFireAttack"; // 총알 이름 변경         
                bullet.GetComponent<Rigidbody>().velocity = direction * c_AttackSpd; // 탄환 방향 설정

                Destroy(bullet, 2.5f); // 4초 후 총알 제거
            }

            yield return new WaitForSeconds(0.75f); // 1초 대기

            for (int k = 0; k < 2; k++)
            {
                float angle = c_AttackAngles2[k]; // 탄환의 각도 계산                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // 총알 위치 설정
                GameObject bullet = Instantiate(c_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                bullet.name = "CryFireAttack"; // 총알 이름 변경         
                bullet.GetComponent<Rigidbody>().velocity = direction * c_AttackSpd; // 탄환 방향 설정

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
        audioManager.F_JumpAudio();
        anim.SetTrigger("Jump");
        for (int j = 0; j < j_AttackNum; j++)
        {
            float angle = Random.Range(135, 225);
            StartCoroutine(Jumpbullet(angle));
        }

        yield return new WaitForSeconds(3);
        audioManager.F_JumpAudio();
        anim.SetTrigger("Jump");
        for (int j = 0; j < j_AttackNum; j++)
        {
            float angle = Random.Range(135, 225);
            StartCoroutine(Jumpbullet(angle));
        }
    }

    IEnumerator Jumpbullet(float angle) // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int j = 0; j < j_BulletNum; j++)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(j_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "JumpFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * j_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 2.5f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartRollAttack()
    {     
        StartCoroutine(RollAttack());
    }

    IEnumerator RollAttack()
    {
        for (int i = 0; i < 2; i++)
        {
            audioManager.F_RollAudio();
            anim.SetTrigger("Roll");

            Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(r_AttackPos[0].transform.position.x, 2f, r_AttackPos[0].transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(r_AttackPrefab, bulletPos, Quaternion.Euler(90,0,0)); // 총알 생성
            bullet.name = "RollFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * r_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 3f);

            yield return new WaitForSeconds(.8f);

            for(int j = 0 ; j < 2; j++)
            {
                for (int k = 0; k < r_AttackNum - 1; k++)
                {
                    direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // 각도에 따른 방향 계산
                    bulletPos = new Vector3(r_AttackPos[j + 1].transform.position.x, 2f, r_AttackPos[j + 1].transform.position.z); // 총알 위치 설정
                    bullet = Instantiate(r_AttackPrefab, bulletPos, Quaternion.Euler(90, 0, 0)); // 총알 생성
                    bullet.name = "RollFireAttack"; // 총알 이름 변경         
                    bullet.GetComponent<Rigidbody>().velocity = direction * r_AttackSpd; // 탄환 방향 설정

                    Destroy(bullet, 3f);
                }
            }
            yield return new WaitForSeconds(1.5f);
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
                StartCoroutine(HitEffect());
                currentHealth -= bulletComponent.damage;
                hpBarScript.UpdateHP(currentHealth, maxHealth);
                anim.SetTrigger("Hit");
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("ExtraAttack"))
        {
            ExtraAttack attack = collision.gameObject.GetComponent<ExtraAttack>();
            if (attack != null)
            {
                audioManager.HitMonsterAudio();
                StartCoroutine(HitEffect());
                currentHealth -= attack.damage;
                hpBarScript.UpdateHP(currentHealth, maxHealth);
                anim.SetTrigger("Hit");
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator HitEffect()
    {
        GameObject effect = Instantiate(hitEffect, hitEffectPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }
} 
