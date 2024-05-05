using System.Collections;
using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    private MonsterMap monsterMap;
    private ClearInfor clearInfor;
    private MapSetting mapSetting;
    private Ability ability;
    private AudioManager audioManager;

    public FixedJoystick fixedJoyStick;
    public GameObject joyStick;

    // 캐릭터 확인
    public int playerNum; // 1 = 검정, 2 = 파랑

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
    public TMP_Text bulletNumText;

    public GameObject extraAttack; // 투사체 공격 

    // 기타
    public int money;
    public bool faint; // 기절여부
     
    // 타일맵 관련
    public int moveNum; // 플레이어 이동 유무(일단 int사용, 확인후 bool변경)
    public GameObject[] moveBtn; // 플레이어 이동버튼
    private Vector3 targetPosition; // 이동 위치
    public GameObject tileCheck; // 타일체크 범위
    public bool tile; // 타일맵인지
    public float currentTile; // 0 - 빈 타일, 2 - 아이템 타일, 3 - 이벤트 타일, 4 - 상점 타일, 5 - 몬스터 타일, 6 - 휴식 타일
    public bool game; // 게임맵인지
    public float tileMoveSpd; // 이동 속도
    public float moveDistance; // 이동 거리

    public Vector3 finalPlayerPos; // 마지막 타일 위치

    public GameObject bossRest1;
    public GameObject bossRest2;
    public GameObject bossRest3;

    // UI 텍스트
    public TMP_Text healthText;
    public TMP_Text spdText;
    public TMP_Text moneyText;

    private Animator anim;
    private Rigidbody rigid;
    private Collider collider;

    // 특수 맵 진행중
    public bool isShop;
    public bool isEvent;
    public bool isItem;
    public bool isRest;

    // 스테이지 넘어감
    public GameObject nextStageUI;
    public bool nextStage;

    // 아이템 획득 여부
    public bool arrow;
    public bool bag;
    public bool book;
    public bool bow;
    public bool dagger;
    public bool fish;
    public bool necklace;
    public bool pick;
    public bool ring;
    public bool shield;
    public bool sword;

    // 이펙트
    public GameObject shieldEffect; // 쉴드 이펙트

    private void Awake()
    {
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
        ability = GameObject.Find("Manager").GetComponent<Ability>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        playerNum = PlayerPrefs.GetInt("Player");
        if (playerNum == 1)
        {
            maxHealth = 10;
            moveSpd = 6;
        }
        else if(playerNum == 2)
        {
            maxHealth = 8;
            moveSpd = 8;
        }
       
        rotateSpd = 3f;
     
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
        bulletNumText.text = bulletNum.ToString(); // 현재 총알 수 표시

        if (itemShield)
        {
            shieldEffect.SetActive(true);
        }
        else
        {
            shieldEffect.SetActive(false);
        }

        if (nextStage)
        {
            nextStage = false;
            StartCoroutine(OnNextStage());
        }

        // 현재 체력이 최대 체력보다 많을 수 없도록
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }

        // UI 텍스트
        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
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

        // 능력 5-1이 활성화
        if (ability.ability5Num == 1) // 능력 5-1에 따라 게임당 1번 체력이 25% 이하로 줄어들면 50%회복
        {
            if (currentHealth <= (maxHealth / 4) && ability.healing)
            {
                currentHealth += maxHealth / 2;
                ability.healing = false;
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        if ((currentTile < 5 && !game) && (!isShop || !isRest || !isItem || !isEvent || !nextStage))
        {
            joyStick.SetActive(false);

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

            // 이동시 1초뒤 타일체크 후 특정 위치에서 이동제한
            TileCheckAndMoveLimited();
        }
        else if (game && transform.position.x > 100)
        {
            tileCheck.GetComponent<Collider>().enabled = false;

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(false);
                move.GetComponent<Collider>().enabled = false;
            }

            if (!faint) // 기절시 이동불가
            {
                joyStick.SetActive(true);
                // 키보드 이동 기능
                GetInput();
                Move();
                Rotate();
            }
        }
    }
    
    void TileCheckAndMoveLimited() // 이동시 1초뒤 타일체크 후 특정 위치에서 이동제한
    {
        if (moveNum > 0 && transform.position.z <= 27) // 보스맵 휴식 타일전
        {
            tileCheck.GetComponent<Collider>().enabled = false;

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(true);
                move.GetComponent<Collider>().enabled = true;
            }

            // 타일이 없는곳으로는 이동불가
            if (transform.position.x <= -4f)
            {
                moveBtn[0].SetActive(false);
                moveBtn[0].GetComponent<Collider>().enabled = false;

                if (transform.position.z >= 25)
                {
                    moveBtn[1].SetActive(false);
                    moveBtn[1].GetComponent<Collider>().enabled = false;
                }
            }
            else if (transform.position.x >= 4f)
            {
                moveBtn[2].SetActive(false);
                moveBtn[2].GetComponent<Collider>().enabled = false;

                if (transform.position.z >= 25)
                {
                    moveBtn[1].SetActive(false);
                    moveBtn[1].GetComponent<Collider>().enabled = false;
                }
            }
            else if (transform.position.x <= -2f && transform.position.z >= 25)
            {
                moveBtn[0].SetActive(false);
                moveBtn[0].GetComponent<Collider>().enabled = false;
            }
            else if (transform.position.x >= 2f && transform.position.z >= 25)
            {
                moveBtn[2].SetActive(false);
                moveBtn[2].GetComponent<Collider>().enabled = false;
            }
            else
            {
                foreach (GameObject move in moveBtn)
                {
                    move.SetActive(true);
                    move.GetComponent<Collider>().enabled = true;
                }
            }
        }
        else if (transform.position.z >= 27) // 보스맵 휴식 타일
        {
            tileCheck.GetComponent<Collider>().enabled = false;

            if (gameObject.GetComponent<Collider>().bounds.Intersects(bossRest1.GetComponent<Collider>().bounds))
            {
                moveBtn[0].SetActive(false);
                moveBtn[0].GetComponent<Collider>().enabled = false;
                moveBtn[1].SetActive(false);
                moveBtn[1].GetComponent<Collider>().enabled = false;
                moveBtn[2].SetActive(true);
                moveBtn[2].GetComponent<Collider>().enabled = true;
            }
            else if (gameObject.GetComponent<Collider>().bounds.Intersects(bossRest2.GetComponent<Collider>().bounds))
            {
                moveBtn[0].SetActive(false);
                moveBtn[0].GetComponent<Collider>().enabled = false;
                moveBtn[1].SetActive(true);
                moveBtn[1].GetComponent<Collider>().enabled = true;
                moveBtn[2].SetActive(false);
                moveBtn[2].GetComponent<Collider>().enabled = false;
            }
            else if (gameObject.GetComponent<Collider>().bounds.Intersects(bossRest3.GetComponent<Collider>().bounds))
            {
                moveBtn[0].SetActive(true);
                moveBtn[0].GetComponent<Collider>().enabled = true;
                moveBtn[1].SetActive(false);
                moveBtn[1].GetComponent<Collider>().enabled = false;
                moveBtn[2].SetActive(false);
                moveBtn[2].GetComponent<Collider>().enabled = false;
            }
        }
        if (moveNum <= 0)
        {
            StartCoroutine(TileCheck());
            foreach (GameObject move in moveBtn)
            {
                move.SetActive(false);
                move.GetComponent<Collider>().enabled = false;
            }
        }
    }

    IEnumerator OnNextStage() // 스테이지가 넘어갈때 잠시 UI사용
    {
        yield return new WaitForSeconds(1f);
        nextStageUI.SetActive(true);

        yield return new WaitForSeconds(2f);
        nextStageUI.SetActive(false);
    }

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

    public void PostionReset()
    {
        finalPlayerPos = new Vector3(0, 1, 0);
        targetPosition = new Vector3(0, 1, 0);
        transform.position = finalPlayerPos;
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
    private void GetInput() // 조이스틱 입력 값 받기
    {
        hAxis = fixedJoyStick.Horizontal;
        vAxis = fixedJoyStick.Vertical;

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
        clearInfor.result = true;
        clearInfor.clearStateText.text = "어둠속으로...";

        // 플레이어 삭제시 많은 오류 발생으로 인한 위치이동 (제거 하지 않아도 문제 없음)
        transform.position = Vector3.zero;
        currentHealth += 1; // 다이 함수 중복방지 

        // 남아있는 몬스터 삭제
        GameObject monster = GameObject.FindWithTag("Monster");
        Destroy(monster);
    }

    IEnumerator PlayerFaint(float time) // 기절 시 일정시간뒤 기절해제
    {
        faint = true;

        yield return new WaitForSeconds(time);
        faint = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (itemShield)
            {
                itemShield = false;
                Destroy(collision.gameObject);
            }
            else if (!itemShield)
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();

                // 능력 5-2가 활성화
                if (ability.ability5Num == 2)
                {
                    int num = Random.Range(0, 10);
                    if (num < 4)
                    {
                        Destroy(collision.gameObject); // 능력 5-2에 따라 피격시 확률적 무시
                        return;
                    }
                }

                currentHealth -= (bullet.damage - defence);
                Destroy(collision.gameObject);
            }

            // 능력 6-1이 활성화
            if (ability.ability6Num == 1)
            {
                ability.HitCannonReload(); // 능력 6-1에 따라 피격시 모든 대포 총알 1증가
            }
            // 능력 6-2가 활성화
            else if (ability.ability6Num == 2)
            {
                ability.HitExtraAttack(); // 능력 6-2에 따라 피격시 2개 투사체 발사
            }
        }

        if (collision.gameObject.CompareTag("FakeBullet"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("BreakBullet"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("FaintBullet"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(PlayerFaint(1f));
        }

        if (collision.gameObject.CompareTag("P_Attack"))
        {
            bulletNum++;
            Destroy(collision.gameObject);

            // 능력 1-1이 활성화
            if (ability.ability1Num == 1)
            {
                ability.GetPlayerMP(); // 능력 1-1에 따라 총알 획득시 확률적 총알 획득
            }
            // 능력 1-2가 활성화
            else if (ability.ability1Num == 2)
            {
                ability.GetCannonReload(); // 능력 1-2에 따라 총알 획득시 확률적으로 모든 대포 총알 1 장전
            }

            // 능력 2-1이 활성화
            if (ability.ability2Num == 1)
            {
                ability.MPExtraAttack(); // 능력 2-1에 따라 총알 획득시 확률적으로 투사체 공격
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            if(mapSetting.stage == 1)
            {
                transform.position = monsterMap.playerMapSpawnPos[0].transform.position;
            }
            else if(mapSetting.stage == 2)
            {
                transform.position = monsterMap.playerMapSpawnPos[1].transform.position;
            }
        }
    }
}
 