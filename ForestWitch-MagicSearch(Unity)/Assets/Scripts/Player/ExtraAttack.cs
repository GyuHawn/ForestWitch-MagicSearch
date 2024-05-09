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
        if (monsters.Length > 0 && monsters[currentTargetIndex] != null)
        {
            Vector3 monsterPos = new Vector3(monsters[currentTargetIndex].transform.position.x, 2f, monsters[currentTargetIndex].transform.position.z);
            Vector3 newPosition = Vector3.MoveTowards(transform.position, monsterPos, spd * Time.deltaTime);
            transform.position = newPosition;

            Vector3 directionToFace = monsterPos - transform.position;
            if (directionToFace != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToFace);
                targetRotation *= Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, spd * Time.deltaTime);
            }

            // 현재 목표가 사라졌다면 다음 몬스터를 목표로 설정
            if (monsters[currentTargetIndex] == null || !monsters[currentTargetIndex].activeInHierarchy)
            {
                currentTargetIndex = (currentTargetIndex + 1) % monsters.Length;
            }
        }
    }
}
