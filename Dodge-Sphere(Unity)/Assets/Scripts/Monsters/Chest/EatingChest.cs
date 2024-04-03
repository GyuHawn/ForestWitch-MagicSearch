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
            if (!IsInvoking("FindBullet")) // FindBullet 코루틴이 이미 시작되지 않았다면
            {
                StartCoroutine(FindBullet());
            }
        }
        else
        {
            if (playerBullet == null)
            {
                // playerBullet이 파괴되었거나 더 이상 존재하지 않는 경우
                findBullet = false; // findBullet을 false로 설정하여 새로운 playerBullet을 찾을 수 있도록 함
            }
            else
            {
                EatingBullet(); // findBullet이 true이고 playerBullet이 존재할 때 계속해서 EatingBullet을 호출
            }
        }
    }

    IEnumerator FindBullet()
    {
        findBullet = false; // 코루틴이 시작될 때 findBullet을 초기화
        while (true)
        {
            playerBullet = GameObject.FindWithTag("P_Attack");
            if (playerBullet != null)
            {
                findBullet = true; // 플레이어의 공격을 찾았다면 findBullet을 true로 설정
                yield break; // 코루틴 종료
            }
            else
            {
                yield return new WaitForSeconds(2); // 플레이어의 공격을 찾지 못했다면 2초 후 다시 시도
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
            monster.e_Eating = false;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("P_Attack"))
        {
            findBullet = false;
            Destroy(collision.gameObject);
        }
    }
}
