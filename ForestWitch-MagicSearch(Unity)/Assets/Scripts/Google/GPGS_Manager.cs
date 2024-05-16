using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class GPGS_Manager : MonoBehaviour
{
    private GameDatas gameDatas;
    private AdventureLevel adventureLevel;
    private StoryScript story;
    private AbilityUI abilityUI;
    private GameLevel gameLevel;

    public GameObject loginUI;
    public GameObject noneLoginUI;

    public bool login;

    void Start()
    {
        login = false;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
            adventureLevel = GameObject.Find("Manager").GetComponent<AdventureLevel>();
            story = GameObject.Find("Manager").GetComponent<StoryScript>();
            abilityUI = GameObject.Find("Manager").GetComponent<AbilityUI>();
            gameLevel = GameObject.Find("Manager").GetComponent<GameLevel>();
        }

        PlayGamesPlatform.Activate();
    }

    public void GPGS_LogIn()
    {
        if (!login)
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
            login = true;
        }
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success) // 로그인 성공시 저장한 데이터
        {
            noneLoginUI.SetActive(false);
            loginUI.SetActive(true);

            gameDatas.LoadData();

            adventureLevel.LoadAbilityLevelData();
            story.LoadStoryData();
            abilityUI.LoadAbilityUIData();
            gameLevel.LoadGameLevelData();
        }
        else // 실패시 기초 데이터
        {
            noneLoginUI.SetActive(false);
            loginUI.SetActive(true);

            gameDatas.BasicData();
        }
    }

    void OnApplicationQuit()
    {
        gameDatas.SaveFieldData("ability1Num", gameDatas.dataSettings.ability1Num);
        gameDatas.SaveFieldData("ability2Num", gameDatas.dataSettings.ability2Num);
        gameDatas.SaveFieldData("ability3Num", gameDatas.dataSettings.ability3Num);
        gameDatas.SaveFieldData("ability4Num", gameDatas.dataSettings.ability4Num);
        gameDatas.SaveFieldData("ability5Num", gameDatas.dataSettings.ability5Num);
        gameDatas.SaveFieldData("ability6Num", gameDatas.dataSettings.ability6Num);
        
        gameDatas.SaveFieldData("maxLevel", gameDatas.dataSettings.maxLevel);
        gameDatas.SaveFieldData("adventLevel", gameDatas.dataSettings.adventLevel);

        gameDatas.SaveFieldData("onStory", gameDatas.dataSettings.onStory);


        gameDatas.SaveFieldData("maxExp", gameDatas.dataSettings.maxExp);
        gameDatas.SaveFieldData("currentExp", gameDatas.dataSettings.currentExp);
        gameDatas.SaveFieldData("gameLevel", gameDatas.dataSettings.gameLevel);
        
        gameDatas.SaveFieldData("playerNum", gameDatas.dataSettings.playerNum);
        gameDatas.SaveFieldData("cannonNum1", gameDatas.dataSettings.cannonNum1);
        gameDatas.SaveFieldData("cannonNum2", gameDatas.dataSettings.cannonNum2);

        gameDatas.SaveFieldData("bgmVolume", gameDatas.dataSettings.bgmVolume);
        gameDatas.SaveFieldData("fncVolume", gameDatas.dataSettings.fncVolume);
        gameDatas.SaveFieldData("monsterVolume", gameDatas.dataSettings.monsterVolume);
    }
}
