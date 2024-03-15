using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    public GameObject pFloorCheck; // 플레이어 주변 바닥 체크
    public GameObject[] floors; // 전체 바닥
    public float moveSpd = 2f; // 바닥의 이동 속도

    void Update()
    {
        FloorUpMove();
    }

    void FloorUpMove() // 플레이어 주변 바닥 이동
    {
        foreach (GameObject floor in floors)
        {           
            if (floor.GetComponent<Collider>().bounds.Intersects(pFloorCheck.GetComponent<Collider>().bounds)) // 바닥이 pFloorCheck과 충돌하는지 확인
            {           
                float newYPosition = Mathf.Lerp(floor.transform.position.y, 0f, Time.deltaTime * moveSpd); // 바닥을 y0까지 서서히 이동
                floor.transform.position = new Vector3(floor.transform.position.x, newYPosition, floor.transform.position.z);
            }
        }
    }
}
