using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortionSlot : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject[] portionPrefabs;
    public GameObject[] portionPos;
    public int currentPortionNum;

    public GameObject player;

    void FindPlayer()
    {
        if (player == null) // 플레이어 찾기
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        FindPlayer(); // 플레이어 찾기

        if (currentPortionNum == 1)
        {
            Image image = portionPos[0].GetComponent<Image>();
            image.enabled = false;
        }
        else if(currentPortionNum < 1)
        {
            Image image = portionPos[0].GetComponent<Image>();
            image.enabled = true;
        }
        if (playerMovement.bag)
        {
            if (currentPortionNum == 2)
            {
                Image image = portionPos[1].GetComponent<Image>();
                image.enabled = false;
            }
            else if(currentPortionNum < 2)
            {
                Image image = portionPos[1].GetComponent<Image>();
                image.enabled = true;
            }
        }


        ItemSetting();
    }

    void ItemSetting()
    {
        if (!playerMovement.bag)
        {
            portionPos[1].SetActive(false);
        }
        else
        {
            portionPos[1].SetActive(true);
        }
    }

    public void GetPortion()
    {
        if ((currentPortionNum == 0) || (currentPortionNum == 1 && playerMovement.bag))
        {
            int num = UnityEngine.Random.Range(0, 3);
            GameObject portion = Instantiate(portionPrefabs[num], Vector3.zero, Quaternion.identity);
            portion.transform.SetParent(portionPos[currentPortionNum].transform, false);
            currentPortionNum++;
        }
    }
}
