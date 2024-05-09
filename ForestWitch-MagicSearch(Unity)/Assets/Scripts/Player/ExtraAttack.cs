using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraAttack : MonoBehaviour
{
    public int damage; // 데미지
    public float spd; // 스피드

    public GameObject[] monsters; // 몬스터
    private int currentTargetIndex = 0; // 현재 타겟팅 중인 몬스터

    void Start()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster"); // 모든 몬스터를 찾기
    }

    void Update()
    {
        if (monsters.Length > 0 && monsters[currentTargetIndex] != null) // 몬스터가 있고 몬스터를 타겟 중 일때
        {
            Vector3 monsterPos = new Vector3(monsters[currentTargetIndex].transform.position.x, 2f, monsters[currentTargetIndex].transform.position.z); // 타겟한 몬스터의 위치에서 y값을 2로 지정
            Vector3 newPosition = Vector3.MoveTowards(transform.position, monsterPos, spd * Time.deltaTime); // 현재위치에서 목표지점까지 spd의 속도로 이동
            transform.position = newPosition; // 현재위치 업데이트 

            Vector3 directionToMonster = monsterPos - transform.position; // 몬스터 거리 계산
            if (directionToMonster != Vector3.zero) // 몬스터와 거리가 0이 아닐때
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToMonster); // 몬스터를 바라보는 회전값 계산
                targetRotation *= Quaternion.Euler(0, 90, 0); // 회전값을 추가로 y축 90도 적용
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, spd * Time.deltaTime); // 현재에서 목표까지 부드럽게 회전
            }

            // 현재 목표가 사라졌다면 다음 몬스터를 목표로 설정
            if (monsters[currentTargetIndex] == null || !monsters[currentTargetIndex].activeInHierarchy)
            {
                currentTargetIndex = (currentTargetIndex + 1) % monsters.Length;
            }
        }
    }
}
