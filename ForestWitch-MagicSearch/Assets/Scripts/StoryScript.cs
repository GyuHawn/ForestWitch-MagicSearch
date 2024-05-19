using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour
{
    private AudioManager audioManager;
    private ClearInfor clearInfor;
    private GameStartScript gameStartScript;
    private GameDatas gameDatas;

    public GameObject story; // Story UI popup
    public int page; // Current page number
    public GameObject[] storyImages; // Story images
    public bool onStory; // Is the story being viewed?
    public Toggle storyToggle;

    void Awake()
    {
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        gameStartScript = GameObject.Find("Manager").GetComponent<GameStartScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();       
    }

    public void LoadStoryData()
    {
        onStory = gameDatas.dataSettings.onStory;
    }

    public void CheckGameStory()
    {
        page = 0;
        if (storyToggle.isOn && !onStory)
        {
            onStory = true;
        }
        else if (!storyToggle.isOn && onStory)
        {
            onStory = false;
        }

        gameDatas.dataSettings.onStory = onStory;
        gameDatas.UpdateAbility("onStory", onStory);
        //gameDatas.SaveFieldData("onStory", onStory);
    }

    public void StartStory() // Activate the whole story UI
    {
        story.SetActive(true);
        audioManager.b_MainMenu.Stop();
        audioManager.StoryAudio();
    }

    public void NextStory() // Progress story one page at a time
    {
        storyImages[page].SetActive(false);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (onStory)
            {
                if (page <= 3)
                {
                    page++;
                    storyImages[page].SetActive(true);
                }
                else if (page > 3)
                {
                    EndMainMenuStory();
                }
            }
            else
            {
                if (page <= 7)
                {
                    page++;
                    storyImages[page].SetActive(true);
                }
                else if (page > 7)
                {
                    EndMainMenuStory();
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            if (clearInfor.clear && clearInfor.onStory)
            {
                if (page <= 3)
                {
                    page++;
                    if (page < 4)
                    {
                        storyImages[page].SetActive(true);
                    }
                }
                if (page >= 4)
                {
                    story.SetActive(false);
                    audioManager.TileMapAudio();
                }
            }
        }
    }

    private void EndMainMenuStory()
    {
        audioManager.b_Story.Stop();
        audioManager.MainAudio();
        page = 0;
        storyImages[page].SetActive(true);
        story.SetActive(false);
        gameStartScript.selectWindow.SetActive(true);
    }

    public void AllStory()
    {
        audioManager.ButtonAudio();
        onStory = false;
        storyToggle.isOn = false;
        story.SetActive(true);
        audioManager.b_MainMenu.Stop();
        audioManager.StoryAudio();
    }
}
