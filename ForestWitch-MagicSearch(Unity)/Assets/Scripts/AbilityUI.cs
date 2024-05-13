/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    private GameLevel gameLevel;
    private  GameDatas gameDatas;

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
    public GameObject[] ability1Check;
    public int ability1Num;
    public GameObject[] ability2Check;
    public int ability2Num;
    public GameObject[] ability3Check;
    public int ability3Num;
    public GameObject[] ability4Check;
    public int ability4Num;
    public GameObject[] ability5Check;
    public int ability5Num;
    public GameObject[] ability6Check;
    public int ability6Num;

    public GameObject[] locks; // 능력 잠금

    public bool on = true;

    public GameObject abilityEx;
    public TMP_Text abilityExText;

    private void Awake()
    {
        gameLevel = GameObject.Find("Manager").GetComponent<GameLevel>();
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    public void LoadAbilityUIData()
    {
        gameDatas.LoadFieldData<int>("ability1Num", value => {
            ability1Num = value;
        }, () => {
            ability1Num = 0;
        });
        gameDatas.LoadFieldData<int>("ability2Num", value => {
            ability2Num = value;
        }, () => {
            ability2Num = 0;
        });
        gameDatas.LoadFieldData<int>("ability3Num", value => {
            ability3Num = value;
        }, () => {
            ability3Num = 0;
        });
        gameDatas.LoadFieldData<int>("ability4Num", value => {
            ability4Num = value;
        }, () => {
            ability4Num = 0;
        });
        gameDatas.LoadFieldData<int>("ability5Num", value => {
            ability5Num = value;
        }, () => {
            ability5Num = 0;
        });
        gameDatas.LoadFieldData<int>("ability6Num", value => {
            ability6Num = value;
        }, () => {
            ability6Num = 0;
        });
    }
    

    void Update()
    {
        AbilityLock(); // 게임 레벨에 따라 능력오픈
        CheckAbility(); // 선택한 능력 표시
    }*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    private GameLevel gameLevel;
    private GameDatas gameDatas;

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
    public GameObject[] ability1Check;
    public GameObject[] ability2Check;
    public GameObject[] ability3Check;
    public GameObject[] ability4Check;
    public GameObject[] ability5Check;
    public GameObject[] ability6Check;

    public GameObject[] locks; // 능력 잠금

    public bool on = true;

    public GameObject abilityEx;
    public TMP_Text abilityExText;

    private void Awake()
    {
        gameLevel = GameObject.Find("Manager").GetComponent<GameLevel>();
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();

        // Load ability data from gameDatas
    }

    public void LoadAbilityUIData()
    {
        ability1Num = gameDatas.dataSettings.ability1Num;
        ability2Num = gameDatas.dataSettings.ability2Num;
        ability3Num = gameDatas.dataSettings.ability3Num;
        ability4Num = gameDatas.dataSettings.ability4Num;
        ability5Num = gameDatas.dataSettings.ability5Num;
        ability6Num = gameDatas.dataSettings.ability6Num;
    }

    void Update()
    {
        AbilityLock(); // 게임 레벨에 따라 능력 오픈
        CheckAbility(); // 선택한 능력 표시
    }
    void CheckAbility()
    {
        if (ability1Num == 1)
        {
            ability1Check[0].SetActive(true);
            ability1Check[1].SetActive(false);
        }
        else if (ability1Num == 2)
        {
            ability1Check[0].SetActive(false);
            ability1Check[1].SetActive(true);
        }
        else
        {
            ability1Check[0].SetActive(false);
            ability1Check[1].SetActive(false);
        }

        if (ability2Num == 1)
        {
            ability2Check[0].SetActive(true);
            ability2Check[1].SetActive(false);
        }
        else if (ability2Num == 2)
        {
            ability2Check[0].SetActive(false);
            ability2Check[1].SetActive(true);
        }
        else
        {
            ability2Check[0].SetActive(false);
            ability2Check[1].SetActive(false);
        }

        if (ability3Num == 1)
        {
            ability3Check[0].SetActive(true);
            ability3Check[1].SetActive(false);
        }
        else if (ability3Num == 2)
        {
            ability3Check[0].SetActive(false);
            ability3Check[1].SetActive(true);
        }
        else
        {
            ability3Check[0].SetActive(false);
            ability3Check[1].SetActive(false);
        }

        if (ability4Num == 1)
        {
            ability4Check[0].SetActive(true);
            ability4Check[1].SetActive(false);
        }
        else if (ability4Num == 2)
        {
            ability4Check[0].SetActive(false);
            ability4Check[1].SetActive(true);
        }
        else
        {
            ability4Check[0].SetActive(false);
            ability4Check[1].SetActive(false);
        }

        if (ability5Num == 1)
        {
            ability5Check[0].SetActive(true);
            ability5Check[1].SetActive(false);
        }
        else if (ability5Num == 2)
        {
            ability5Check[0].SetActive(false);
            ability5Check[1].SetActive(true);
        }
        else
        {
            ability5Check[0].SetActive(false);
            ability5Check[1].SetActive(false);
        }

        if (ability6Num == 1)
        {
            ability6Check[0].SetActive(true);
            ability6Check[1].SetActive(false);
        }
        else if (ability6Num == 2)
        {
            ability6Check[0].SetActive(false);
            ability6Check[1].SetActive(true);
        }
        else
        {
            ability6Check[0].SetActive(false);
            ability6Check[1].SetActive(false);
        }
    }

    public void AbilityLock() // 게임 레벨에 따라 능력오픈
    {
        if (!on)
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
        else
        {
            locks[0].SetActive(false);
            locks[1].SetActive(false);
            locks[2].SetActive(false);
            locks[3].SetActive(false);
            locks[4].SetActive(false);
            locks[5].SetActive(false);
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
            abilityEx.SetActive(false);
            abilityExText.text = "";
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

        if (!on)
        {
            ability.gameObject.SetActive(false);
        }
    }



    public void Ability1_1()
    {
        abilityEx.SetActive(true);
        ability1Num = 1;
        abilityExText.text = "총알을 획득시 30%의 확률로\n추가 총알을 1개 더 획득합니다.";
        gameDatas.UpdateAbility("ability1Num", ability1Num);
    }
    public void Ability1_2()
    {
        abilityEx.SetActive(true);
        ability1Num = 2;
        abilityExText.text = "총알을 획득시 10%의 확률로\n모든 대포가 1개의 총알을 장전합니다.";
        gameDatas.UpdateAbility("ability1Num", ability1Num);
    }
    public void Ability2_1()
    {
        abilityEx.SetActive(true);
        ability2Num = 1;
        abilityExText.text = "총알을 획득시 30%의 확률로\n마력으로 만든 나비를 생성하여 공격 합니다.";
        gameDatas.UpdateAbility("ability2Num", ability2Num);
    }
    public void Ability2_2()
    {
        abilityEx.SetActive(true);
        ability2Num = 2;
        abilityExText.text = "공격시 30%의 확률로\n마력으로 만든 나비를 생성하여 공격 합니다.";
        gameDatas.UpdateAbility("ability2Num", ability2Num);
    }
    public void Ability3_1()
    {
        abilityEx.SetActive(true);
        ability3Num = 1;
        abilityExText.text = "돈을 획득시 50%의 확률로\n획득한 돈의 절반 or 2배로 획득 합니다.";
        gameDatas.UpdateAbility("ability3Num", ability3Num);
    }
    public void Ability3_2()
    {
        abilityEx.SetActive(true);
        ability3Num = 2;
        abilityExText.text = "돈을 획득시 50% 추가로 획득합니다.";
        gameDatas.UpdateAbility("ability3Num", ability3Num);
    }
    public void Ability4_1()
    {
        abilityEx.SetActive(true);
        ability4Num = 1;
        abilityExText.text = "공격시 30%의 확률로 1회 추가 공격을 합니다.";
        gameDatas.UpdateAbility("ability4Num", ability4Num);
    }
    public void Ability4_2()
    {
        abilityEx.SetActive(true);
        ability4Num = 2;
        abilityExText.text = "공격시 80%의 확률로\n마력으로 만든 나비를 생성하여 공격 합니다.";
        gameDatas.UpdateAbility("ability4Num", ability4Num);
    }
    public void Ability5_1()
    {
        abilityEx.SetActive(true);
        ability5Num = 1;
        abilityExText.text = "게임 중 1회 체력이 25% 이하로 줄어들시\n 자동으로 체력의 50%를 회복합니다.";
        gameDatas.UpdateAbility("ability5Num", ability5Num);
    }
    public void Ability5_2()
    {
        abilityEx.SetActive(true);
        ability5Num = 2;
        abilityExText.text = "피격 시 일정 확률로 받은 피해를 무시합니다.";
        gameDatas.UpdateAbility("ability5Num", ability5Num);
    }
    public void Ability6_1()
    {
        abilityEx.SetActive(true);
        ability6Num = 1;
        abilityExText.text = "피격 시마다 모든 대포가 총알 1개을 장전합니다.";
        gameDatas.UpdateAbility("ability6Num", ability6Num);
    }
    public void Ability6_2()
    {
        abilityEx.SetActive(true);
        ability6Num = 2;
        abilityExText.text = "피격 시마다 마력으로 만든 나비를 생성하여 2번 공격 합니다.";
        gameDatas.UpdateAbility("ability6Num", ability6Num);
    }

    public int ability1Num
    {
        get { return gameDatas.dataSettings.ability1Num; }
        set
        {
            gameDatas.dataSettings.ability1Num = value;
            gameDatas.SaveFieldData("ability1Num", value);
        }
    }
    public int ability2Num
    {
        get { return gameDatas.dataSettings.ability2Num; }
        set
        {
            gameDatas.dataSettings.ability2Num = value;
            gameDatas.SaveFieldData("ability2Num", value);
        }
    }
    public int ability3Num
    {
        get { return gameDatas.dataSettings.ability3Num; }
        set
        {
            gameDatas.dataSettings.ability3Num = value;
            gameDatas.SaveFieldData("ability3Num", value);
        }
    }
    public int ability4Num
    {
        get { return gameDatas.dataSettings.ability4Num; }
        set
        {
            gameDatas.dataSettings.ability4Num = value;
            gameDatas.SaveFieldData("ability4Num", value);
        }
    }
    public int ability5Num
    {
        get { return gameDatas.dataSettings.ability5Num; }
        set
        {
            gameDatas.dataSettings.ability5Num = value;
            gameDatas.SaveFieldData("ability5Num", value);
        }
    }
    public int ability6Num
    {
        get { return gameDatas.dataSettings.ability6Num; }
        set
        {
            gameDatas.dataSettings.ability6Num = value;
            gameDatas.SaveFieldData("ability6Num", value);
        }

    }
}

