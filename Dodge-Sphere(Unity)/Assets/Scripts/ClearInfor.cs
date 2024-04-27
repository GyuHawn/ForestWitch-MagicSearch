using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearInfor : MonoBehaviour
{
    private AudioManager audioManager;
    private TimeManager timeManager;
    private StoryScript storyScript;
    private PlayerMovement playerMovement;

    // 총 시간
    public float totalTime;
    public TMP_Text totalTimeText;

    // 잡은 몬스터 수
    public int killedMonster;
    public TMP_Text killedMonsterText;

    // 잡은 보스 수
    public int killedBoss;
    public TMP_Text killedBossText;

    // 상점 이용 횟수
    public int useShop;
    public TMP_Text useShopText;

    // 발생한 이벤트 횟수
    public int useEvent;
    public TMP_Text useEventText;

    // 획득한 아이템 수
    public int getItem;
    public TMP_Text getItemText;

    // 쉬어간 횟수
    public int useRest;
    public TMP_Text useRestText;

    // 총 획득한 코인
    public int getMoney;
    public TMP_Text getMoneyText;

    // 사용한 물약 수
    public int usePotion;
    public TMP_Text usePotionText;

    // 총합
    public int totalScore;
    public TMP_Text totalScoreText;

    public GameObject resultUI; // 결과창 
    public bool result; // 결과창 표시 여부
    public TMP_Text clearStateText; // 클리어 or 사망에 따른 이름 텍스트

    public bool onStory;
    public bool clear; // 클리어 여부

    private void Awake()
    {
        timeManager = GameObject.Find("Manager").GetComponent<TimeManager>();
        storyScript = GameObject.Find("Manager").GetComponent<StoryScript>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        onStory = PlayerPrefs.GetInt("Story") == 1 ? true : false;
    }

    
    void Update()
    {       
        if (onStory && result)
        {
            if (!clear)
            {
                audioManager.DefeatAudio();
                ClearUI();
            }
            else
            {               
                if (storyScript.page == 0)
                {
                    storyScript.story.SetActive(true);
                    audioManager.StoryAudio();
                }
                else if (storyScript.page >= 4)
                {
                    audioManager.ClearAudio();
                    ClearUI();
                }
            }
            
        }
        else if (!onStory && result) // 결과창 표시 중
        {
            if (!clear)
            {
                audioManager.DefeatAudio();
                ClearUI();
            }
            else
            {
                audioManager.ClearAudio();
                ClearUI();
            }     
        }
    }

    void ClearUI()
    {      
        resultUI.SetActive(true);

        TotalResults();

        // 총시간 표시
        totalTime = timeManager.currentTime;
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);
        totalTimeText.text = timeManager.currnetTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

        // 잡은 몬스터 수 표시 (몬스터 죽을때 증가)
        killedMonsterText.text = killedMonster.ToString();

        // 잡은 보스 수 표시 (보스 죽을때 증가)
        killedBossText.text = killedBoss.ToString();

        // 상점 이용 횟수 표시 (아이템 구매시 증가)
        useShopText.text = useShop.ToString();

        // 발생한 이벤트 횟수 표시 (이벤트 선택시 증가)
        useEventText.text = useEvent.ToString();

        // 획득한 아이템 수 표시 (아이템 획득시 증가)
        getItemText.text = getItem.ToString();

        // 쉬어간 횟수 표시 (휴시 이벤트 선택시 증가)
        useRestText.text = useRest.ToString();

        // 총 획득한 코인 수 표시 (코인 획득 마다 증가)
        getMoneyText.text = getMoney.ToString();

        // 사용한 물약 수 표시 (물약 사용시 증가)
        usePotionText.text = usePotion.ToString();

        // 총합 표시
        totalScoreText.text = totalScore.ToString();

        PlayerPrefs.SetInt("GameExp", totalScore);

        result = false;
    }

    void TotalResults()
    {
        totalScore = (killedMonster * 3) + (killedBoss * 10) + (useShop * 2) + (useEvent * 2) + (getItem * 2)
                    + (useRest * 2) + (getItem / 200) + (usePotion * 5);
    }

    public void GameClear()
    {
        LoadingController.LoadNextScene("MainMenu");
    }
}
