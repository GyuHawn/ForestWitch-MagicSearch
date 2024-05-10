using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Ability : MonoBehaviour
{
    private PlayerMovement playerMovement;

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

    private void Start()
    {
        /*ability1Num = PlayerPrefs.GetInt("Ability1");
        ability2Num = PlayerPrefs.GetInt("Ability2");
        ability3Num = PlayerPrefs.GetInt("Ability3");
        ability4Num = PlayerPrefs.GetInt("Ability4");
        ability5Num = PlayerPrefs.GetInt("Ability5");
        ability6Num = PlayerPrefs.GetInt("Ability6");*/
        GPGSBinder.Inst.LoadCloud("Ability1", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability1Num = loadedAbility1;
            }
            else
            {
                ability1Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability2", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability2Num = loadedAbility1;
            }
            else
            {
                ability2Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability3", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability3Num = loadedAbility1;
            }
            else
            {
                ability3Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability4", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability4Num = loadedAbility1;
            }
            else
            {
                ability4Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability5", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability5Num = loadedAbility1;
            }
            else
            {
                ability5Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability6", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability6Num = loadedAbility1;
            }
            else
            {
                ability6Num = 0;
            }
        });
    }

    void Update()
    {
        FindPlayer(); // 플레이어 찾기
    }

    void FindPlayer()
    {
        if (player == null) // 플레이어 찾기
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    public void GetPlayerMP() // (30%) 총알 획득시 확률적으로 총알 획득 (능력 1-1)
    {
        int num = Random.Range(0, 10);
        if (num < 3)
        {
            playerMovement.bulletNum++;
        }
    }

    public void GetCannonReload() // (10%) 총알 획득시 확률적으로 모든 대포 총알 1 장전 (능력 1-2) 
    {
        Debug.Log("1_2");
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

    public void MPExtraAttack() // (30%) 총알 획득시 확률적으로 투사체 공격 (능력 2-1) 
    {
        int num = Random.Range(0, 10);
        if (num < 3)
        {
            GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            playerMovement.extraAttacks.Add(attack);
        }
    }

    public void CannonExtraAttack() // (50%) 공격 시 확률적으로 투사체 공격 (능력 2-2) 
    {
        Debug.Log("2_2");
        int num = Random.Range(0, 10);
        if (num < 5)
        {
            GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            playerMovement.extraAttacks.Add(attack);
        }
    }

    public int GamblingCoin(int money) // 코인 획득시 50%:50% 절반:2배 획득 (능력 3-1) 
    {
        int num = Random.Range(0, 10);
        if (num < 5)
        {
            return money * 2;
        }
        else
        {
            return (int)(money * 0.5f);
        }
    }

    public int PlusCoin(int money) // 코인 획득시 150%로 획득 (능력 3-2) 
    {
        Debug.Log("3_2");
        return (int)(money * 1.5f);
    }

    // (30%) 공격시 확률적으로 한번더 공격 (능력 4-1)  - Cannon 스크립트에 직접 적용
                             
    public void PlusExtraAttack() // (80%) 공격시 확률적 투사체 공격 (능력 4-2) 
    {
        Debug.Log("4_2");
        int num = Random.Range(0, 10);
        if (num < 8)
        {
            GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            playerMovement.extraAttacks.Add(attack);
        }
    }

    // 게임당 1번 체력이 25% 이하로 줄어들면 50%회복 (능력 5-1)  - PlayerMovement 스크립트에 직접 적용
    // 피격시 확률적 무시 (능력 5-2)  - PlayerMovement 스크립트에 직접 적용

    public void HitCannonReload() // 피격시 모든 대포 총알 1증가 (능력 6-1) 
    {
        GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");

        foreach (GameObject cannon in cannons)
        {
            Cannon p_Cannon = cannon.GetComponent<Cannon>();
            p_Cannon.currentBullet++;
        }
    }

    public void HitExtraAttack() // 피격시 2개 투사체 발사 (능력 6-2) 
    {
        Debug.Log("6_2");
        for (int i = 0; i < 2; i++)
        {
            GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            playerMovement.extraAttacks.Add(attack);
        }
    }
}
