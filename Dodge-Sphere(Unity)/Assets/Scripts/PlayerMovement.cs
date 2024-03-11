using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int moveNum; // 플레이어 이동 유무(일단 int사용, 확인후 bool변경)
    public GameObject[] moveBtn; // 플레이어 이동버튼
    private Vector3 targetPosition; // 이동 위치
    public GameObject tileCheck; // 타일체크 범위
    public bool tile; // 타일맵인지
    public bool game; // 게임맵인지

    public float moveDistance = 2.3f; // 이동 거리
    public float moveSpd = 5f; // 이동 속도

    private void Start()
    {
        moveNum = 1;
        tileCheck.SetActive(false);
        tile = true;
        game = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 클릭시 해당위치로 이동
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < moveBtn.Length; i++)
                {
                    if (hit.collider.gameObject == moveBtn[i])
                    {
                        MovePlayer(i);
                        break;
                    }
                }
            }
        }

        // Lerp를 사용하여 목표 위치로 이동
        float newY = transform.position.y; // 현재 y값 저장
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpd);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // y값 복원

        // 타일이 없는곳으로는 이동불가
        if (transform.position.x <= -4f)
        {
            moveBtn[0].SetActive(false);
            moveBtn[0].GetComponent<Collider>().enabled = false;
        }
        else
        {
            moveBtn[0].SetActive(true);
            moveBtn[0].GetComponent<Collider>().enabled = true;
        }

        if (transform.position.x >= 4f)
        {
            moveBtn[2].SetActive(false);
            moveBtn[2].GetComponent<Collider>().enabled = false;
        }
        else
        {
            moveBtn[2].SetActive(true);
            moveBtn[2].GetComponent<Collider>().enabled = true;
        }

        // 이동불가시 1초뒤 타일체크
        if (moveNum > 0)
        {
            tileCheck.SetActive(false);

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(true);
                move.GetComponent<Collider>().enabled = true;
            }
        }
        else
        {
            StartCoroutine(TileCheck());
            foreach (GameObject move in moveBtn)
            {
                move.SetActive(false);
                move.GetComponent<Collider>().enabled = false;
            }
        }
    }

    IEnumerator TileCheck() // 타일체크
    {
        yield return new WaitForSeconds(1f);
        tileCheck.SetActive(true);
    }

    void MovePlayer(int direction) // 이동거리
    {
        moveNum = 0;
        switch (direction)
        {
            case 0:
                targetPosition += new Vector3(-moveDistance, 0f, moveDistance);
                break;
            case 1:
                targetPosition += new Vector3(0f, 0f, moveDistance);
                break;
            case 2:
                targetPosition += new Vector3(moveDistance, 0f, moveDistance);
                break;
            default:
                break;
        }
    }
}
