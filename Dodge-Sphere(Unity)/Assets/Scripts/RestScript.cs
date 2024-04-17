using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RestScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PortionSlot portionSlot;
    private ClearInfor clearInfor;
    private AudioManager audioManager;

    public GameObject restUI;

    public GameObject player;

    private void Awake()
    {
        portionSlot = GameObject.Find("Manager").GetComponent<PortionSlot>();
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

    }

    public void SelectRest1()
    {
        clearInfor.useRest++;

        audioManager.TileMapAudio();

        playerMovement.currentHealth += 2;

        if (Random.Range(0, 100) < 50)
        {
            portionSlot.GetPortion();
        }
        restUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isRest = false;
    }
    public void SelectRest2()
    {
        clearInfor.useRest++;

        audioManager.TileMapAudio();

        playerMovement.currentHealth += 4;

        if (Random.Range(0, 100) < 30)
        {
            portionSlot.GetPortion();
        }
        restUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isRest = false;
    }

    public void SelectRest3()
    {
        clearInfor.useRest++;

        audioManager.TileMapAudio();

        playerMovement.currentHealth += 6;
        restUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isRest = false;
    }
}
