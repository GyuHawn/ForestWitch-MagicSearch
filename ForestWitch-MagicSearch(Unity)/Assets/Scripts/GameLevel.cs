/*using System.Collections;
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
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    public void LoadGameLevelData()
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
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLevel : MonoBehaviour
{
    private GameDatas gameDatas;

    public TMP_Text gameLevelText;
    public TMP_Text gameExpText;

    // UI Elements for locking gameplay features
    public GameObject playerLock;
    public GameObject[] cannonLocks;

    private void Awake()
    {
        // Obtain the GameDatas component from the GameObject named "GameData"
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    public void LoadGameLevelData()
    {
        // Directly accessing values from GameDatas
        gameLevel = gameDatas.dataSettings.gameLevel;
        maxExp = gameDatas.dataSettings.maxExp;
        currentExp = gameDatas.dataSettings.currentExp;

        // Initialize the display texts
        UpdateUI();
    }

    void Update()
    {
        if (currentExp >= maxExp)
        {
            LevelUp();
        }

        UpdateUI();
        Unlocked();
    }

    private void LevelUp()
    {
        gameLevel++;
        currentExp -= maxExp;

        // Set new max experience needed for the next level
        SettingMaxExp();

        // Save updated data
        gameDatas.dataSettings.gameLevel = gameLevel;
        gameDatas.dataSettings.currentExp = currentExp;
        gameDatas.dataSettings.maxExp = maxExp;
        gameDatas.UpdateAbility("gameLevel", gameLevel);
        gameDatas.UpdateAbility("currentExp", currentExp);
        gameDatas.UpdateAbility("maxExp", maxExp);
    }

    void UpdateUI()
    {
        gameLevelText.text = "Level: " + gameLevel.ToString();
        gameExpText.text = $"{currentExp} / {maxExp}";
    }

    void Unlocked() // Unlock features based on the level
    {
        playerLock.SetActive(gameLevel < 5);
        cannonLocks[0].SetActive(gameLevel < 2);

        if (cannonLocks.Length > 1)
        {
            bool isLevel3 = gameLevel >= 3;
            for (int i = 1; i < 3 && i < cannonLocks.Length; i++)
            {
                cannonLocks[i].SetActive(!isLevel3);
            }
        }

        if (cannonLocks.Length > 3)
        {
            cannonLocks[3].SetActive(gameLevel < 6);
        }

        if (cannonLocks.Length > 4)
        {
            cannonLocks[4].SetActive(gameLevel < 8);
        }
    }

    void SettingMaxExp()
    {
        maxExp = gameLevel * 50; // For example, each level requires 50 more experience
    }

    // Properties for easy access and auto-saving
    public int gameLevel
    {
        get { return gameDatas.dataSettings.gameLevel; }
        set
        {
            gameDatas.dataSettings.gameLevel = value;
            gameDatas.SaveFieldData("gameLevel", value);
        }
    }

    public int maxExp
    {
        get { return gameDatas.dataSettings.maxExp; }
        set
        {
            gameDatas.dataSettings.maxExp = value;
            gameDatas.SaveFieldData("maxExp", value);
        }
    }

    public int currentExp
    {
        get { return gameDatas.dataSettings.currentExp; }
        set
        {
            gameDatas.dataSettings.currentExp = value;
            gameDatas.SaveFieldData("currentExp", value);
        }
    }
}
