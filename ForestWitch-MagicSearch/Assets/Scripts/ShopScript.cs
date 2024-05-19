using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;


public class ShopScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GetItem getItem;
    private ClearInfor clearInfor;
    private AudioManager audioManager;

    public GameObject shopUI;
    public GameObject[] shopSolts; // 판매 아이템 슬롯 
    public List<GameObject> shopItems = new List<GameObject>();
    public List<int> itemAmount = new List<int>(); // 판매 금액
    public TMP_Text[] amountText;

    public bool reroll;
    public GameObject itemReroll;
    public int rerollNum;

    public GameObject player;

    private void Awake()
    {
        getItem = GameObject.Find("Manager").GetComponent<GetItem>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

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

        if (reroll && rerollNum == 1)
        {
            itemReroll.SetActive(true);
        }
        else
        {
            itemReroll.SetActive(false);
        }

        // 플레이어 소지 금액보다 비쌀경우 금액을 빨간색으로 변경
        for (int i = 0; i < itemAmount.Count; i++)
        {
            if (!playerMovement.fish)
            {
                if (itemAmount[i] > playerMovement.money)
                {
                    amountText[i].color = Color.red;
                }
                else
                {
                    amountText[i].color = Color.white;
                }
            }
        }
    }

    public void ItemSetting()
    {
        if (shopItems.Count >= 6) // 이미 6개의 아이템이 있다면 더 이상 추가하지 않음
            return;

        List<int> selectedItems = new List<int>();

        for (int i = 0; i < shopSolts.Length; i++)
        {
            if (shopItems.Count >= 6) // 슬롯 설정 중에도 6개 이상 되면 중단
                break;

            int itemNum;
            do
            {
                itemNum = Random.Range(0, getItem.items.Count);
            }
            while (selectedItems.Contains(itemNum));

            selectedItems.Add(itemNum);

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
        
        if (playerMovement.fish) // 생선 아이템 보유시 1회 상점 무료 이용
        {
            playerMovement.fish = false;

            clearInfor.useShop++;

            shopSolts[0].SetActive(false);
            getItem.OnItem(shopItems[0].name);

            clearInfor.getItem++;
        }
        else
        {
            if ((playerMovement.money - itemAmount[0]) >= 0)
            {
                clearInfor.useShop++;

                shopSolts[0].SetActive(false);
                playerMovement.money -= itemAmount[0];
                getItem.OnItem(shopItems[0].name);

                clearInfor.getItem++;
            }
            else
            {
                Debug.Log("aa");
                audioManager.ButtonFailAudio(); // 금액 부족 시 오디오 재생
            }
        }
    }
    public void BuySolt2()
    {       
        if (playerMovement.fish)
        {
            playerMovement.fish = false;

            clearInfor.useShop++;

            shopSolts[1].SetActive(false);
            getItem.OnItem(shopItems[1].name);

            clearInfor.getItem++;
        }
        else
        {
            if ((playerMovement.money - itemAmount[1]) >= 0)
            {
                clearInfor.useShop++;

                shopSolts[1].SetActive(false);
                playerMovement.money -= itemAmount[1];
                getItem.OnItem(shopItems[1].name);

                clearInfor.getItem++;
            }
            else
            {
                audioManager.ButtonFailAudio();
            }
        }
    }
    public void BuySolt3()
    {
        if (playerMovement.fish)
        {
            playerMovement.fish = false;

            clearInfor.useShop++;

            shopSolts[2].SetActive(false);
            getItem.OnItem(shopItems[2].name);

            clearInfor.getItem++;
        }
        else
        {
            if ((playerMovement.money - itemAmount[2]) >= 0)
            {
                clearInfor.useShop++;

                shopSolts[2].SetActive(false);
                playerMovement.money -= itemAmount[2];
                getItem.OnItem(shopItems[2].name);

                clearInfor.getItem++;
            }
            else
            {
                audioManager.ButtonFailAudio();
            }
        }
    }
    public void BuySolt4()
    {
        if (playerMovement.fish)
        {
            playerMovement.fish = false;

            clearInfor.useShop++;

            shopSolts[3].SetActive(false);
            getItem.OnItem(shopItems[3].name);

            clearInfor.getItem++;
        }
        else
        {
            if ((playerMovement.money - itemAmount[3]) >= 0)
            {
                clearInfor.useShop++;

                shopSolts[3].SetActive(false);
                playerMovement.money -= itemAmount[3];
                getItem.OnItem(shopItems[3].name);

                clearInfor.getItem++;
            }
            else
            {
                audioManager.ButtonFailAudio();
            }
        }
    }
    public void BuySolt5()
    {
        if (playerMovement.fish)
        {
            playerMovement.fish = false;

            clearInfor.useShop++;

            shopSolts[4].SetActive(false);
            getItem.OnItem(shopItems[4].name);

            clearInfor.getItem++;
        }
        else
        {
            if ((playerMovement.money - itemAmount[4]) >= 0)
            {
                clearInfor.useShop++;

                shopSolts[4].SetActive(false);
                playerMovement.money -= itemAmount[4];
                getItem.OnItem(shopItems[4].name);
                clearInfor.getItem++;
            }
            else
            {
                audioManager.ButtonFailAudio();
            }
        }
    }
    public void BuySolt6()
    {
        if (playerMovement.fish)
        {
            playerMovement.fish = false;

            clearInfor.useShop++;

            shopSolts[5].SetActive(false);
            getItem.OnItem(shopItems[5].name);

            clearInfor.getItem++;
        }
        else
        {
            if ((playerMovement.money - itemAmount[5]) >= 0)
            {
                clearInfor.useShop++;

                shopSolts[5].SetActive(false);
                playerMovement.money -= itemAmount[5];
                getItem.OnItem(shopItems[5].name);

                clearInfor.getItem++;
            }
            else
            {
                audioManager.ButtonFailAudio();
            }
        }
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
        shopItems.Clear(); // 명확히 리스트를 초기화

        itemAmount.Clear(); // 금액 목록도 초기화

        for (int j = 0; j < shopSolts.Length; j++)
        {
            shopSolts[j].SetActive(true);
        }
    }


    public void Exit()
    {
        ResetSetting();

        playerMovement.isShop = false;
        playerMovement.moveNum = 1;

        audioManager.TileMapAudio();

        shopUI.SetActive(false);
    }
}
