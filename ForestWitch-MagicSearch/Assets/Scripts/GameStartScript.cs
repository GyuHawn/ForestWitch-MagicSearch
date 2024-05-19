using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    private GameDatas gameDatas;
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
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    void Update()
    {
        Select();

        if (cannonNum1 != 0)
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
            gameDatas.SaveFieldData("playerNum", playerNum);
        }
    }
    public void Player2()
    {
        if (playerNum == 0)
        {
            playerNum = 2;
            gameDatas.SaveFieldData("playerNum", playerNum);
        }
    }

    public void Cannon1()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 1;
                gameDatas.SaveFieldData("cannonNum1", cannonNum1);
                showCannonPos[0].sprite = showCannons[0];
            }
            else
            {
                cannonNum2 = 1;
                gameDatas.SaveFieldData("cannonNum2", cannonNum2);
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
                gameDatas.SaveFieldData("cannonNum1", cannonNum1);
                showCannonPos[0].sprite = showCannons[1];
            }
            else
            {
                cannonNum2 = 2;
                gameDatas.SaveFieldData("cannonNum2", cannonNum2);
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
                gameDatas.SaveFieldData("cannonNum1", cannonNum1);
                showCannonPos[0].sprite = showCannons[2];
            }
            else
            {
                cannonNum2 = 3;
                gameDatas.SaveFieldData("cannonNum2", cannonNum2);
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
                gameDatas.SaveFieldData("cannonNum1", cannonNum1);
                showCannonPos[0].sprite = showCannons[3];
            }
            else
            {
                cannonNum2 = 4;
                gameDatas.SaveFieldData("cannonNum2", cannonNum2);
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
                gameDatas.SaveFieldData("cannonNum1", cannonNum1);
                showCannonPos[0].sprite = showCannons[4];
            }
            else
            {
                cannonNum2 = 5;
                gameDatas.SaveFieldData("cannonNum2", cannonNum2);
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
                gameDatas.SaveFieldData("cannonNum1", cannonNum1);
                showCannonPos[0].sprite = showCannons[5];
            }
            else
            {
                cannonNum2 = 6;
                gameDatas.SaveFieldData("cannonNum2", cannonNum2);
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
                gameDatas.SaveFieldData("cannonNum1", cannonNum1);
                showCannonPos[0].sprite = showCannons[6];
            }
            else
            {
                cannonNum2 = 7;
                gameDatas.SaveFieldData("cannonNum2", cannonNum2);
                showCannonPos[1].sprite = showCannons[6];
            }
        }
    }
    public void Select()
    {
        if (playerNum != 0)
        {
            selectPlayers[playerNum - 1].SetActive(true);
        }

        if (cannonNum1 != 0)
        {
            selectCannons[cannonNum1 - 1].SetActive(true);
        }

        if (cannonNum2 != 0)
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
        gameDatas.DeleteData();

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
