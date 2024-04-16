using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    // BGM
    public AudioSource b_MainMenu; // 메인메뉴
    public AudioSource b_Story; // 스토리
    public AudioSource b_Loading; // 로딩
    public AudioSource[] b_Monsters; // 몬스터
    public AudioSource b_Boss; // 보스
    public AudioSource b_Shop; // 상점
    public AudioSource b_Event; // 이벤트
    public AudioSource b_Rest; // 휴식
    public AudioSource b_TileMap; // 타일맵

    // Function
    public AudioSource f_Button; // 버튼 (o) 
    public AudioSource f_ButtonFail; // 버튼 (x)
    public AudioSource f_Spwan; // 몬스터 소환
    public AudioSource f_Potion; // 물약
    public AudioSource f_Win; // 승리
    public AudioSource f_GetItem; // 아이템획득
    public AudioSource f_Clear; // 클리어
    public AudioSource f_Die; // 플레이어 사망
    public AudioSource f_Heal; // 회복
  


    // Slider bgmSlider;
    //public Slider generalSlider;

    private void Awake()
    {

    }

    void Start()
    {
        StopAudio();
        /*
        // 전체 볼륨 조절
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        float genVolume = PlayerPrefs.GetFloat("GenVolume", 1.0f);

        bgmSlider.value = bgmVolume;
        generalSlider.value = genVolume;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            bgmMainMenu.volume = bgmVolume;

            startAudio.volume = genVolume;
            buttonAudio.volume = genVolume;
            bgmMainMenu.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Loading")
        {
            bgmMainMenu.volume = bgmVolume;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            bgmStage.volume = bgmVolume;
            bgmBossStage.volume = bgmVolume;
            bgmSelectMenu.volume = bgmVolume;
            bgmResultMenu.volume = bgmVolume;
        }*/
    }

    /*void Update()
    {
        // 전체 볼륨 조절
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            bgmMainMenu.volume = bgmSlider.value;

            startAudio.volume = generalSlider.value;
            buttonAudio.volume = generalSlider.value;
        }
        else if (SceneManager.GetActiveScene().name == "Loding")
        {
            bgmMainMenu.volume = bgmSlider.value;
        }
        else if (SceneManager.GetActiveScene().name == "Character")
        {
            bgmCharacterMenu.volume = bgmSlider.value;

            startAudio.volume = generalSlider.value;
            buttonAudio.volume = generalSlider.value;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            bgmStage.volume = bgmSlider.value;
            bgmBossStage.volume = bgmSlider.value;
            bgmSelectMenu.volume = bgmSlider.value;
            bgmResultMenu.volume = bgmSlider.value;

            attackAudio.volume = generalSlider.value;
            defenseAudio.volume = generalSlider.value;
            hitAudio.volume = generalSlider.value;
            // monsterAttackAudio.volume = generalSlider.value;
            buttonAudio.volume = generalSlider.value;

        }

       PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
       PlayerPrefs.SetFloat("GenVolume", generalSlider.value);
    }*/
    
    // BGM 재생
    public void MainAudio()
    {
        b_MainMenu.Play();
    }
    public void StoryAudio()
    {
        b_Story.Play();
    }
    public void LoadinAudio()
    {
        b_Loading.Play();
    }
    public void MonsterAudio(int num)
    {
        num = UnityEngine.Random.Range(0, b_Monsters.Length);

        b_Monsters[num].Play();
    }
    public void BossAudio()
    {
        b_Boss.Play();
    }
    public void ShopAudio()
    {
        b_Shop.Play();
    }
    public void EventAudio()
    {
        b_Event.Play();
    }
    public void RestAudio()
    {
        b_Rest.Play();
    }
    public void TileMapAudio()
    {
        b_TileMap.Play();
    }

    // 기능 오디오 재생
    public void ButtonAudio()
    {
        f_Button.Play();
    }
    public void ButtonFailAudio()
    {
        f_ButtonFail.Play();
    }
    public void SpwanAudio()
    {
        f_Spwan.Play();
    } 
    public void PotionAudio()
    {
        f_Potion.Play();
    }
    public void WinAudio()
    {
        f_Win.Play();
    } 
    public void GetItemAudio()
    {
        f_GetItem.Play();
    } 
    public void ClearAudio()
    {
        f_Clear.Play();
    }
    public void DieAudio()
    {
        f_Die.Play();
    }
    public void HealAudio()
    {
        f_Heal.Play();
    }


    // 시작시 소리 중복 제거용
    void StopAudio()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            b_Story.Stop();
            f_Button.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            foreach(AudioSource audio in b_Monsters)
            {
                audio.Stop();
            }
            b_Boss.Stop();
            b_Shop.Stop();
            b_Event.Stop();
            b_Rest.Stop();
            b_TileMap.Stop();


            f_Button.Stop();
            f_ButtonFail.Stop();
            f_Spwan.Stop();
            f_Potion.Stop();
            f_Win.Stop();
            f_GetItem.Stop();
            f_Clear.Stop();
            f_Die.Stop();
            f_Heal.Stop();
        }
    }
}