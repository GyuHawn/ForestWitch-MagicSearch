using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EventScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GetItem getItem;
    private ClearInfor clearInfor;
    private Ability ability;
    private AudioManager audioManager;

    public int eventNum;
    public GameObject eventUI;
    public GameObject[] events;

    public bool onEvent; // 이벤트 실행 여부

    public GameObject player;

    private void Awake()
    {
        getItem = GameObject.Find("Manager").GetComponent<GetItem>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        ability = GameObject.Find("Manager").GetComponent<Ability>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        
        if (onEvent)
        {
            onEvent = false;
            playerMovement.currentTile = 0;
            StartEvent();
        }
    }

    void StartEvent()
    {
        eventNum = Random.Range(0, events.Length);
  
        eventUI.SetActive(true);
        events[eventNum].SetActive(true);
    }

    // 호수 이벤트
    public void LakeEvent1()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        playerMovement.currentHealth += 2;
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    public void LakeEvent2()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        if (Random.Range(0, 100) < 50)
        {
            int money = 300; // 기본적 300코인을 획득

            // 능력 3-1이 활성화
            if (ability.ability3Num == 1)
            {
                money = ability.GamblingCoin(money); // 능력 3-1에 따라 코인 획득을 조절
            }
            // 능력 3-2가 활성화
            else if (ability.ability3Num == 2)
            {
                money = ability.PlusCoin(money); // 능력 3-2에 따라 코인 획득을 조절
            }

            playerMovement.money += money;
            clearInfor.getMoney += money;
        }

        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    // 집 이벤트
    public void HouseEvent1()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        if (Random.Range(0, 100) < 50)
        {
            playerMovement.currentHealth += 2;
        }
        else
        {
            playerMovement.currentHealth -= 2;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
    public void HouseEvent2()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        if (Random.Range(0, 100) < 50)
        {
            playerMovement.currentHealth += 4;
        }
        else
        {
            playerMovement.currentHealth -= 4;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    // 동굴 이벤트
    public void CaveEvent1()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        playerMovement.currentHealth += 3;
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
    public void CaveEvent2()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        if (Random.Range(0, 100) < 50)
        {
            int money = 300; // 기본적 300코인을 획득

            // 능력 3-1이 활성화
            if (ability.ability3Num == 1)
            {
                money = ability.GamblingCoin(money); // 능력 3-1에 따라 코인 획득을 조절
            }
            // 능력 3-2가 활성화
            else if (ability.ability3Num == 2)
            {
                money = ability.PlusCoin(money); // 능력 3-2에 따라 코인 획득을 조절
            }

            playerMovement.money += money;
            clearInfor.getMoney += money;
        }
        else
        {
            playerMovement.currentHealth -= 2;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    // 상자 이벤트
    public void ItemEvent1()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        playerMovement.currentHealth -= 2;

        if (Random.Range(0, 100) < 50)
        {
            int money = 100; // 기본적 300코인을 획득

            // 능력 3-1이 활성화
            if (ability.ability3Num == 1)
            {
                money = ability.GamblingCoin(money); // 능력 3-1에 따라 코인 획득을 조절
            }
            // 능력 3-2가 활성화
            else if (ability.ability3Num == 2)
            {
                money = ability.PlusCoin(money); // 능력 3-2에 따라 코인 획득을 조절
            }

            playerMovement.money += money;
            clearInfor.getMoney += money;
        }
        else
        {
            getItem.SelectItem();
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
    public void ItemEvent2()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        if (Random.Range(0, 100) < 30)
        {
            getItem.SelectItem();
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    // 돌아가기
    public void PassEvent()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
}
