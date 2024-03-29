using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GetItem getItem;

    public GameObject shopUI;
    public GameObject[] shopSolts; // 판매 아이템 슬롯 
    public List<GameObject> shopItems = new List<GameObject>();
    public List<int> itemAmount = new List<int>(); // 판매 금액
    public TMP_Text[] amountText;

    public bool reroll;
    public GameObject itemReroll;
    public int rerollNum;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        getItem = GameObject.Find("Manager").GetComponent<GetItem>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (reroll && rerollNum == 1)
        {
            itemReroll.SetActive(true);
        }
        else
        {
            itemReroll.SetActive(false);
        }
    }

    public void ItemSetting()
    {
        List<int> selectedItems = new List<int>(); // 선택된 번호 저장 리스트

        for (int i = 0; i < shopSolts.Length; i++)
        {
            int itemNum;
            do
            {
                itemNum = Random.Range(0, getItem.items.Count); // 아이템 번호 랜덤 선택
            }
            while (selectedItems.Contains(itemNum)); // 선택된 번호 리스트에 있을시 다시 선택

            selectedItems.Add(itemNum); // 선택된 번호를 리스트에 추가

            // 아이템 인스턴스화 및 설정
            GameObject item = Instantiate(getItem.items[itemNum], Vector3.zero, Quaternion.identity);
            item.transform.SetParent(shopSolts[i].transform, false);
            item.name = getItem.items[itemNum].name;
            shopItems.Add(item);
        }

        AmountSetting();

        selectedItems.Clear();
    }

    void AmountSetting()
    {
        int min = 700 / 50;
        int max = 1500 / 50;

        for(int i  = 0; i < amountText.Length; i++)
        {
            int amount = Random.Range(min, max + 1) * 50; // 50단위로 값 생성
            itemAmount.Add(amount);
            amountText[i].text = itemAmount[i].ToString();
        }

    }

    public void BuySolt1()
    {
        shopSolts[0].SetActive(false);
        playerMovement.money -= itemAmount[0];
        getItem.OnItem(shopItems[0].name);
    }
    public void BuySolt2()
    {
        shopSolts[1].SetActive(false);
        playerMovement.money -= itemAmount[1];
        getItem.OnItem(shopItems[1].name);

    }
    public void BuySolt3()
    {
        shopSolts[2].SetActive(false);
        playerMovement.money -= itemAmount[2];
        getItem.OnItem(shopItems[2].name);
    }
    public void BuySolt4()
    {
        shopSolts[3].SetActive(false);
        playerMovement.money -= itemAmount[3];
        getItem.OnItem(shopItems[3].name);
    }
    public void BuySolt5()
    {
        shopSolts[4].SetActive(false);
        playerMovement.money -= itemAmount[4];
        getItem.OnItem(shopItems[4].name);
    }
    public void BuySolt6()
    {
        shopSolts[5].SetActive(false);
        playerMovement.money -= itemAmount[5];
        getItem.OnItem(shopItems[5].name);
    }


    public void Reroll()
    {
        rerollNum--;
        ResetSetting();
        ItemSetting();
    }

    void ResetSetting()
    {
        foreach (GameObject shopItem in shopItems)
        {
            Destroy(shopItem);
        }

        for (int i = 0; i < itemAmount.Count; i++)
        {
            itemAmount.Clear();
        }

        for (int j = 0 ; j < shopSolts.Length; j++)
        {
            shopSolts[j].SetActive(true);
        }
    }

    public void Exit()
    {
        ResetSetting();
        playerMovement.moveNum = 1;
        shopUI.SetActive(false);
    }
}
