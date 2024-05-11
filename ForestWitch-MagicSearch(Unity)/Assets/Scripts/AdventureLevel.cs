using System.Collections;
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
        gameDatas = GameObject.Find("Manager").GetComponent<GameDatas>();
    }

    void Start()
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
