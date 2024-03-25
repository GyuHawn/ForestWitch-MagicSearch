using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerItem playerItem;

    public int itemNum; // 획득 아이템 수
    public bool onItem; // 획득 가능 여부

    public GameObject[] items; // 아이템
    public GameObject getItemUI; // 아이템 획득 UI
    public Transform getPos; // 아이템 표시 위치

    // 아이템 확률
    private Dictionary<GameObject, int> itemProbabilities = new Dictionary<GameObject, int>();

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerItem = GameObject.Find("Manager").GetComponent<PlayerItem>();

        // 아이템과 확률을 딕셔너리에 추가
        itemProbabilities.Add(items[0], 1); // crown 
        itemProbabilities.Add(items[1], 2); // pick 
        itemProbabilities.Add(items[2], 3); // jewel 
        itemProbabilities.Add(items[3], 3); // fish 
        itemProbabilities.Add(items[4], 3); // necklace 
        itemProbabilities.Add(items[5], 4); // bag 
        itemProbabilities.Add(items[6], 4); // mushroom 
        itemProbabilities.Add(items[7], 4); // shield 
        itemProbabilities.Add(items[8], 4); // sowrd 
        itemProbabilities.Add(items[9], 5); // bow 
        itemProbabilities.Add(items[10], 5); // dagger 
        itemProbabilities.Add(items[11], 5); // gold 
        itemProbabilities.Add(items[12], 5); // grow 
        itemProbabilities.Add(items[13], 5); // skull 
        itemProbabilities.Add(items[14], 7); // bone 
        itemProbabilities.Add(items[15], 7); // book 
        itemProbabilities.Add(items[16], 7); // hood 
        itemProbabilities.Add(items[17], 8); // ring 
        itemProbabilities.Add(items[18], 9); // arrow 
        itemProbabilities.Add(items[19], 9); // coin        
    }

    void Update()
    {
        if (playerMovement.currentTile == 2f)
        {
            onItem = true;
            itemNum = 1;
        }

        if (onItem && itemNum > 0)
        {
            SelectItem();
            onItem = false;
            itemNum--;
        }
    }

    public void SelectItem()
    {
        getItemUI.SetActive(true);

        // 확률에 따라 아이템 선택
        GameObject selectedItem = ChooseItem();

        if (selectedItem != null)
        {
            // 아이템이 null이 아닌 경우에만 아이템을 생성합니다.
            GameObject newItem = Instantiate(selectedItem, getPos.position, Quaternion.identity);

            // 중복된 아이템 제거
            RemoveItem(selectedItem);
        }
        else
        {
            Debug.LogError("Selected item is null."); // 현재 여기 에러
        }
    }

    private GameObject ChooseItem()
    {
        // 총 확률 합 계산
        int totalProb = 0;
        foreach (var pair in itemProbabilities)
        {
            totalProb += pair.Value;
        }

        // 랜덤한 값을 기준으로 아이템 선택
        int randomValue = Random.Range(1, totalProb + 1);
        int cumulativeProb = 0;
        foreach (var pair in itemProbabilities)
        {
            cumulativeProb += pair.Value;
            if (randomValue <= cumulativeProb)
            {
                return pair.Key;
            }
        }

        // 아이템을 선택하지 못한 경우 null을 반환합니다.
        return null;
    }

    private void RemoveItem(GameObject item)
    {
        // 선택된 아이템을 확률에서 제거
        itemProbabilities.Remove(item);

        // 확률을 재배열
        ResetPercent();
    }

    public void ResetPercent()
    {
        // 딕셔너리 키의 복사본을 만들어서 딕셔너리를 반복하는 동안 수정을 방지합니다.
        List<GameObject> keys = new List<GameObject>(itemProbabilities.Keys);

        // 확률을 조정하기 위해 기준값을 설정합니다.
        int baseProbability = 0;

        // 아이템의 확률을 기준값에 더해가면서 조정합니다.
        foreach (var key in keys)
        {
            baseProbability += itemProbabilities[key];
        }

        // 각 아이템에 새로운 확률을 설정합니다.
        foreach (var key in keys)
        {
            itemProbabilities[key] = baseProbability / itemProbabilities[key];
        }
    }

}
