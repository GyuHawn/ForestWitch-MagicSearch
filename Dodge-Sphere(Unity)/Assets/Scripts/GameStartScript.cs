using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    public GameObject selectWindow;
    public GameObject playerWindow;
    public GameObject cannonWindow;
    public GameObject startButton;

    public int playerNum;
    public int cannonNum1;
    public int cannonNum2;

    public GameObject[] playerPrefabs;
    public GameObject[] cannonPrefabs;

    public GameObject playerPos;
    public GameObject cannon1Pos;
    public GameObject cannon2Pos;

    public GameObject[] selectCannons;

    public GameObject player;
    public GameObject cannon1;
    public GameObject cannon2;

    void Start()
    {

    }

    

    void Update()
    {
        Select();

        if (playerNum != 0 && player == null)
        {
            Vector3 select = new Vector3(playerPos.transform.position.x, playerPos.transform.position.y + 1, playerPos.transform.position.z);
            player = Instantiate(playerPrefabs[playerNum - 1], select, Quaternion.identity, playerPos.transform);
            playerWindow.SetActive(false);
            cannonWindow.SetActive(true);
        }


        if (cannonNum1 != 0 && cannon1 == null)
        {
            cannon1 = Instantiate(cannonPrefabs[cannonNum1 - 1], CannonSelectPos(cannon1Pos, cannonNum1), Quaternion.identity, cannon1Pos.transform);
        }

        if (cannonNum2 != 0 && cannon2 == null)
        {
            cannon2 = Instantiate(cannonPrefabs[cannonNum2 - 1], CannonSelectPos(cannon2Pos, cannonNum2), Quaternion.identity, cannon2Pos.transform);
        }

        if(playerNum != 0 && cannonNum1 != 0 && cannonNum2 != 0)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    Vector3 CannonSelectPos(GameObject cannon, int num)
    {
        Vector3 selectPos = Vector3.zero; // 초기화된 벡터 생성

        switch (num)
        {
            case 1:
                selectPos = new Vector3(cannon.transform.position.x, cannon.transform.position.y + 2, cannon.transform.position.z);
                break;
            case 2:
                selectPos = new Vector3(cannon.transform.position.x, cannon.transform.position.y + 2, cannon.transform.position.z);
                break;
            case 3:
                selectPos = new Vector3(cannon.transform.position.x - 1, cannon.transform.position.y + 2, cannon.transform.position.z);
                break;
            case 4:
                selectPos = new Vector3(cannon.transform.position.x - 1.5f, cannon.transform.position.y + 3, cannon.transform.position.z);
                break;
            case 5:
                selectPos = new Vector3(cannon.transform.position.x - 2.5f, cannon.transform.position.y + 2, cannon.transform.position.z);
                break;
            case 6:
                selectPos = new Vector3(cannon.transform.position.x - 1.5f, cannon.transform.position.y + 1, cannon.transform.position.z);
                break;
            case 7:
                selectPos = new Vector3(cannon.transform.position.x - 1.5f, cannon.transform.position.y + 1, cannon.transform.position.z);
                break;
        }

        return selectPos; // 선택된 위치 반환
    }



    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);
    }

    public void Player1()
    {
        playerNum = 1;
        PlayerPrefs.SetInt("Player", 1);
    }
    public void Player2()
    {
        playerNum = 2;
        PlayerPrefs.SetInt("Player", 2);
    }

    public void Cannon1()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 1;
                PlayerPrefs.SetInt("Cannon1", 1);
            }
            else
            {
                cannonNum2 = 1;
                PlayerPrefs.SetInt("Cannon2", 1);
            }
        }
    }
    public void Cannon2()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 2;
                PlayerPrefs.SetInt("Cannon1", 2);
            }
            else
            {
                cannonNum2 = 2;
                PlayerPrefs.SetInt("Cannon2", 2);
            }
        }
    }
    public void Cannon3()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 3;
                PlayerPrefs.SetInt("Cannon1", 3);
            }
            else
            {
                cannonNum2 = 3;
                PlayerPrefs.SetInt("Cannon2", 3);
            }
        }
    }
    public void Cannon4()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 4;
                PlayerPrefs.SetInt("Cannon1", 4);
            }
            else
            {
                cannonNum2 = 4;
                PlayerPrefs.SetInt("Cannon2", 4);
            }
        }
    }
    public void Cannon5()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 5;
                PlayerPrefs.SetInt("Cannon1", 5);
            }
            else
            {
                cannonNum2 = 5;
                PlayerPrefs.SetInt("Cannon2", 5);
            }
        }
    }
    public void Cannon6()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 6;
                PlayerPrefs.SetInt("Cannon1", 6);
            }
            else
            {
                cannonNum2 = 6;
                PlayerPrefs.SetInt("Cannon2", 6);
            }
        }
    }
    public void Cannon7()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 7;
                PlayerPrefs.SetInt("Cannon1", 7);
            }
            else
            {
                cannonNum2 = 7;
                PlayerPrefs.SetInt("Cannon2", 7);
            }
        }
    }
    public void Select()
    {
        if(cannonNum1 != 0)
        {
            selectCannons[cannonNum1 - 1].SetActive(true);
        }

        if(cannonNum2 != 0)
        {
            selectCannons[cannonNum2 - 1].SetActive(true);
        }
    }

    public void ChoiceReset()
    {
        playerNum = 0;
        cannonNum1 = 0;
        cannonNum2 = 0;

        Destroy(player);
        Destroy(cannon1);
        Destroy(cannon2);

        playerWindow.SetActive(true);
        cannonWindow.SetActive(false);

        foreach(GameObject select in selectCannons)
        {
            select.SetActive(false);
        }
    }

    public void GameStart()
    {
        LodingController.LoadNextScene("Game");
    }
}
