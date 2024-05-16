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
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    public void LoadGameLevelData()
    {
        gameLevel = gameDatas.dataSettings.gameLevel;
        maxExp = gameDatas.dataSettings.maxExp;
        currentExp = gameDatas.dataSettings.currentExp;

        UpdateUI();
        Unlocked();
    }

    void Update()
    {
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        gameLevel++;
        currentExp -= maxExp;

        SettingMaxExp();

        gameDatas.dataSettings.gameLevel = gameLevel;
        gameDatas.dataSettings.currentExp = currentExp;
        gameDatas.dataSettings.maxExp = maxExp;
        gameDatas.UpdateAbility("gameLevel", gameLevel);
        gameDatas.UpdateAbility("currentExp", currentExp);
        gameDatas.UpdateAbility("maxExp", maxExp);

        UpdateUI();
        Unlocked();
    }

    private void UpdateUI()
    {
        gameLevelText.text = gameLevel.ToString();
        gameExpText.text = $"{currentExp} / {maxExp}";
    }

    private void Unlocked()
    {
        playerLock.SetActive(gameLevel < 5);
        if (cannonLocks.Length > 0) cannonLocks[0].SetActive(gameLevel < 2);
        if (cannonLocks.Length > 1) cannonLocks[1].SetActive(gameLevel < 3);
        if (cannonLocks.Length > 2) cannonLocks[2].SetActive(gameLevel < 3);
        if (cannonLocks.Length > 3) cannonLocks[3].SetActive(gameLevel < 6);
        if (cannonLocks.Length > 4) cannonLocks[4].SetActive(gameLevel < 8);
    }

    private void SettingMaxExp()
    {
        maxExp = gameLevel * 50;
    }

    public int gameLevel
    {
        get { return gameDatas.dataSettings.gameLevel; }
        set
        {
            gameDatas.dataSettings.gameLevel = value;
            gameDatas.SaveFieldData("gameLevel", value);
            UpdateUI();
            Unlocked();
        }
    }

    public int maxExp
    {
        get { return gameDatas.dataSettings.maxExp; }
        set
        {
            gameDatas.dataSettings.maxExp = value;
            gameDatas.SaveFieldData("maxExp", value);
            UpdateUI();
        }
    }

    public int currentExp
    {
        get { return gameDatas.dataSettings.currentExp; }
        set
        {
            gameDatas.dataSettings.currentExp = value;
            gameDatas.SaveFieldData("currentExp", value);
            UpdateUI();
        }
    }
}
