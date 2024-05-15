using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
            adventureLevel = GameObject.Find("Manager").GetComponent<AdventureLevel>();
            story = GameObject.Find("Manager").GetComponent<StoryScript>();
            abilityUI = GameObject.Find("Manager").GetComponent<AbilityUI>();
            gameLevel = GameObject.Find("Manager").GetComponent<GameLevel>();
        }
    }

    public void GPGS_LogIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        login = true;
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (!login)
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
    }
}
