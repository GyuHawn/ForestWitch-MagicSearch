using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLevel : MonoBehaviour
{
    public int gameLevel;
    public TMP_Text gameLevelText;
    public TMP_Text gameExpText;
    public int maxExp;
    public int currentExp;

    void Start()
    {
        currentExp = PlayerPrefs.GetInt("GameExp");
        maxExp = PlayerPrefs.GetInt("MaxExp", 100);
        gameLevel = PlayerPrefs.GetInt("GameLevel", 1);
    }

    
    void Update()
    {
        gameLevelText.text = gameLevel.ToString();
        gameExpText.text = currentExp.ToString() + " / " + maxExp.ToString(); 
        if (currentExp > maxExp)
        {
            gameLevel++;
            currentExp -= maxExp;

            PlayerPrefs.SetInt("GameLevel", gameLevel);

            SettingMaxExp();
        }
    }

    void SettingMaxExp()
    {
        maxExp = gameLevel * maxExp;
        PlayerPrefs.SetInt("MaxExp", maxExp);
    }
}
