using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLevel : MonoBehaviour
{
    // 레벨 
    public int gameLevel;
    public TMP_Text gameLevelText;
    public TMP_Text gameExpText;
    public int maxExp;
    public int currentExp;

    // 캐릭터 락
    public GameObject playerLock;
    public GameObject[] cannonLocks;

    void Start()
    {
        /*currentExp = PlayerPrefs.GetInt("GameExp");
        maxExp = PlayerPrefs.GetInt("MaxExp", 50);
        gameLevel = PlayerPrefs.GetInt("GameLevel", 1);*/
        GPGSBinder.Inst.LoadCloud("GameExp", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                currentExp = loadedAbility1;
            }
            else
            {
                currentExp = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("MaxExp", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                maxExp = loadedAbility1;
            }
            else
            {
                maxExp = 50;
            }
        });
        GPGSBinder.Inst.LoadCloud("GameLevel", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                gameLevel = loadedAbility1;
            }
            else
            {
                gameLevel = 1;
            }
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

            //PlayerPrefs.SetInt("GameLevel", gameLevel);
            GPGSBinder.Inst.SaveCloud("GameLevel", gameLevel.ToString(), (success) => { });

            SettingMaxExp();
        }

        Unlocked();
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
        //PlayerPrefs.SetInt("MaxExp", maxExp);
        GPGSBinder.Inst.SaveCloud("MaxExp", maxExp.ToString(), (success) => { });
    }
}
