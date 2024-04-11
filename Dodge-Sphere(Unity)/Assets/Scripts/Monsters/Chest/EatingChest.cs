using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingChest : MonoBehaviour
{
    public GameObject playerBullet;
    public bool findBullet;
    public float speed = 300f;

    void Update()
    {
        if (!findBullet)
        {
            if (!IsInvoking("FindBullet"))
            {
                StartCoroutine(FindBullet());
            }
        }
        else
        {
            if (playerBullet == null)
            {
                findBullet = false;
            }
            else
            {
                EatingBullet();
            }
        }
    }

    IEnumerator FindBullet()
    {
        findBullet = false;
        while (true)
        {
            playerBullet = GameObject.FindWithTag("P_Attack");
            if (playerBullet != null)
            {
                findBullet = true; 
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(2);
            }
        }
    }


    void EatingBullet()
    {
        if (playerBullet != null)
        {
            float step = speed * Time.deltaTime;
            playerBullet.transform.position = Vector3.MoveTowards(playerBullet.transform.position, transform.position, step);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChestMonster monster = GameObject.Find("ChestMonster").GetComponent<ChestMonster>();
            monster.Eating = false;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("P_Attack"))
        {
            findBullet = false;
            Destroy(collision.gameObject);
        }
    }
}
