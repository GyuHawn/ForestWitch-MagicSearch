using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;

public class AdventureLevel : MonoBehaviour
{
    public int maxLevel;
    public int currentLevel;

    public TMP_Text currentLevelText;

    void Start()
    {
        maxLevel = PlayerPrefs.GetInt("MaxAdventLevel", 1);
        currentLevel = PlayerPrefs.GetInt("AdventLevel", 1);
    }

    
    void Update()
    {
        currentLevelText.text = "모험 " + currentLevel + "레벨";
    }

    public void NextLevel()
    {
        if(maxLevel > currentLevel)
        {
            currentLevel++;
            PlayerPrefs.SetInt("AdventLevel", currentLevel);
        }
    }
    public void BeforeLevel()
    {
        if(currentLevel != 1)
        {
            currentLevel--;
            PlayerPrefs.SetInt("AdventLevel", currentLevel);
        }
    }


}
