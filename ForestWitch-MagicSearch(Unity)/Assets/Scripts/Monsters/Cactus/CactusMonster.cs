using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CactusMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private MonsterGetMoney monsterGetMoney;
    private HpBarScript hpBarScript;
    private ClearInfor clearInfor;
    private Ability ability;
    private MapSetting mapSetting;
    private AudioManager audioManager;

    // 기본 스탯
    public int maxHealth;
    public int currentHealth;
    public int money;

    // 바운스 패턴
    public GameObject b_AttackPrefab; // 총알 프리팹
    public float b_AttackSpd; // 총알 속도
    public int b_BulletNum; // 발사 수
    private int[] b_BulletNums; // 총알 개수 배열
    private int b_CurrentNumIndex; // 현재 총알 인덱스

    // 웨이브 패턴
    public GameObject w_AttackPrefab; // 총알 프리팹
    public GameObject[] w_SpwanPos; // 총알 발사 위치
    public float w_AttackSpd; // 총알 속도
    public int w_AttackNum; // 발사 수
    public int w_AttackAngle; // 발사 각도

    // 펀치 패턴
    public GameObject p_BaseAttackPrefab; // 총알 프리팹
    public float p_AttackSpd; // 총알 속도  
    public int p_BulletNum; // 총알 수

    // 박치기 패턴
    public GameObject bu_AttackPrefab; // 총알 프리팹
    public float bu_AttackSpd; // 총알 속도
    public int bu_AttackNum; // 발사 수
    public int bu_AttackAngle; // 발사 각도

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
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 7 + (mapSetting.adventLevel * 3);
        currentHealth = maxHealth;
        money = 200;

        b_AttackSpd = 10f;
        b_BulletNums = new int[] { 30, 29, 30, 29, 30 };

        w_AttackSpd = 12f;
        w_AttackNum = 10;

        p_AttackSpd = 10f;        
        p_BulletNum = 3;

        bu_AttackSpd = 8f;
        bu_AttackNum = 3;
                
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
        yield return new WaitForSeconds(1f);

        hpBarScript.MoveToYStart(150, 0.1f);
        hpBarScript.ResetHealthBar(currentHealth, maxHealth);
        hpBarScript.healthBarFill.fillAmount = 1.0f;

        GetMoney();

        playerMovement.OnTile();
        playerMovement.MoveFinalPosition();
        playerMovement.moveNum = 1;
        playerMovement.currentTile = 0;
        playerMovement.bulletNum = 0;

        monsterMap.cactusMoved = false;
        p_AttackSpawn.spawned = false;

        cameraMovement.fix = true;

        monsterMap.DeleteObject();

        clearInfor.killedMonster++;

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
                StartBounceAttack();
                break;
            case 1:
                StartWaveAttack();
                break;
            case 2:
                StartPunchAttack();
                break;
            case 3:
                StartButtAttack();
                break;
        }
    }

    private void StartBounceAttack()
    {     
        anim.SetBool("Bounce", true);
        StartCoroutine(BounceAttacks());
    }

    IEnumerator BounceAttacks()
    {
        for (int i = 0; i < 5; i++)
        {
            audioManager.C_BounceAudio();
            b_BulletNum = b_BulletNums[b_CurrentNumIndex]; // 다음 총알 개수 가져오기
            BounceBullet();
            b_CurrentNumIndex = (b_CurrentNumIndex + 1) % b_BulletNums.Length; // 다음 총알 인덱스 설정
            yield return new WaitForSeconds(1f);
        }
        anim.SetBool("Bounce", false);
    }
    
    void BounceBullet() // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int i = 0; i < b_BulletNum; i++)
        {
            float angle = i * (360f / b_BulletNum); // 탄환의 각도 계산                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(b_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "BounceAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 2.5f); // 2.5초 후 총알 제거
        }
    }

    private void StartWaveAttack()
    {
        anim.SetTrigger("Wave");
        StartCoroutine(WaveAttacks());
    }

    IEnumerator WaveAttacks() // 양손에서 무작위 총알 발사
    {
        yield return new WaitForSeconds(0.3f);

        int num = 0;
        bool pos = true;
        for (int i = 0; i < 4; i++)
        {
            audioManager.C_WaveAudio();
            for (int j = 0; j < w_AttackNum; j++)
            {
                num = pos ? 1 : 0;
                w_AttackAngle = Random.Range(135, 226); // 탄환의 각도 계산                                             
                Vector3 direction = Quaternion.Euler(0, w_AttackAngle, 0) * Vector3.forward; // 각도에 따른 방향 계산
                Vector3 bulletPos = new Vector3(w_SpwanPos[num].transform.position.x, 2f, w_SpwanPos[1].transform.position.z); // 총알 위치 설정
                GameObject bullet = Instantiate(w_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                bullet.name = "WaveFireAttack"; // 총알 이름 변경         
                bullet.GetComponent<Rigidbody>().velocity = direction * w_AttackSpd; // 탄환 방향 설정

                Destroy(bullet, 2.5f); // 2.5초 후 총알 제거

                yield return new WaitForSeconds(0.11f);
            }
            pos = pos ? false : true;
        }
    }

    public void StartPunchAttack()
    {
        StartCoroutine(PunchBullet());
    }

    IEnumerator PunchBullet() // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int j = 0; j < p_BulletNum; j++)
        {
            anim.SetTrigger("Punch");
            yield return new WaitForSeconds(0.1f);
            audioManager.C_PunchAudio();

            Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // 앞 방향 
            Vector3 bulletPos = new Vector3(transform.position.x, 2.5f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(p_BaseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "PunchAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // 탄환 방향 설정

            yield return new WaitForSeconds(1f);
        }
    }

    public void StartButtAttack()
    {
        StartCoroutine(ButtAttack());
    }

    IEnumerator ButtAttack()
    {
        anim.SetTrigger("Butt");
        yield return new WaitForSeconds(0.4f);
        audioManager.C_ButtAudio();

        int bu_AttackAngle = Random.Range(160, 200);
        Vector3 direction = Quaternion.Euler(0, bu_AttackAngle, 0) * Vector3.forward; // 각도에 따른 방향 계산
        Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
        GameObject bullet = Instantiate(bu_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
        bullet.name = "ButtAttack"; // 총알 이름 변경         
        bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd; // 탄환 방향 설정
        yield return new WaitForSeconds(1.5f);
        if(bullet != null)
        {
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        yield return new WaitForSeconds(4f);
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
