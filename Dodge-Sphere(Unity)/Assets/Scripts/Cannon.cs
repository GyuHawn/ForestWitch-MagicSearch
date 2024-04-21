using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cannon : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioManager audioManager;

    private GameObject player; // 플레이어

    public bool ready; // 장전 여부
    public int maxBullet; // 최대 총알
    public int currentBullet; // 현재 총알
    public GameObject loadBullet; // 총알 장전 위치
    public GameObject shotPos; // 총알 발사 위치
    public float reloadDelay = 1f; // 재장전 딜레이
    private bool reloading = false; // 총알 재장전 중 여부
    public GameObject shotBullet; // 발사 준비된 총알 
    public GameObject shotBulletPrefab; // 발사할 총알 프리팹
    public TMP_Text currentBulletText; // 현재 총알 상태 텍스트
    public float bulletSpd; // 총알의 속도

    // 아이템 관련
    public bool book;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        ready = false;
    }

    void ItemSetting()
    {
        // 화살 아이템 획득시 속도업
        if (!playerMovement.arrow) 
        {
            bulletSpd = 30f;
        }
        else
        {
            bulletSpd = 40f;
        }

        // 책 아이템 획득시 최대 총알수 1감소
        if (playerMovement.bow && !book) 
        {
            book = true;

            if (maxBullet > 1)
            {
                maxBullet--;
            }
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        // 아이템 관련
        ItemSetting();

        currentBulletText.text = currentBullet + " / " + maxBullet;

        if (currentBullet < maxBullet)
        {
            ready = false;
        }
        else if (currentBullet >= maxBullet)
        {
            ready = true;
        }

        if (!reloading && !ready)
        {
            LoadBullet(); // 총알 넣기
        }
        else if (ready)
        {
            ShotBullet(); // 총알 생성
        }

        if (shotBullet != null)
        {
            AttackMonster();
        }
    }

    void LoadBullet() // 총알 넣기
    {
        if (player != null && player.GetComponent<Collider>().bounds.Intersects(loadBullet.GetComponent<Collider>().bounds))
        {
            if (playerMovement.bulletNum > 0) // 총알이 있을때
            {
                reloading = true; // 재장전 시작
                StartCoroutine(ReloadDelayCoroutine());
            }
        }
    }

    IEnumerator ReloadDelayCoroutine()
    {
        yield return new WaitForSeconds(reloadDelay); // 재장전 딜레이
        reloading = false; // 재장전 종료

        if (playerMovement.bulletNum >= maxBullet)
        {
            if (currentBullet > 0)
            {
                playerMovement.bulletNum -= (maxBullet + currentBullet);
                currentBullet = maxBullet;
            }
            else
            {
                playerMovement.bulletNum -= maxBullet;
                currentBullet = maxBullet;
            }
        }
        else if (playerMovement.bulletNum < maxBullet)
        {
            if (currentBullet > 0)
            {
                currentBullet = playerMovement.bulletNum + currentBullet;
                playerMovement.bulletNum = 0;
            }
            else
            {
                currentBullet = playerMovement.bulletNum;
                playerMovement.bulletNum = 0;
            }
        }
    }

    void ShotBullet() // 총알 생성
    {
        ready = false;
        currentBullet = 0;
        audioManager.CannonAudio();
        shotBullet = Instantiate(shotBulletPrefab, shotPos.transform.position, Quaternion.identity);
    }

    void AttackMonster()
    {
        // 모든 몬스터를 찾습니다.
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (monsters.Length > 0 && shotBullet != null)
        {
            
            foreach (var monster in monsters)
            {
                Vector3 shotBulletPosition = shotBullet.transform.position;
                Vector3 fixedYVector = new Vector3(0f, 0.5f, 0f);

                Vector3 monsterDirection = (monster.transform.position - shotBulletPosition).normalized;

                Vector3 direction = monsterDirection + fixedYVector;

                direction.Normalize();

                Vector3 force = direction * bulletSpd;

                Rigidbody bulletRb = shotBullet.GetComponent<Rigidbody>();
                bulletRb.velocity = force;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BreakBullet"))
        {
            Destroy(gameObject);
        }
    }
}
