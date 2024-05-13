/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;

public class AdventureLevel : MonoBehaviour
{
    private GameDatas gameDatas;

    public int maxLevel;
    public int currentLevel;

    public TMP_Text currentLevelText;

    private void Awake()
    {
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    public void LoadAbilityLevelData()
    {
        gameDatas.LoadFieldData<int>("maxLevel", value => {
            maxLevel = value;
        }, () => {
            maxLevel = 1;
        });

        gameDatas.LoadFieldData<int>("currentLevel", value => {
            currentLevel = value;
            UpdateLevelText();
        }, () => {
            currentLevel = 1;
        });
    }


    void UpdateLevelText()
    {
        currentLevelText.text = "모험 " + currentLevel + "레벨";
    }

    public void NextLevel()
    {
        if(maxLevel > currentLevel)
        {
            currentLevel++;
            UpdateLevelText();
            gameDatas.SaveFieldData("currentLevel", currentLevel);
        }
    }
    public void BeforeLevel()
    {
        if (currentLevel > 1)
        {
            currentLevel--;
            UpdateLevelText();
            gameDatas.SaveFieldData("currentLevel", currentLevel);
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdventureLevel : MonoBehaviour
{
    private GameDatas gameDatas;

    public TMP_Text currentLevelText;

    private void Awake()
    {
        // Get the GameDatas component from the GameObject named "GameData"
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();

        // You may want to call LoadData here if your game needs to immediately load settings on startup
        // Alternatively, this can be done externally if there are multiple initializations dependent on the loaded data
        gameDatas.LoadData();
    }

    private void Start()
    {
        // After the game data is loaded, we can assign it directly
        maxLevel = gameDatas.dataSettings.maxLevel;
        currentLevel = gameDatas.dataSettings.currentLevel;

        UpdateLevelText();
    }

    public void LoadAbilityLevelData()
    {
        // Directly accessing values from GameDatas
        maxLevel = gameDatas.dataSettings.maxLevel;
        currentLevel = gameDatas.dataSettings.currentLevel;
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
        get { return gameDatas.dataSettings.currentLevel; }
        set
        {
            gameDatas.dataSettings.currentLevel = value;
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
