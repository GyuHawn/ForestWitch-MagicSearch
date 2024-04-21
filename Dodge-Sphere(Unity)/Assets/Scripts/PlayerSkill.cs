using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject player;

    public GameObject skill;
    public int playerNum;
    public GameObject[] playerSkills;
    public Image skillCoolTime;

    public float coolTime;
    public float reLoadTime;
    public float purificatTime;

    void Start()
    {
        playerNum = PlayerPrefs.GetInt("Player");

        if (playerNum == 1)
        {
            purificatTime = 30;
        }
        else if (playerNum == 2)
        {
            reLoadTime = 30;
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        if (coolTime > 0)
        {
            skillCoolTime.gameObject.SetActive(true);
            coolTime -= Time.deltaTime;

            // 쿨타임에 따른 fillAmount 업데이트
            if (playerNum == 1)
            {
                skillCoolTime.fillAmount = coolTime / purificatTime;
            }
            else if (playerNum == 2)
            {
                skillCoolTime.fillAmount = coolTime / reLoadTime;
            }
        }
        else
        {
            skillCoolTime.gameObject.SetActive(false);
            skillCoolTime.fillAmount = 0; // 쿨타임이 끝날때 fillAmount를 0으로
        }

        if (playerMovement.game) // 몬스터 맵일때 활성화
        {
            skill.SetActive(true);

            // 플레이어 따라 스킬 활성화
            if (playerNum == 1)
            {
                playerSkills[0].SetActive(true);
            }
            else if (playerNum == 2)
            {
                playerSkills[1].SetActive(true);
            }
        }
        else
        {
            skill.SetActive(false);
        }
    }

    public void Purification() // 모든 총알 제거
    {
        if (coolTime <= 0)
        {
            // 모든 총알 찾기
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

            // 찾은 총알 제거
            foreach (GameObject bullet in bullets)
            {
                Destroy(bullet);
            }
            
            // 쿨타임 설정
            coolTime = purificatTime;
            skillCoolTime.fillAmount = 1;
        }
    }
    public void Reload() // 모든 대포 장전
    {
        if(coolTime <= 0)
        {
            // 전체 대포 찾기
            GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");

            // 전체 대포 장전
            foreach (GameObject cannon in cannons)
            {
                Cannon p_Cannon = cannon.GetComponent<Cannon>();
                p_Cannon.currentBullet = p_Cannon.maxBullet;
            }

            coolTime = reLoadTime;
            skillCoolTime.fillAmount = 1;
        }
    }
}
