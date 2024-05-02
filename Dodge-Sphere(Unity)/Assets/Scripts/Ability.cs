using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Ability : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Cannon cannon;

    public GameObject player;

    // 선택한 능력
    public int ability1Num;
    public int ability2Num;
    public int ability3Num;
    public int ability4Num;
    public int ability5Num;
    public int ability6Num;

    // 능력 
    public bool healing = true; // 한번만 발동하도록

    private void Awake()
    {
        cannon = GameObject.Find("Manager").GetComponent<Cannon>();
    }

    private void Start()
    {
        ability1Num = PlayerPrefs.GetInt("Ability1");
        ability2Num = PlayerPrefs.GetInt("Ability2");
        ability3Num = PlayerPrefs.GetInt("Ability3");
        ability4Num = PlayerPrefs.GetInt("Ability4");
        ability5Num = PlayerPrefs.GetInt("Ability5");
        ability6Num = PlayerPrefs.GetInt("Ability6");
    }

    void Update()
    {
        if(player != null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    public void GetPlayerMP() // (30%) 총알 획득시 확률적으로 총알 획득 (능력 1-1)
    {
        if (ability1Num == 1)
        {
            int num = Random.Range(0, 10);
            if (num < 3)
            {
                playerMovement.bulletNum++;
            }
        }
    }

    public void GetCannonReload() // (10%) 총알 획득시 확률적으로 모든 대포 총알 장전 (능력 1-2) 
    {
        if (ability1Num == 2)
        {
            int num = Random.Range(0, 10);
            if (num < 1)
            {
                GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");

                foreach (GameObject cannon in cannons)
                {
                    Cannon p_Cannon = cannon.GetComponent<Cannon>();
                    p_Cannon.currentBullet++;
                }
            }
        }
    }

    public void MPExtraAttack() // (30%) 총알 획득시 확률적으로 투사체 공격 (능력 2-1) 
    {
        if (ability2Num == 1)
        {
            int num = Random.Range(0, 10);
            if (num < 3)
            {
                GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            }
        }
    }

    public void CannonExtraAttack() // (50%) 공격 시 확률적으로 투사체 공격 (능력 2-2) 
    {
        if (ability2Num == 2)
        {
            int num = Random.Range(0, 10);
            if (num < 5)
            {
                GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            }
        }
    }

    public void GamblingCoin() // 코인 획득시 50%:50% 절반:2배 획득 (능력 3-1) 
    {
        if (ability3Num == 1)
        {
            int num = Random.Range(0, 10);
            if (num < 5)
            {

            }
        }
    }

    public void PlusCoin() // 코인 획득시 150%로 획득 (능력 3-2) 
    {
        if (ability3Num == 2)
        {
        }
    }

    public void DoubleAttack() // (30%) 공격시 확률적으로 2번 공격 (능력 4-1) 
    {
        if (ability4Num == 1)
        {
            int num = Random.Range(0, 10);
            if (num < 3)
            {
                cannon.AttackMonster();
            }
        }
    }

    public void PlusExtraAttack() // (80%) 공격시 확률적 투사체 공격 (능력 4-2) 
    {
        if (ability4Num == 2)
        {
            int num = Random.Range(0, 10);
            if (num < 8)
            {
                GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            }
        }
    }

    public void Healing() // 게임당 1번 체력이 25% 이하로 줄어들면 50%회복 (능력 5-1) 
    {
        if (ability5Num == 1)
        {
            if (playerMovement.currentHealth <= (playerMovement.maxHealth / 4) && healing)
            {
                healing = false;
                playerMovement.currentHealth = playerMovement.currentHealth + (playerMovement.maxHealth / 2);
            }
        }
    }

    public void Avoid() // (40%) 피격시 확률적 무시 (능력 5-2) 
    {
        if (ability5Num == 2)
        {
            int num = Random.Range(0, 10);
            if (num < 4)
            {

            }
        }
    }

    public void HitCannonReload() // 피격시 모든 대포 총알 1증가 (능력 6-1) 
    {
        if (ability6Num == 1)
        {
            GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");

            foreach (GameObject cannon in cannons)
            {
                Cannon p_Cannon = cannon.GetComponent<Cannon>();
                p_Cannon.currentBullet++;
            }
        }
    }

    public void HitExtraAttack() // 피격시 2개 투사체 발사 (능력 6-2) 
    {
        if (ability6Num == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            }
        }
    }
}
