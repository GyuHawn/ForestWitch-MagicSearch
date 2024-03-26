using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private MonsterMap monsterMap;

    // 전투관련 플레이어 스탯
    public int maxHealth;
    public int currentHealth;
    public float moveSpd;
    public float rotateSpd;
    public int defence;
    public bool itemShield; // 방패 아이템 획득시 보호막 사용

    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;

    // 공격 관련
    public int bulletNum;

    // 기타
    public int money;

    // 타일맵 관련
    public int moveNum; // 플레이어 이동 유무(일단 int사용, 확인후 bool변경)
    public GameObject[] moveBtn; // 플레이어 이동버튼
    private Vector3 targetPosition; // 이동 위치
    public GameObject tileCheck; // 타일체크 범위
    public bool tile; // 타일맵인지
    public float currentTile; // -1 - 빈 타일, 1 - 휴식 타일, 2 - 아이템 타일, 3 - 이벤트 타일, 4 - 상점 타일, 5 - 몬스터 타일
    public bool game; // 게임맵인지
    public float tileMoveSpd; // 이동 속도
    public float moveDistance; // 이동 거리

    private Vector3 previousPosition; // 이전 플레이어 위치
    private float timeSinceLastMovement; // 마지막으로 움직인 시간
    public Vector3 finalPlayerPos; // 마지막 타일 위치

    // UI 텍스트
    public TMP_Text healthText;
    public TMP_Text spdText;
    public TMP_Text moneyText;

    private Animator anim;
    private Rigidbody rigid;
    private Collider collider;

    // 아이템 획득 여부
    public bool arrow;
    public bool bag;
    public bool book; // 상점 미추가
    public bool bow;
    public bool dagger;
    public bool fish; // 상점 미추가
    public bool necklace;
    public bool pick;
    public bool ring;
    public bool shield;
    public bool sword;


    private void Awake()
    {
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        moveSpd = 5;
        rotateSpd = 3f;

        maxHealth = 10;
        currentHealth = maxHealth;
        defence = 0;

        moveNum = 1;
        tileMoveSpd = 3f;
        moveDistance = 2.3f;
        currentTile = 0;
        tileCheck.GetComponent<Collider>().enabled = false;
        tile = true;
        game = false;
    }


    void Update()
    {
        // 현재 체력이 최대 체력보다 많을수 없도록
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }

        // UI 텍스트
        healthText.text = currentHealth + " / " + maxHealth.ToString();
        spdText.text = moveSpd.ToString();
        moneyText.text = "$ " + money.ToString();

        // 이동 애니메이션
        if (tile)
        {
            anim.SetBool("Game", false);
            anim.SetBool("GameRun", false);
            anim.SetBool("Tile", true);
        }
        else if (game)
        {
            anim.SetBool("Tile", false);
            anim.SetBool("Game", true);

            if (moveVec != Vector3.zero)
            {
                anim.SetBool("GameRun", true);
            }
            else
            {
                anim.SetBool("GameRun", false);
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        if (currentTile < 5 && !game)
        {
            if (Input.GetMouseButtonDown(0)) // 클릭시 해당위치로 이동
            {             
                monsterMap.monsterNum = 1;

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

            // 이동시 1초뒤 타일체크
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

    /*void OnCollider() // 플레이어 충돌 켜기
    {
        collider.enabled = true;
    }
    IEnumerator delayOnCollider(float delay) // 일정시간 후 플레이어 충돌 켜기
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }
    void OffCollider() // 플레이어 충돌 끄기
    {
        collider.enabled = false;
    }*/

    public void OnTile() // 타일 맵으로 이동
    {
        tile = true;
        game = false;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void OnGame() // 게임맵으로 이동
    {
        tile = false;
        game = true;

        if (necklace) // 목걸이 아이템 획득 중 게임맵 이동시 체력 회복
        {
            currentHealth += 3;
        }

        if (ring) // 반지 아이템 획득 중 게임맵 이동시 체력 회복
        {
            currentHealth += 2;
        }

        if (shield) // 방패 아이템 획득중 게임맵 이동시 방어막 사용
        {
            itemShield = true;
        }
    }

    IEnumerator SaveFinalPosition() // 마지막 위치 저장
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("TileRun", false); // 도착시 이동 애니메이션 종료
        finalPlayerPos = transform.position;    
    }

    public void MoveFinalPosition() // 타 스크립트 코드용
    {
        transform.position = finalPlayerPos;
    }

    IEnumerator TileCheck() // 타일체크
    {
        yield return new WaitForSeconds(1f);
        tileCheck.GetComponent<Collider>().enabled = true;
    }

    void MovePlayer(int direction) // 이동거리
    {
        anim.SetBool("TileRun", true); // 이동 애니메이션 시작
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
        StartCoroutine(SaveFinalPosition());
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

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (itemShield)
            {
                itemShield = false;
            }
            else
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                currentHealth -= (bullet.damage - defence);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("P_Attack"))
        {
            bulletNum++;
            Destroy(collision.gameObject);
        }
    }
}
 