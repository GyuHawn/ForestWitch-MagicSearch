using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public int damage;

    // 아이템 관련
    public bool dagger;
    public bool sword;

    public GameObject player;
    
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();

            if (playerMovement.pick)
            {
                int num = Random.Range(0, 1);
                if (num == 0)
                {
                    damage = damage * 2;
                }
            }
        }

        ItemSetting();
    }

    void ItemSetting()
    {
        // 단검 아이템 획득시 데미지 1증가
        if (playerMovement.dagger && !dagger)
        {
            dagger = true;

            damage++;
        }
        // 단검 아이템 획득시 데미지 1증가
        if (playerMovement.sword && !sword)
        {
            sword = true;

            damage += 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }*/
    }
}
