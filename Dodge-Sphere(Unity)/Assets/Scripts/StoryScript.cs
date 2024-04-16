using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour
{
    private AudioManager audioManager;

    private GameStartScript gameStartScript;

    public GameObject story; // 스토리 UI오픈

    public int page; // 현재 페이지 수
    public GameObject[] storyImages; // 스토리 이미지
    public bool onStory; // 스토리를 볼 것 인가
    public Toggle storyToggle;

    void Awake()
    {
        gameStartScript = GameObject.Find("Manager").GetComponent<GameStartScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void CheckGameStory()
    {
        if (storyToggle.isOn && !onStory)
        {
            onStory = true;
        }
        else if (!storyToggle.isOn && onStory)
        {
            onStory = false;
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
