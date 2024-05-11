using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public int damage;

    // ������ ����
    public bool dagger;
    public bool sword;

    public GameObject player;

    private void Start()
    {
        FindPlayer(); // �÷��̾� ã��
    }

    void FindPlayer()
    {
        if (player == null) // �÷��̾� ã��
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        void Update()
        {
            if (playerMovement.pick)
            {
                int num = Random.Range(0, 1);
                if (num == 0)
                {
                    damage = damage * 2;
                }
            }

            ItemSetting();
        }

        void ItemSetting()
        {
            // �ܰ� ������ ȹ��� ������ 1����
            if (playerMovement.dagger && !dagger)
            {
                dagger = true;

                damage++;
            }
            // �ܰ� ������ ȹ��� ������ 1����
            if (playerMovement.sword && !sword)
            {
                sword = true;

                damage += 2;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            /*if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }*/
        }
    }
}