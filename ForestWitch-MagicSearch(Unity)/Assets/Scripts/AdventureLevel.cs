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
       // maxLevel = PlayerPrefs.GetInt("MaxAdventLevel", 1);
        GPGSBinder.Inst.LoadCloud("MaxAdventLevel", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                maxLevel = loadedAbility1;
            }
            else
            {
                maxLevel = 1;
            }
        });
        //currentLevel = PlayerPrefs.GetInt("AdventLevel", 1);
        GPGSBinder.Inst.LoadCloud("AdventLevel", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                currentLevel = loadedAbility1;
            }
            else
            {
                currentLevel = 1;   
            }
        });
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
           // PlayerPrefs.SetInt("AdventLevel", currentLevel);
            GPGSBinder.Inst.SaveCloud("AdventLevel", currentLevel.ToString(), (success) => {});
        }
    }
    public void BeforeLevel()
    {
        if(currentLevel != 1)
        {
            currentLevel--;
           // PlayerPrefs.SetInt("AdventLevel", currentLevel);
            GPGSBinder.Inst.SaveCloud("AdventLevel", currentLevel.ToString(), (success) => {});
        }
    }


}
