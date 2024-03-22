using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public GameObject[] players;
    public GameObject player;
    public int playerNum;
    public GameObject[] cannonPrifabs;
    public List<GameObject> cannons; // 대포 리스트

    void Start()
    {
        playerNum = PlayerPrefs.GetInt("Player");
        int cannonNum1 = PlayerPrefs.GetInt("Cannon1");
        int cannonNum2 = PlayerPrefs.GetInt("Cannon2");
        cannons.Add(cannonPrifabs[cannonNum1 - 1]);
        cannons.Add(cannonPrifabs[cannonNum2 - 1]);
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
}
