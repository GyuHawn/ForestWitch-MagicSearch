using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cannon : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameObject player; // 플레이어

    public bool ready; // 장전 여부
    public int maxBullet; // 최대 총알
    public int currentBullet; // 현재 총알
    public GameObject loadBullet; // 총알 장전 위치
    public GameObject shotPos; // 총알 발사 위치
    public float reloadDelay = 1f; // 재장전 딜레이
    private bool reloading = false; // 총알 재장전 중 여부

    public GameObject shotBulletPrefab; // 발사할 총알 프리팹

    public TMP_Text currentBulletText; // 현재 총알 상태 텍스트


    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        ready = false;
    }

    void Update()
    {
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
        Instantiate(shotBulletPrefab, shotPos.transform.position, Quaternion.identity);
    }
}
