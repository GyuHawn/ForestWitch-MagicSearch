using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GetItem : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerItem playerItem;
    private ClearInfor clearInfor;
    private AudioManager audioManager;

    public bool onItem; // 획득 가능 여부

    public GameObject item; // 획득한 아이템
    public List<GameObject> items = new List<GameObject>(); // 아이템
    public GameObject getItemUI; // 아이템 획득 UI
    public GameObject getPos; // 아이템 표시 위치
    
    public GameObject player;

    private void Awake()
    {
        playerItem = GameObject.Find("Manager").GetComponent<PlayerItem>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        FindPlayer(); // 플레이어 찾기

        if (onItem)
        {
            onItem = false;
            playerMovement.currentTile = 0;
            SelectItem();
        }
    }
    void FindPlayer()
    {
        if (player == null) // 플레이어 찾기
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

    }

    IEnumerator GetItemAudio() // 아이템 획득 오디오 사용후 다시 BGM시작
    {
        audioManager.GetItemAudio();

        yield return new WaitForSeconds(1f);

        audioManager.TileMapAudio();
    }

    public void SelectItem()
    {
        getItemUI.SetActive(true);

        StartCoroutine(GetItemAudio());

        int index = Random.Range(0, items.Count);
        GameObject selectedItem = items[index];
        item = Instantiate(selectedItem, Vector3.zero, Quaternion.identity);
        item.name = selectedItem.name;
        item.transform.SetParent(getPos.transform, false);

        items.RemoveAt(index);

        clearInfor.getItem++;
        OnItem(selectedItem.name);

        StartCoroutine(ResetSetting());
    }

    IEnumerator ResetSetting()
    {
        yield return new WaitForSeconds(2f);

        playerMovement.isItem = false;
        getItemUI.SetActive(false);
        Destroy(item);
    }

    public void OnItem(string item)
    {
        switch (item)
        {
            case "Arrow":
                {
                    playerItem.arrow = true;
                    break;
                }
            case "Bag":
                {
                    playerItem.bag = true;
                    break;
                }
            case "Bone":
                {
                    playerItem.bone = true;
                    break;
                }
            case "Book":
                {
                    playerItem.book = true;
                    break;
                }
            case "Bow":
                {
                    playerItem.bow = true;
                    break;
                }
            case "Crown":
                {
                    playerItem.crown = true;
                    break;
                }
            case "Dagger":
                {
                    playerItem.dagger = true;
                    break;
                }
            case "Fish":
                {
                    playerItem.fish = true;
                    break;
                }
            case "Gold":
                {
                    playerItem.gold = true;
                    break;
                }
            case "Grow":
                {
                    playerItem.grow = true;
                    break;
                }
            case "Hood":
                {
                    playerItem.hood = true;
                    break;
                }
            case "Jewel":
                {
                    playerItem.jewel = true;
                    break;
                }
            case "Coin":
                {
                    playerItem.coin = true;
                    break;
                }
            case "Mushroom":
                {
                    playerItem.mushroom = true;
                    break;
                }
            case "Necklace":
                {
                    playerItem.necklace = true;
                    break;
                }
            case "Pick":
                {
                    playerItem.pick = true;
                    break;
                }
            case "Ring":
                {
                    playerItem.ring = true;
                    break;
                }
            case "Shield":
                {
                    playerItem.shield = true;
                    break;
                }
            case "Skull":
                {
                    playerItem.skull = true;
                    break;
                }
            case "Sword":
                {
                    playerItem.sword = true;
                    break;
                }
        }
    }
}
