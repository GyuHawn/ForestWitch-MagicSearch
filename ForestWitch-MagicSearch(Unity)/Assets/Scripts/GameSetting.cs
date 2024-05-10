using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public GameObject[] players;
    public GameObject player;
    public int playerNum;
    public GameObject[] cannonPrefabs;
    public List<GameObject> cannons; // 대포 리스트

    public GameObject settingUI;

    private int cannonNum1;
    private int cannonNum2;

    void Start()
    {
        /*playerNum = PlayerPrefs.GetInt("Player");
        int cannonNum1 = PlayerPrefs.GetInt("Cannon1");
        int cannonNum2 = PlayerPrefs.GetInt("Cannon2");*/
        GPGSBinder.Inst.LoadCloud("Player", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                playerNum = loadedAbility1;
            }
            else
            {
                playerNum = 1;
            }
        });
        GPGSBinder.Inst.LoadCloud("Cannon1", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                cannonNum1 = loadedAbility1;
            }
            else
            {
                cannonNum1 = 1;
            }
        });
        GPGSBinder.Inst.LoadCloud("Cannon2", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                cannonNum2 = loadedAbility1;
            }
            else
            {
                cannonNum2 = 1;
            }
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
