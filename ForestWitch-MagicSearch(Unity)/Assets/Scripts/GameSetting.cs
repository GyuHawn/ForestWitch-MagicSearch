using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    private GameDatas gameDatas;

    public GameObject[] players;
    public GameObject player;
    public int playerNum;
    public GameObject[] cannonPrefabs;
    public List<GameObject> cannons; // 대포 리스트

    public GameObject settingUI;

    private int cannonNum1;
    private int cannonNum2;

    private void Awake()
    {
        gameDatas = GameObject.Find("Manager").GetComponent<GameDatas>();
    }

    void Start()
    {

        gameDatas.LoadFieldData<int>("playerNum", value => {
            playerNum = value;
        }, () => {
            playerNum = 1;
        });
        gameDatas.LoadFieldData<int>("cannonNum1", value => {
            cannonNum1 = value;
        }, () => {
            cannonNum1 = 1;
        });
        gameDatas.LoadFieldData<int>("cannonNum2", value => {
            cannonNum2 = value;
        }, () => {
            cannonNum2 = 2;
        });

        cannons.Add(cannonPrefabs[cannonNum1 - 1]);
        cannons.Add(cannonPrefabs[cannonNum2 - 1]);
    }

    void Update()
    {
        if(playerNum == 1) 
        {
            Destroy(players[1]);
        }
        else if(playerNum == 2)
        {
            Destroy(players[0]);
        }
    }

    public void Setting()
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }
}
