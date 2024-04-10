using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portion : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PortionSlot portionSlot;

    public int functionNum;
    public Button function;

    public GameObject player;

    private void Awake()
    {
        portionSlot = GameObject.Find("Manager").GetComponent<PortionSlot>();
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
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }


    public void SpeedPortion()
    {
        portionSlot.currentPortionNum--;
        Image image = GetComponent<Image>();
        image.enabled = false;
        StartCoroutine(SpeedUP());
    }

    IEnumerator SpeedUP()
    {
        playerMovement.moveSpd++;
        yield return new WaitForSeconds(2f);
        playerMovement.moveSpd--;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void GamblePortion()
    {
        portionSlot.currentPortionNum--;
        int num = Random.Range(0, 2);

        if(num == 0)
        {
            playerMovement.money += 300;
        }
        else if(num == 1) 
        {
            playerMovement.currentHealth--;
        }

        Destroy(gameObject);
    }

    public void HealthPortion()
    {
        portionSlot.currentPortionNum--;
        playerMovement.currentHealth += 2;
        Destroy(gameObject);
    }
}
