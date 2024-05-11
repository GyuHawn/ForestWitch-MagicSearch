using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    public GameObject pFloorCheck; // �÷��̾� �ֺ� �ٴ� üũ
    public GameObject[] floors; // ��ü �ٴ�
    public float moveSpd; // �ٴ��� �̵� �ӵ�

    void Update()
    {
        if (pFloorCheck == null)
        {
            pFloorCheck = GameObject.Find("Check");
        }
        moveSpd = 3f;
        FloorUpMove();
    }

    void FloorUpMove() // �÷��̾� �ֺ� �ٴ� �̵�
    {
        if (pFloorCheck != null)
        {
            foreach (GameObject floor in floors)
            {
                if (floor.GetComponent<Collider>().bounds.Intersects(pFloorCheck.GetComponent<Collider>().bounds)) // �ٴ��� pFloorCheck�� �浹�ϴ��� Ȯ��
                {
                    float newYPosition = Mathf.Lerp(floor.transform.position.y, 0f, Time.deltaTime * moveSpd); // �ٴ��� y0���� ������ �̵�
                    floor.transform.position = new Vector3(floor.transform.position.x, newYPosition, floor.transform.position.z);
                }
            }
        }
    }
}