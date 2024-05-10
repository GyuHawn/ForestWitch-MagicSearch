using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    private StoryScript storyScript;
    private GameLevel gameLevel;
    private AdventureLevel adventureLevel;
    private AbilityUI abilityUI;
    private AudioManager audioManager;

    public GameObject selectWindow;
    public GameObject startButton;

    public int playerNum;
    public int cannonNum1;
    public int cannonNum2;

    public GameObject playerPos;
    public GameObject cannon1Pos;
    public GameObject cannon2Pos;

    public GameObject[] selectPlayers;
    public GameObject[] selectCannons;

    public Sprite[] showCannons; 
    public Image[] showCannonPos; 

    public GameObject cannon1;
    public GameObject cannon2;

    public GameObject startSelect;

    public GameObject settingUI;
    public GameObject resetBtn;
    public GameObject resetUI;
    public GameObject copyrightUI;

    void Awake()
    {
        storyScript = GameObject.Find("Manager").GetComponent<StoryScript>();
        gameLevel = GameObject.Find("Manager").GetComponent<GameLevel>();
        adventureLevel = GameObject.Find("Manager").GetComponent<AdventureLevel>();
        abilityUI = GameObject.Find("Manager").GetComponent<AbilityUI>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        Select();

        if(cannonNum1 != 0)
        {
            showCannonPos[0].enabled = true;
        }
        else
        {
            showCannonPos[0].enabled = false;
        }
        if (cannonNum2 != 0)
        {
            showCannonPos[1].enabled = true;
        }
        else
        {
            showCannonPos[1].enabled = false;
        }

        if (playerNum != 0 && cannonNum1 != 0 && cannonNum2 != 0)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void OpenSelectWindow()
    {
        audioManager.ButtonAudio();
        StartCoroutine(StartScale());
    }


    IEnumerator StartScale()
    {
        float time = 0;
        Vector3 originalScale = startSelect.transform.localScale;
        Vector3 targetScale = new Vector3(originalScale.x, 0, originalScale.z);

        while (time < 1)
        {
            startSelect.transform.localScale = Vector3.Lerp(originalScale, targetScale, time);
            time += Time.deltaTime;
            yield return null;
        }

        startSelect.transform.localScale = targetScale;

        SelectCharacter();

        startSelect.transform.localScale = new Vector3(1, 1, 1);
    }

    void SelectCharacter() // 선택창 오픈
    {
        if (storyScript.onStory)
        {
            storyScript.StartStory();
        }
        else
        {
            selectWindow.SetActive(true);
        }
    }

    public void ExitSelect() // 선택창 나가기
    {
        selectWindow.SetActive(false);
    }

    public void Player1()
    {
        if (playerNum == 0)
        {
            playerNum = 1;
            //PlayerPrefs.SetInt("Player", 1);
            GPGSBinder.Inst.SaveCloud("Player", playerNum.ToString(), (success) => { });
        }
    }
    public void Player2()
    {
        if (playerNum == 0)
        {
            playerNum = 2;
            //PlayerPrefs.SetInt("Player", 2);
            GPGSBinder.Inst.SaveCloud("Player", playerNum.ToString(), (success) => { });
        }
    }

    public void Cannon1()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 1;
                //PlayerPrefs.SetInt("Cannon1", 1);
                GPGSBinder.Inst.SaveCloud("Cannon1", cannonNum1.ToString(), (success) => { });
                showCannonPos[0].sprite = showCannons[0];
            }
            else
            {
                cannonNum2 = 1;
                //PlayerPrefs.SetInt("Cannon2", 1);
                GPGSBinder.Inst.SaveCloud("Cannon2", cannonNum2.ToString(), (success) => { });
                showCannonPos[1].sprite = showCannons[0];
            }
        }
    }
    public void Cannon2()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 2;
                // PlayerPrefs.SetInt("Cannon1", 2);
                GPGSBinder.Inst.SaveCloud("Cannon1", cannonNum1.ToString(), (success) => { });
                showCannonPos[0].sprite = showCannons[1];
            }
            else
            {
                cannonNum2 = 2;
                //PlayerPrefs.SetInt("Cannon2", 2);
                GPGSBinder.Inst.SaveCloud("Cannon2", cannonNum2.ToString(), (success) => { });
                showCannonPos[1].sprite = showCannons[1];
            }
        }
    }
    public void Cannon3()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 3;
                //PlayerPrefs.SetInt("Cannon1", 3);
                GPGSBinder.Inst.SaveCloud("Cannon1", cannonNum1.ToString(), (success) => { });
                showCannonPos[0].sprite = showCannons[2];
            }
            else
            {
                cannonNum2 = 3;
                //PlayerPrefs.SetInt("Cannon2", 3);
                GPGSBinder.Inst.SaveCloud("Cannon2", cannonNum2.ToString(), (success) => { });
                showCannonPos[1].sprite = showCannons[2];
            }
        }
    }
    public void Cannon4()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 4;
                //PlayerPrefs.SetInt("Cannon1", 4);
                GPGSBinder.Inst.SaveCloud("Cannon1", cannonNum1.ToString(), (success) => { });
                showCannonPos[0].sprite = showCannons[3];
            }
            else
            {
                cannonNum2 = 4;
                //PlayerPrefs.SetInt("Cannon2", 4);
                GPGSBinder.Inst.SaveCloud("Cannon2", cannonNum2.ToString(), (success) => { });
                showCannonPos[1].sprite = showCannons[3];
            }
        }
    }
    public void Cannon5()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 5;
                //PlayerPrefs.SetInt("Cannon1", 5);
                GPGSBinder.Inst.SaveCloud("Cannon1", cannonNum1.ToString(), (success) => { });
                showCannonPos[0].sprite = showCannons[4];
            }
            else
            {
                cannonNum2 = 5;
                //PlayerPrefs.SetInt("Cannon2", 5);
                GPGSBinder.Inst.SaveCloud("Cannon2", cannonNum2.ToString(), (success) => { });
                showCannonPos[1].sprite = showCannons[4];
            }
        }
    }
    public void Cannon6()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 6;
                //PlayerPrefs.SetInt("Cannon1", 6);
                GPGSBinder.Inst.SaveCloud("Cannon1", cannonNum1.ToString(), (success) => { });
                showCannonPos[0].sprite = showCannons[5];
            }
            else
            {
                cannonNum2 = 6;
                //PlayerPrefs.SetInt("Cannon2", 6);
                GPGSBinder.Inst.SaveCloud("Cannon2", cannonNum2.ToString(), (success) => { });
                showCannonPos[1].sprite = showCannons[5];
            }
        }
    }
    public void Cannon7()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 7;
                //PlayerPrefs.SetInt("Cannon1", 7);
                GPGSBinder.Inst.SaveCloud("Cannon1", cannonNum1.ToString(), (success) => { });
                showCannonPos[0].sprite = showCannons[6];
            }
            else
            {
                cannonNum2 = 7;
                //PlayerPrefs.SetInt("Cannon2", 7);
                GPGSBinder.Inst.SaveCloud("Cannon2", cannonNum2.ToString(), (success) => { });
                showCannonPos[1].sprite = showCannons[6];
            }
        }
    }
    public void Select()
    {
        if(playerNum != 0)
        {
            selectPlayers[playerNum - 1].SetActive(true);
        }

        if(cannonNum1 != 0)
        {
            selectCannons[cannonNum1 - 1].SetActive(true);
        }

        if(cannonNum2 != 0)
        {
            selectCannons[cannonNum2 - 1].SetActive(true);
        }
    }

    public void ChoiceReset()
    {
        playerNum = 0;
        cannonNum1 = 0;
        cannonNum2 = 0;

        foreach (GameObject select in selectPlayers)
        {
            select.SetActive(false);
        }

        foreach (GameObject select in selectCannons)
        {
            select.SetActive(false);
        }

        showCannonPos[0].sprite = null;
        showCannonPos[1].sprite = null;
    }

    public void GameStart()
    {
        LoadingController.LoadNextScene("Game");
    }

    public void Setting() // 세팅 UI 온/오프
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }

    public void DataReset() // 데이터 초기화
    {
        resetUI.SetActive(true);
        resetBtn.SetActive(false);
    }
    public void Reset_O()
    {
        PlayerPrefs.DeleteAll();

        // 게임레벨 초기화
        gameLevel.gameLevel = 1;
        //PlayerPrefs.SetInt("GameLevel", gameLevel.gameLevel);
        GPGSBinder.Inst.SaveCloud("GameLevel", gameLevel.gameLevel.ToString(), (success) => { });
        
        gameLevel.currentExp = 0;
        gameLevel.maxExp = 50;

        // 모험레벨 초기화
        adventureLevel.currentLevel = 1;
        //PlayerPrefs.SetInt("AdventLevel", adventureLevel.currentLevel);
        GPGSBinder.Inst.SaveCloud("AdventLevel", adventureLevel.currentLevel.ToString(), (success) => { });
        adventureLevel.maxLevel = 1;
        //PlayerPrefs.SetInt("MaxAdventLevel", adventureLevel.maxLevel);
        GPGSBinder.Inst.SaveCloud("MaxAdventLevel", adventureLevel.maxLevel.ToString(), (success) => { });

        // 능력 초기화
        abilityUI.ability1Num = 0;
        //PlayerPrefs.SetInt("Ability1", abilityUI.ability1Num);
        GPGSBinder.Inst.SaveCloud("Ability1", abilityUI.ability1Num.ToString(), (success) => { });
        abilityUI.ability2Num = 0;
        //PlayerPrefs.SetInt("Ability2", abilityUI.ability2Num);
        GPGSBinder.Inst.SaveCloud("Ability2", abilityUI.ability2Num.ToString(), (success) => { });
        abilityUI.ability3Num = 0;
        //PlayerPrefs.SetInt("Ability3", abilityUI.ability3Num);
        GPGSBinder.Inst.SaveCloud("Ability3", abilityUI.ability3Num.ToString(), (success) => { });
        abilityUI.ability4Num = 0;
        //PlayerPrefs.SetInt("Ability4", abilityUI.ability4Num);
        GPGSBinder.Inst.SaveCloud("Ability4", abilityUI.ability4Num.ToString(), (success) => { });
        abilityUI.ability5Num = 0;
        //PlayerPrefs.SetInt("Ability5", abilityUI.ability5Num);
        GPGSBinder.Inst.SaveCloud("Ability5", abilityUI.ability5Num.ToString(), (success) => { });
        abilityUI.ability6Num = 0;
        //PlayerPrefs.SetInt("Ability6", abilityUI.ability6Num);
        GPGSBinder.Inst.SaveCloud("Ability6", abilityUI.ability6Num.ToString(), (success) => { });

        resetBtn.SetActive(true);
        resetUI.SetActive(false);
        settingUI.SetActive(false);
    }
    public void Reset_x()
    {
        resetBtn.SetActive(true);
        resetUI.SetActive(false);
    }

    public void Copyright() // 저작권 표시 UI 온
    {
        copyrightUI.SetActive(!copyrightUI.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
