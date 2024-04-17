using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class MonsterGetMoney : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private ClearInfor clearInfor;
    private AudioManager audioManager;

    public GameObject getMoneyUI;
    public TMP_Text getMoneyText;
    public int getMoney;

    public GameObject player;

    private void Awake()
    {
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        getMoneyText.text = getMoney.ToString();
    }

    public void PickUpMoney()
    {
        StartCoroutine(StartGetMoney());
    }

    IEnumerator StartGetMoney()
    {
        audioManager.WinAudio();

        getMoneyUI.SetActive(true);
        playerMovement.money += getMoney;

        clearInfor.getMoney += getMoney;

        yield return new WaitForSeconds(2f);
        getMoneyUI.SetActive(false);
    }
}
