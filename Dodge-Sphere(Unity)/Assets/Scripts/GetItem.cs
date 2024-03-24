using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GetItem : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject getMoneyUI;
    public TMP_Text getMoneyText;
    public int getMoney;
    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        getMoneyText.text = getMoney.ToString();
    }

    public void GetMoney()
    {
        StartCoroutine(StartGetMoney());
    }

    IEnumerator StartGetMoney()
    {
        getMoneyUI.SetActive(true);
        playerMovement.money += getMoney;

        yield return new WaitForSeconds(2f);
        getMoneyUI.SetActive(false);
    }
}
