using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    private StoryScript storyScript;
    private AudioManager audioManager;

    public GameObject selectWindow;
    public GameObject playerWindow;
    public GameObject cannonWindow;
    public GameObject startButton;

    public int playerNum;
    public int cannonNum1;
    public int cannonNum2;

    public GameObject playerPos;
    public GameObject cannon1Pos;
    public GameObject cannon2Pos;

    public GameObject[] selectCannons;

    public GameObject cannon1;
    public GameObject cannon2;

    public GameObject startSelect;

    void Awake()
    {
        storyScript = GameObject.Find("Manager").GetComponent<StoryScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
   
    void Update()
    {
        Select();

        if (playerNum != 0)
        {
            playerWindow.SetActive(false);
            cannonWindow.SetActive(true);
        }

        if(playerNum != 0 && cannonNum1 != 0 && cannonNum2 != 0)
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
    }

    void SelectCharacter()
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

    public void Player1()
    {
        playerNum = 1;
        PlayerPrefs.SetInt("Player", 1);
    }
    public void Player2()
    {
        playerNum = 2;
        PlayerPrefs.SetInt("Player", 2);
    }

    public void Cannon1()
    {
        if (cannonNum1 == 0 || cannonNum2 == 0)
        {
            if (cannonNum1 == 0)
            {
                cannonNum1 = 1;
                PlayerPrefs.SetInt("Cannon1", 1);
            }
            else
            {
                cannonNum2 = 1;
                PlayerPrefs.SetInt("Cannon2", 1);
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
                PlayerPrefs.SetInt("Cannon1", 2);
            }
            else
            {
                cannonNum2 = 2;
                PlayerPrefs.SetInt("Cannon2", 2);
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
                PlayerPrefs.SetInt("Cannon1", 3);
            }
            else
            {
                cannonNum2 = 3;
                PlayerPrefs.SetInt("Cannon2", 3);
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
                PlayerPrefs.SetInt("Cannon1", 4);
            }
            else
            {
                cannonNum2 = 4;
                PlayerPrefs.SetInt("Cannon2", 4);
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
                PlayerPrefs.SetInt("Cannon1", 5);
            }
            else
            {
                cannonNum2 = 5;
                PlayerPrefs.SetInt("Cannon2", 5);
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
                PlayerPrefs.SetInt("Cannon1", 6);
            }
            else
            {
                cannonNum2 = 6;
                PlayerPrefs.SetInt("Cannon2", 6);
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
                PlayerPrefs.SetInt("Cannon1", 7);
            }
            else
            {
                cannonNum2 = 7;
                PlayerPrefs.SetInt("Cannon2", 7);
            }
        }
    }
    public void Select()
    {
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

        playerWindow.SetActive(true);
        cannonWindow.SetActive(false);

        foreach(GameObject select in selectCannons)
        {
            select.SetActive(false);
        }
    }

    public void GameStart()
    {
        LoadingController.LoadNextScene("Game");
    }
}
