using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RestScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PortionSlot portionSlot;

    public GameObject restUI;

    public GameObject player;

    private void Awake()
    {
        portionSlot = GameObject.Find("Manager").GetComponent<PortionSlot>();
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
        playerMovement.currentHealth += 6;
        restUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isRest = false;
    }
}
