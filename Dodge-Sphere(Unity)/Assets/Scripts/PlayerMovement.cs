using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 전투관련 플레이어 스탯
    public int maxHealth;
    public int currentHealth;
    public float moveSpd;
    public float rotateSpd;

    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;

    // 타일맵 관련
    public int moveNum; // 플레이어 이동 유무(일단 int사용, 확인후 bool변경)
    public GameObject[] moveBtn; // 플레이어 이동버튼
    private Vector3 targetPosition; // 이동 위치
    public GameObject tileCheck; // 타일체크 범위
    public bool tile; // 타일맵인지
    public float currentTile; // 0 - 빈 타일, 1 - 휴식 타일, 2 - 아이템 타일, 3 - 이벤트 타일, 4 - 상점 타일, 5 - 몬스터 타일
    public bool game; // 게임맵인지
    public float tileMoveSpd = 5f; // 이동 속도
    public float moveDistance = 2.3f; // 이동 거리

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveSpd = 5;
        rotateSpd = 5f;

        moveNum = 1;
        currentTile = 0;
        //currentTile = 5; // 몬스터 싸움 기능 제작중
        tileCheck.GetComponent<Collider>().enabled = false;
        tile = true;
        game = false;
        //tile = false;
        //game = true;
    }

    void Update()
    {
        if (currentTile < 5 && !game)
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
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * tileMoveSpd);
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
                tileCheck.GetComponent<Collider>().enabled = false;

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
        else if (game)
        {
            tileCheck.GetComponent<Collider>().enabled = false;

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(false);
                move.GetComponent<Collider>().enabled = false;
            }

            // 키보드 움직임 기능
            GetInput();
            Move();
            Rotate();

        }
/*        else // 몬스터 싸움 종료후
        {
            tileCheck.GetComponent<Collider>().enabled = true;

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(true);
                move.GetComponent<Collider>().enabled = true;
            }
        }*/
    }

    IEnumerator TileCheck() // 타일체크
    {
        yield return new WaitForSeconds(1f);
        tileCheck.GetComponent<Collider>().enabled = true;
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

    // 전투중 이동 관련
    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis);
    }

    private void Move()
    {
        transform.position += moveVec * moveSpd * Time.deltaTime;
    }

    private void Rotate()
    {
        if (moveVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVec.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpd);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
 