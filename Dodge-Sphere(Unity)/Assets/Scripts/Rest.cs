using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rest : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PortionSlot portionSlot;

    public GameObject restUI;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        portionSlot = GameObject.Find("Manager").GetComponent<PortionSlot>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
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
    }

    public void SelectRest3()
    {
        playerMovement.currentHealth += 6;
        restUI.SetActive(false);
        playerMovement.moveNum = 1;
    }
}
