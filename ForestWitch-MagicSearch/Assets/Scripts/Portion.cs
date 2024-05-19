using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portion : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PortionSlot portionSlot;
    private ClearInfor clearInfor;
    private AudioManager audioManager;

    public int functionNum;
    public Button function;

    public GameObject ex;
    public Button exBtn;

    public GameObject player;

    private void Awake()
    {
        portionSlot = GameObject.Find("Manager").GetComponent<PortionSlot>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        if (functionNum == 1)
        {
            function.onClick.AddListener(SpeedPortion);
        }
        else if (functionNum == 2)
        {
            function.onClick.AddListener(GamblePortion);
        }
        else if (functionNum == 3)
        {
            function.onClick.AddListener(HealthPortion);
        }
        exBtn.onClick.AddListener(ExUI);
    }

    void Update()
    {
        FindPlayer(); // 플레이어 찾기
    }

    void FindPlayer()
    {
        if (player == null) // 플레이어 찾기
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    public void SpeedPortion()
    {
        audioManager.PotionAudio();
        portionSlot.currentPortionNum--;
        Image image = GetComponent<Image>();
        image.enabled = false;
        StartCoroutine(SpeedUP());
    }

    IEnumerator SpeedUP()
    {
        clearInfor.usePotion++;

        playerMovement.moveSpd++;
        yield return new WaitForSeconds(2f);
        playerMovement.moveSpd--;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void GamblePortion()
    {
        audioManager.PotionAudio();

        clearInfor.usePotion++;

        portionSlot.currentPortionNum--;
        int num = Random.Range(0, 2);

        if (num == 0)
        {
            playerMovement.maxHealth += 1;
        }
        else if (num == 1)
        {
            playerMovement.money += 300;
        }

        Destroy(gameObject);
    }

    public void HealthPortion()
    {
        audioManager.PotionAudio();

        clearInfor.usePotion++;

        portionSlot.currentPortionNum--;
        playerMovement.currentHealth += 2;
        Destroy(gameObject);
    }

    public void ExUI()
    {
        ex.SetActive(!ex.gameObject.activeSelf);
    }
}
