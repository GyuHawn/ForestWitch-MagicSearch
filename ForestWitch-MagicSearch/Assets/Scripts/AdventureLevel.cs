using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdventureLevel : MonoBehaviour
{
    private GameDatas gameDatas;

    public TMP_Text currentLevelText;
    public TMP_Text maxLevelText;

    private void Awake()
    {
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    private void Start()
    {
        maxLevelText.text = "최대 " + maxLevel + "레벨";
        UpdateLevelText();
    }

    public void LoadAbilityLevelData()
    {
        maxLevel = gameDatas.dataSettings.maxLevel;
        currentLevel = gameDatas.dataSettings.adventLevel;
    }

    public int maxLevel
    {
        get { return gameDatas.dataSettings.maxLevel; }
        set
        {
            gameDatas.dataSettings.maxLevel = value;
            gameDatas.SaveFieldData("maxLevel", value);
        }
    }

    public int currentLevel
    {
        get { return gameDatas.dataSettings.adventLevel; }
        set
        {
            gameDatas.dataSettings.adventLevel = value;
            UpdateLevelText();
            gameDatas.SaveFieldData("currentLevel", value);
        }
    }

    void UpdateLevelText()
    {
        currentLevelText.text = "모험 " + currentLevel + "레벨";
    }

    public void NextLevel()
    {
        if (maxLevel > currentLevel)
        {
            currentLevel++;
            UpdateLevelText();
        }
    }

    public void BeforeLevel()
    {
        if (currentLevel > 1)
        {
            currentLevel--;
            UpdateLevelText();
        }
    }
}
