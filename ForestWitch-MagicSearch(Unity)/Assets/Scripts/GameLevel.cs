using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLevel : MonoBehaviour
{
    private GameDatas gameDatas;

    // 레벨 
    public int gameLevel;
    public TMP_Text gameLevelText;
    public TMP_Text gameExpText;
    public int maxExp;
    public int currentExp;

    // 캐릭터 락
    public GameObject playerLock;
    public GameObject[] cannonLocks;

    private void Awake()
    {
        gameDatas = GameObject.Find("Manager").GetComponent<GameDatas>();
    }

    void Start()
    {
        gameDatas.LoadFieldData<int>("currentExp", value => {
            currentExp = value;
        }, () => {
            currentExp = 0;
        });
        gameDatas.LoadFieldData<int>("maxExp", value => {
            maxExp = value;
        }, () => {
            maxExp = 50;
        });
        gameDatas.LoadFieldData<int>("gameLevel", value => {
            gameLevel = value;
        }, () => {
            gameLevel = 1;
        });
    }


    void Update()
    {
        gameLevelText.text = gameLevel.ToString();
        gameExpText.text = currentExp.ToString() + " / " + maxExp.ToString();
        if (currentExp > maxExp)
        {
            gameLevel++;
            currentExp -= maxExp;

            gameDatas.SaveFieldData("gameLevel", gameLevel);

            SettingMaxExp();
        }

        // Unlocked();
    }

    void Unlocked() // 레벨에 따른 잠금해제
    {
        if (gameLevel >= 2)
        {
            cannonLocks[0].SetActive(false);
        }
        else
        {
            cannonLocks[0].SetActive(true);
        }

        if (gameLevel >= 3)
        {
            cannonLocks[1].SetActive(false);
            cannonLocks[2].SetActive(false);
        }
        else
        {
            cannonLocks[1].SetActive(true);
            cannonLocks[2].SetActive(true);
        }

        if (gameLevel >= 5)
        {
            playerLock.SetActive(false);
        }
        else
        {
            playerLock.SetActive(true);
        }

        if (gameLevel >= 6)
        {
            cannonLocks[3].SetActive(false);
        }
        else
        {
            cannonLocks[3].SetActive(true);
        }

        if (gameLevel >= 8)
        {
            cannonLocks[4].SetActive(false);
        }
        else
        {
            cannonLocks[4].SetActive(true);
        }
    }

    void SettingMaxExp()
    {
        maxExp = gameLevel * 50;
        gameDatas.SaveFieldData("maxExp", maxExp);
    }
}
