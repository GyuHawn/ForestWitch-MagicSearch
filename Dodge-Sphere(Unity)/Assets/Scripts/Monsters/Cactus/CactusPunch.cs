using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPunch : MonoBehaviour
{
    public GameObject p_AttackPrefab; // 총알 프리팹
    public int p_AttackNum; // 발사 수
    public int p_AttackAngle; // 발사 앵글
    public int p_AttackSpd; // 발사 앵글

    void Start()
    {
        p_AttackNum = 25;
        p_AttackSpd = 8;

        Invoke("Attack", 0.8f);
    }

    void Attack()
    {
        for (int j = 0; j < p_AttackNum; j++)
        {
            p_AttackAngle = Random.Range(0, 361); // 탄환의 각도 계산                                             
            Vector3 direction = Quaternion.Euler(0, p_AttackAngle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(p_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "WaveFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 2.5f); // 2.5초 후 총알 제거
        }
        Destroy(gameObject);
    }
}
