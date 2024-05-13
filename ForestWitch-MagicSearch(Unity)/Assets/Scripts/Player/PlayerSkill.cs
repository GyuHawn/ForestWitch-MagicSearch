using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameDatas gameDatas;
    
    public GameObject player; // 플레이어

    public int playerNum; // 캐릭터 확인
    public GameObject skillUI; // 스킬 UI
    public GameObject[] playerSkills; // 캐릭터 스킬
    public Image skillCoolTime; // 쿨타임 UI

    public float coolTime; // 현재 쿨타임
    public float purificatTime; // 1번 스킬 쿨타임
    public float reLoadTime; // 2번 스킬 쿨타임

    public GameObject purificatEffect;

    private void Awake()
    {
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    void Start()
    {

      /*  gameDatas.LoadFieldData<int>("playerNum", value => {
            playerNum = value;
        }, () => {
            playerNum = 1;
        });*/

        // 캐릭터에 따른 쿨타임 적용
        if (playerNum == 1)
        {
            purificatTime = 25;
        }
        else if (playerNum == 2)
        {
            reLoadTime = 30;
        }
    }

    void Update()
    {
        FindPlayer(); // 플레이어 찾기
        UpdateCoolTime(); // 쿨타임 업데이트
        UpdateSkillUI(); // 맵에 따른 UI 활성화
    }
    
    void FindPlayer()
    {
        if (player == null) // 플레이어 찾기
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void UpdateCoolTime()  // 쿨타임 업데이트
    {
        if (coolTime > 0) // 쿨타임 중 시간 줄이기
        {
            coolTime -= Time.deltaTime;
            skillCoolTime.gameObject.SetActive(true);

            // 쿨타임에 따른 fillAmount 업데이트
            skillCoolTime.fillAmount = coolTime / (playerNum == 1 ? purificatTime : reLoadTime);
        }
        else
        {
            skillCoolTime.gameObject.SetActive(false);
            skillCoolTime.fillAmount = 0; // 쿨타임이 끝날때 fillAmount를 0으로
        }
    }

    void UpdateSkillUI() // 맵에 따른 UI 활성화
    {
        if (playerMovement.game) // 몬스터 맵일때 활성화
        {
            skillUI.SetActive(true);

            // 플레이어 따라 스킬 활성화
            playerSkills[0].SetActive(playerNum == 1);
            playerSkills[1].SetActive(playerNum == 2);
        }
        else
        {
            skillUI.SetActive(false);
        }
    }

    public void StartCooldown(float time) // 쿨타임 설정
    {
        coolTime = time;
        skillCoolTime.fillAmount = 1;
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
                GameObject effect = Instantiate(purificatEffect, bullet.transform.position, Quaternion.identity);           
                Destroy(effect, 1f);
                Destroy(bullet);
            }
            
            // 쿨타임 설정
            StartCooldown(purificatTime);
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

            StartCooldown(reLoadTime);
        }
    } 
}
