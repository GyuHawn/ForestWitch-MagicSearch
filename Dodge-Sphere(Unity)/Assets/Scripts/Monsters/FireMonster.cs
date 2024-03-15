using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    // 기본 스탯
    public int maxHealth;
    public int currentHealth;

    // 기본 패턴
    public GameObject b_AttackPrefab; // 총알 프리팹
    public float b_AttackSpd; // 총알 속도
    public int b_BulletNum; // 발사 수
    private int[] b_BulletNums; // 총알 개수 배열
    private int b_CurrentNumIndex; // 현재 총알 인덱스

    // 울기 패턴
    public GameObject c_AttackPrefab; // 총알 프리팹
    public float c_AttackSpd; // 총알 속도
    public int c_AttackNum; // 발사 수
    public int[] c_AttackAngles1; // 발사 각도
    public float[] c_AttackAngles2; // 발사 각도

    // 점프 패턴
    public GameObject j_AttackPrefab; // 총알 프리팹
    public float j_AttackSpd = 20f; // 총알 속도
    public int j_AttackNum = 10; // 발사 수
    public int j_BulletNum = 10; // 총알 수

    // 구르기 패턴
    public GameObject r_AttackPrefab; // 총알 프리팹
    public GameObject[] r_AttackPos;
    public float r_AttackSpd; // 총알 속도
    public int r_AttackNum; // 발사 수

    void Start()
    {
        b_AttackSpd = 10f;
        b_BulletNums = new int[] { 30, 29, 30, 29, 30 };

        c_AttackSpd = 10f;
        c_AttackNum = 4;
        c_AttackAngles1 = new int[] { 135, 180, 225 };
        c_AttackAngles2 = new float[] { 157.5f, 202.5f };

        j_AttackSpd = 15f;
        j_AttackNum = 15;
        j_BulletNum = 10;

        r_AttackSpd = 7f;
        r_AttackNum = 3;

        InvokeRepeating("StartPattern", 1f, 10f); // 랜덤 패턴 실행
    }

    void Update()
    {

    }

    void StartPattern() // 랜덤 패턴 선택
    {
        int randomPattern = Random.Range(0, 4); // 0 ~ 3 랜덤

        switch (randomPattern)
        {
            case 0:
                StartBaseAttacks();
                break;
            case 1:
                StartCryAttacks();
                break;
            case 2:
                StartJumpAttacks();
                break;
            case 3:
                StartRollAttack();
                break;
        }
    }

    private void StartBaseAttacks()
    {
       StartCoroutine(BaseAttacks());
    }

    IEnumerator BaseAttacks()
    {
        for (int i = 0; i < 5; i++)
        {
            b_BulletNum = b_BulletNums[b_CurrentNumIndex]; // 다음 총알 개수 가져오기
            StartCoroutine(Basebullet());
            b_CurrentNumIndex = (b_CurrentNumIndex + 1) % b_BulletNums.Length; // 다음 총알 인덱스 설정
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Basebullet() // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int i = 0; i < b_BulletNum; i++)
        {
            float angle = i * (360f / b_BulletNum); // 탄환의 각도 계산                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(b_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "BaseFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd; // 탄환 방향 설정
            
            Destroy(bullet, 4f); // 4초 후 총알 제거

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void StartCryAttacks()
    {
        StartCoroutine(CryAttacks());
    }

    IEnumerator CryAttacks() // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int i = 0; i < 4; i++) // 총 2번 발사
        {
            // c_AttackAngles1에서 3번 발사
            for (int j = 0; j < 3; j++)
            {
                float angle = c_AttackAngles1[j]; // 탄환의 각도 계산                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // 총알 위치 설정
                GameObject bullet = Instantiate(c_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                bullet.name = "CryFireAttack"; // 총알 이름 변경         
                bullet.GetComponent<Rigidbody>().velocity = direction * c_AttackSpd; // 탄환 방향 설정

                Destroy(bullet, 4f); // 4초 후 총알 제거
            }

            yield return new WaitForSeconds(0.75f); // 1초 대기

            // c_AttackAngles2에서 2번 발사
            for (int k = 0; k < 2; k++)
            {
                float angle = c_AttackAngles2[k]; // 탄환의 각도 계산                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // 총알 위치 설정
                GameObject bullet = Instantiate(c_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                bullet.name = "CryFireAttack"; // 총알 이름 변경         
                bullet.GetComponent<Rigidbody>().velocity = direction * c_AttackSpd; // 탄환 방향 설정

                Destroy(bullet, 4f); // 4초 후 총알 제거
            }
        }
    }

    public void StartJumpAttacks()
    {       
        StartCoroutine(JumpAttack());
    }

    IEnumerator JumpAttack()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(3);
            for (int j = 0; j < j_AttackNum; j++)
            {
                float angle = Random.Range(135, 225);
                StartCoroutine(Jumpbullet(angle));
            }
        }
    }

    IEnumerator Jumpbullet(float angle) // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int j = 0; j < j_BulletNum; j++)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(j_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "JumpFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * j_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 4f); // 4초 후 총알 제거

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartRollAttack()
    {
        StartCoroutine(RollAttack());
    }

    IEnumerator RollAttack()
    {
        Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // 각도에 따른 방향 계산
        Vector3 bulletPos = new Vector3(r_AttackPos[0].transform.position.x, 2f, r_AttackPos[0].transform.position.z); // 총알 위치 설정
        GameObject bullet = Instantiate(r_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
        bullet.name = "RollFireAttack"; // 총알 이름 변경         
        bullet.GetComponent<Rigidbody>().velocity = direction * r_AttackSpd; // 탄환 방향 설정

        Destroy(bullet, 8f);

        yield return new WaitForSeconds(1f);

        for (int j = 0; j < r_AttackNum - 1; j++)
        {
            direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // 각도에 따른 방향 계산
            bulletPos = new Vector3(r_AttackPos[j + 1].transform.position.x, 2f, r_AttackPos[j + 1].transform.position.z); // 총알 위치 설정
            bullet = Instantiate(r_AttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "RollFireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * r_AttackSpd; // 탄환 방향 설정

            Destroy(bullet, 8f);
        }
    }
}
