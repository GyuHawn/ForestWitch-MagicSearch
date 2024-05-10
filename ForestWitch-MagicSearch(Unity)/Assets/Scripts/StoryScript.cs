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


    public GameObject story; // 스토리 UI오픈

    public int page; // 현재 페이지 수
    public GameObject[] storyImages; // 스토리 이미지
    public bool onStory; // 스토리를 볼 것 인가
    public Toggle storyToggle;

    void Awake()
    {
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        gameStartScript = GameObject.Find("Manager").GetComponent<GameStartScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        onStory = false;
        // PlayerPrefs.SetInt("Story", onStory ? 1 : 0);
        GPGSBinder.Inst.LoadCloud("Story", (success, data) => {
            if (success && int.TryParse(data, out int loadedValue))
            {
                onStory = loadedValue == 1;
            }
            else
            {
                onStory = false;
            }
        });
    }

    public void CheckGameStory()
    {
        page = 0;
        if (storyToggle.isOn && !onStory)
        {
            onStory = true;
            // PlayerPrefs.SetInt("Story", onStory ? 1 : 0);
            GPGSBinder.Inst.LoadCloud("Story", (success, data) => {
                if (success && int.TryParse(data, out int loadedValue))
                {
                    onStory = loadedValue == 1;
                }
                else
                {
                    onStory = false;
                }
            });
        }
        else if (!storyToggle.isOn && onStory)
        {
            onStory = false;
            // PlayerPrefs.SetInt("Story", onStory ? 1 : 0);
            GPGSBinder.Inst.LoadCloud("Story", (success, data) => {
                if (success && int.TryParse(data, out int loadedValue))
                {
                    onStory = loadedValue == 1;
                }
                else
                {
                    onStory = false;
                }
            });
        }
    }

    public void StartStory() // 전체 스토리 UI 활성화
    {
        story.SetActive(true);
        audioManager.b_MainMenu.Stop();
        audioManager.StoryAudio();
    }

    public void NextStory() // 한 페이지씩 스토리 진행
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
                    audioManager.b_Story.Stop();
                    audioManager.MainAudio();

                    page = 0;
                    storyImages[page].SetActive(true);
                    story.SetActive(false);
                    gameStartScript.selectWindow.SetActive(true);
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
                    audioManager.b_Story.Stop();
                    audioManager.MainAudio();

                    page = 0;
                    storyImages[page].SetActive(true);
                    story.SetActive(false);
                }
            }
        }
        else if(SceneManager.GetActiveScene().name == "Game")
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
