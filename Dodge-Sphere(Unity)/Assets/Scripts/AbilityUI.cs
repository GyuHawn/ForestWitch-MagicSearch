using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    public GameLevel gameLevel;

    // 능력 UI
    public GameObject abilityUI;
    public GameObject[] abilitys;
    public GameObject[] abilitys1;
    public GameObject[] abilitys2;
    public GameObject[] abilitys3;
    public GameObject[] abilitys4;
    public GameObject[] abilitys5;
    public GameObject[] abilitys6;
    
    // 선택한 능력
    public int ability1Num;
    public int ability2Num;
    public int ability3Num;
    public int ability4Num;
    public int ability5Num;
    public int ability6Num;

    public GameObject[] locks; // 능력 잠금

    public bool on = true;

    private void Awake()
    {
        gameLevel = GameObject.Find("Manager").GetComponent<GameLevel>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        AbilityLock(); // 게임 레벨에 따라 능력오픈
    }

    void AbilityLock() // 게임 레벨에 따라 능력오픈
    {
        if (gameLevel.gameLevel >= 2)
        {
            locks[0].SetActive(false);
        }
        else
        {
            locks[0].SetActive(true);
        }
        if (gameLevel.gameLevel >= 3)
        {
            locks[1].SetActive(false);
        }
        else
        {
            locks[1].SetActive(true);
        }
        if (gameLevel.gameLevel >= 5)
        {
            locks[2].SetActive(false);
        }
        else
        {
            locks[2].SetActive(true);
        }
        if (gameLevel.gameLevel >= 6)
        {
            locks[3].SetActive(false);
        }
        else
        {
            locks[3].SetActive(true);
        }
        if (gameLevel.gameLevel >= 8)
        {
            locks[4].SetActive(false);
        }
        else
        {
            locks[4].SetActive(true);
        }
        if (gameLevel.gameLevel >= 9)
        {
            locks[5].SetActive(false);
        }
        else
        {
            locks[5].SetActive(true);
        }
    }

    public void OnAbility() // UI 이동
    {
        if (on)
        {
            on = false;

            for (int i = 0; i < abilitys.Length; i++)
            {
                StartCoroutine(MoveAbility_X(abilitys[i], -100 * (i + 1), true));
            }

            foreach (GameObject abili in abilitys1)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys2)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys3)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys4)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys5)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys6)
            {
                abili.SetActive(true);
            }     
        }
        else
        {
            on = true;

            for (int i = 0; i < abilitys.Length; i++)
            {
                StartCoroutine(MoveAbility_X(abilitys[i], +100 * (i + 1), false));
            }

            foreach (GameObject abili in abilitys1)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys2)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys3)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys4)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys5)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys6)
            {
                abili.SetActive(false);
            }        
        }
    }

    IEnumerator MoveAbility_X(GameObject ability, float moveX, bool on) // 부드럽게 UI이동
    {
        if (on)
        {
            ability.gameObject.SetActive(true);
        }

        RectTransform rectTransform = ability.GetComponent<RectTransform>();
        Vector2 targetPosition = new Vector2(rectTransform.anchoredPosition.x + moveX, rectTransform.anchoredPosition.y);

        while (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) > 0.05f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, 300f * Time.deltaTime);
            yield return null;
        }

        if(!on)
        {
            ability.gameObject.SetActive(false);
        }
    }

    public void Ability1_1()
    {
        PlayerPrefs.SetInt("Ability1", 1);
    }
    public void Ability1_2()
    {
        PlayerPrefs.SetInt("Ability1", 2);
    }
    public void Ability2_1()
    {
        PlayerPrefs.SetInt("Ability2", 1);
    }
    public void Ability2_2()
    {
        PlayerPrefs.SetInt("Ability2", 2);
    }
    public void Ability3_1()
    {
        PlayerPrefs.SetInt("Ability3", 1);
    }
    public void Ability3_2()
    {
        PlayerPrefs.SetInt("Ability3", 2);
    }
    public void Ability4_1()
    {
        PlayerPrefs.SetInt("Ability4", 1);
    }
    public void Ability4_2()
    {
        PlayerPrefs.SetInt("Ability4", 2);
    }
    public void Ability5_1()
    {
        PlayerPrefs.SetInt("Ability5", 1);
    }
    public void Ability5_2()
    {
        PlayerPrefs.SetInt("Ability5", 2);
    }
    public void Ability6_1()
    {
        PlayerPrefs.SetInt("Ability6", 1);
    }
    public void Ability6_2()
    {
        PlayerPrefs.SetInt("Ability6", 2);
    }
}
