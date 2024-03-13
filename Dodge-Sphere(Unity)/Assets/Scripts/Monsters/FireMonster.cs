using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public float bulletSpeed = 20f; // 총알 속도
    public int baseAttackNum = 10; // 발사할 탄환의 수

    private int[] baseAttackNums = { 20, 19, 20, 19, 20 }; // 총알 개수 배열
    private int currentBaseNumIndex = 0; // 현재 총알 인덱스

    void Start()
    {
        bulletSpeed = 10f;
        InvokeRepeating("StartBaseAttacks", 1f, 8f); // 1초마다 실행
    }

    void Update()
    {

    }

    private void StartBaseAttacks()
    {
       StartCoroutine(BaseAttacks());
    }

    IEnumerator BaseAttacks()
    {
        for (int i = 0; i < 5; i++)
        {
            baseAttackNum = baseAttackNums[currentBaseNumIndex]; // 다음 총알 개수 가져오기
            Basebullet();
            currentBaseNumIndex = (currentBaseNumIndex + 1) % baseAttackNums.Length; // 다음 총알 인덱스 설정
            yield return new WaitForSeconds(1f);
        }
    }

    public void Basebullet() // 탄환을 몬스터 주위 원형으로 발사
    {
        for (int i = 0; i < baseAttackNum; i++)
        {
            float angle = i * (360f / baseAttackNum); // 탄환의 각도 계산                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // 각도에 따른 방향 계산
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // 총알 위치 설정
            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "FireAttack"; // 총알 이름 변경         
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed; // 탄환 방향 설정

            Destroy(bullet, 4f); // 4초 후 총알 제거
        }
    }

    public void CryAttack()
    {

    }

    public void JumpAttack()
    {

    }

    public void RollAttack()
    {

    }
}
