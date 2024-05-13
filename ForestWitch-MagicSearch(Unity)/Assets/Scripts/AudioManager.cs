using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    private GameDatas gameDatas;

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
    public AudioSource b_Defeat; // 패배

    // Function
    public AudioSource fn_Button; // 버튼 (o) 
    public AudioSource fn_ButtonFail; // 버튼 (x)
    public AudioSource fn_Potion; // 물약
    public AudioSource fn_Win; // 승리
    public AudioSource fn_GetItem; // 아이템획득
    public AudioSource fn_Clear; // 클리어
    public AudioSource fn_Convert; // 전환
    public AudioSource fn_Cannon; // 대포 발사
    public AudioSource fn_HitMonster; // 대포 발사
    
    // Monster 
    // 불
    public AudioSource f_Base;
    public AudioSource f_Cry;
    public AudioSource f_Jump;
    public AudioSource f_Roll;

    // 선인장
    public AudioSource c_Bounce;
    public AudioSource c_Wave;
    public AudioSource c_Punch;
    public AudioSource c_Butt;

    // 버섯
    public AudioSource m_Butt;
    public AudioSource m_Spin;
    public AudioSource m_Uper;

    // 광대
    public AudioSource cl_Push;
    public AudioSource cl_Shot;
    public AudioSource cl_Dance;

    // 상자
    public AudioSource ch_Bite;
    public AudioSource ch_Butt;
    public AudioSource ch_Eating;

    // 주시자
    public AudioSource be_Laser; 
    public AudioSource be_Multi;
    public AudioSource be_Aiming;


    
    private AudioSource currentAudioSource; // 현재 재생중인 오디오

    public Slider b_Slider; // 배경음 볼륨 슬라이드
    public TMP_Text b_PercentText;
    public Slider f_Slider; // 효과음 볼륨 슬라이드 
    public TMP_Text f_PercentText;
    public Slider m_Slider; // 몬스터 볼륨 슬라이드
    public TMP_Text m_PercentText;
    
    private float bgmVolume;
    private float fncVolume;
    private float monsterVolume;

    private void Awake()
    {
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    void Start()
    {
        /*// 전체 볼륨 조절
        gameDatas.LoadFieldData<int>("bgmVolume", value => {
            bgmVolume = value;
        }, () => {
            bgmVolume = 1.0f;
        });
        gameDatas.LoadFieldData<int>("fncVolume", value => {
            fncVolume = value;
        }, () => {
            fncVolume = 1.0f;
        });
        gameDatas.LoadFieldData<int>("monsterVolume", value => {
            monsterVolume = value;
        }, () => {
            monsterVolume = 1.0f;
        });*/

        b_Slider.value = bgmVolume;
        f_Slider.value = fncVolume;
        m_Slider.value = monsterVolume;

        SartAudioSetting();
        if (SceneManager.GetActiveScene().name == "Game")
        {
            TileMapAudio();
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            b_MainMenu.volume = bgmVolume;          
            b_Story.volume = bgmVolume;

            fn_Button.volume = fncVolume;
        }
        else if (SceneManager.GetActiveScene().name == "Loading")
        {
            b_Loading.volume = bgmVolume;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            foreach (AudioSource audio in b_Monsters)
            {
                audio.volume = bgmVolume;
            }
            b_Boss.volume = bgmVolume;
            b_Shop.volume = bgmVolume;
            b_Event.volume = bgmVolume;
            b_Rest.volume = bgmVolume;
            b_Story.volume = bgmVolume;
            b_TileMap.volume = bgmVolume;
            b_Defeat.volume = bgmVolume;

            fn_Potion.volume = fncVolume;
            fn_Button.volume = fncVolume;
            fn_ButtonFail.volume = fncVolume; 
            fn_Win.volume = fncVolume;
            fn_GetItem.volume = fncVolume;
            fn_Clear.volume = fncVolume;
            fn_Convert.volume = fncVolume;
            fn_Cannon.volume = fncVolume;
            fn_HitMonster.volume = fncVolume;

            f_Base.volume = monsterVolume;
            f_Cry.volume = monsterVolume;
            f_Jump.volume = monsterVolume;
            f_Roll.volume = monsterVolume;
            m_Butt.volume = monsterVolume;
            m_Spin.volume = monsterVolume;
            m_Uper.volume = monsterVolume;
            c_Bounce.volume = monsterVolume;
            c_Wave.volume = monsterVolume;
            c_Punch.volume = monsterVolume;
            c_Butt.volume = monsterVolume;
            cl_Push.volume = monsterVolume;
            cl_Shot.volume = monsterVolume;
            cl_Dance.volume = monsterVolume;
            ch_Bite.volume = monsterVolume;
            ch_Butt.volume = monsterVolume;
            ch_Eating.volume = monsterVolume;
            be_Laser.volume = monsterVolume;
            be_Multi.volume = monsterVolume;
            be_Aiming.volume = monsterVolume;
        }
    }

    void Update()
    {
        // 현재 볼륨 퍼센트
        b_PercentText.text = "[ " + (int)(b_Slider.value * 100) + "% ]";
        f_PercentText.text = "[ " + (int)(f_Slider.value * 100) + "% ]";
        m_PercentText.text = "[ " + (int)(m_Slider.value * 100) + "% ]";

        // 전체 볼륨 조절
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            b_MainMenu.volume = b_Slider.value;
            b_Story.volume = b_Slider.value;

            fn_Button.volume = f_Slider.value;

        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            foreach (AudioSource audio in b_Monsters)
            {
                audio.volume = b_Slider.value;
            }
            b_Boss.volume = b_Slider.value;
            b_Shop.volume = b_Slider.value;
            b_Event.volume = b_Slider.value;
            b_Rest.volume = b_Slider.value;
            b_Story.volume = b_Slider.value;
            b_TileMap.volume = b_Slider.value;
            b_Defeat.volume = b_Slider.value;

            fn_Potion.volume = f_Slider.value;
            fn_Button.volume = f_Slider.value;
            fn_ButtonFail.volume = f_Slider.value;
            fn_Win.volume = f_Slider.value;
            fn_GetItem.volume = f_Slider.value;
            fn_Clear.volume = f_Slider.value;
            fn_Convert.volume = f_Slider.value;
            fn_Cannon.volume = f_Slider.value;
            fn_HitMonster.volume = f_Slider.value;

            f_Base.volume = m_Slider.value;
            f_Cry.volume = m_Slider.value;
            f_Jump.volume = m_Slider.value;
            f_Roll.volume = m_Slider.value;
            m_Butt.volume = m_Slider.value;
            m_Spin.volume = m_Slider.value;
            m_Uper.volume = m_Slider.value;
            c_Bounce.volume = m_Slider.value;
            c_Wave.volume = m_Slider.value;
            c_Punch.volume = m_Slider.value;
            c_Butt.volume = m_Slider.value;
            cl_Push.volume = m_Slider.value;
            cl_Shot.volume = m_Slider.value;
            cl_Dance.volume = m_Slider.value;
            ch_Bite.volume = m_Slider.value;
            ch_Butt.volume = m_Slider.value;
            ch_Eating.volume = m_Slider.value;
            be_Laser.volume = m_Slider.value;
            be_Multi.volume = m_Slider.value;
            be_Aiming.volume = m_Slider.value;
        }

        gameDatas.SaveFieldData("bgmVolume", b_Slider.value);
        gameDatas.SaveFieldData("fncVolume", f_Slider.value);
        gameDatas.SaveFieldData("monsterVolume", m_Slider.value);
    }

    // 현재 재생되고 있는 오디오 정지
    public void StopCurrentAudio()
    {
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }
    }

    // BGM 재생
    public void MainAudio()
    {
        // 현재 재생중인 오디오 정지 후 재생
        StopCurrentAudio(); 
        currentAudioSource = b_MainMenu;
        currentAudioSource.Play();
    }

    public void StoryAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Story;
        currentAudioSource.Play();
    }

    public void LoadinAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Loading;
        currentAudioSource.Play();
    }
    
    public void MonsterAudio()
    {
        StopCurrentAudio();
        int num = UnityEngine.Random.Range(0, b_Monsters.Length);
        currentAudioSource = b_Monsters[num];
        currentAudioSource.Play();
    }

    public void BossAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Boss;
        currentAudioSource.Play();
    }

    public void ShopAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Shop;
        currentAudioSource.Play();
    }

    public void EventAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Event;
        currentAudioSource.Play();
    }

    public void RestAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Rest;
        currentAudioSource.Play();
    }

    public void TileMapAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_TileMap;
        currentAudioSource.Play();
    }
    
    public void DefeatAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Defeat;
        currentAudioSource.Play();
    }

    // 기능 오디오 재생
    public void ButtonAudio()
    {
        fn_Button.Play();
    }
     
    public void ButtonFailAudio()
    {
        fn_ButtonFail.Play();
    }

    public void PotionAudio()
    {
        fn_Potion.Play();
    }

    public void WinAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_Win;
        currentAudioSource.Play();
    }

    public void GetItemAudio()
    {
        fn_GetItem.Play();
    }

    public void ClearAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_Clear;
        currentAudioSource.Play();
    }

    public void ConvertAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_Convert;
        currentAudioSource.Play();
    }
    public void CannonAudio()
    {
        fn_Cannon.Play();
    }
    public void HitMonsterAudio()
    {
        fn_HitMonster.Play();
    }
    

    // 몬스터
    // 선인장
    public void C_BounceAudio()
    {
        c_Bounce.Play();
    }
    public void C_WaveAudio()
    {
        c_Wave.Play();
    }
    public void C_PunchAudio()
    {
        c_Punch.Play();
    }
    public void C_ButtAudio()
    {
        c_Butt.Play();
    }

    // 버섯
    public void M_ButtAudio()
    {
        m_Butt.Play();
    }
    public void M_SpinAudio()
    {
        m_Spin.Play();
    }
    public void M_UperAudio()
    {
        m_Uper.Play();
    }

    // 불
    public void F_BaseAudio()
    {
        f_Base.Play();
    }
    public void F_CryAudio()
    {
        f_Cry.Play();
    }
    public void F_JumpAudio()
    {
        f_Jump.Play();
    }
    public void F_RollAudio()
    {
        f_Roll.Play();
    }

    // 상자
    public void Ch_BiteAudio()
    {
        ch_Bite.Play();
    }
    public void Ch_ButtAudio()
    {
        ch_Butt.Play();
    }
    public void Ch_EatingAudio()
    {
        ch_Eating.Play();
    }

    // 주시자
    public void Be_LazerAudio()
    {
        be_Laser.Play();
    }
    public void Be_MultiAudio()
    {
        be_Multi.Play();
    }
    public void Be_AimingAudio()
    {
        be_Aiming.Play();
    }

    // 광대
    public void Cl_PushAudio()
    {
        cl_Push.Play();
    }
    public void Cl_ShotAudio()
    {
        cl_Shot.Play();
    }
    public void Cl_DanceAudio()
    {
        cl_Dance.Play();
    }

    // 시작시 소리 셋팅
    void SartAudioSetting()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainAudio();
            b_Story.Stop();
            fn_Button.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            StopBGMAudio(); // BGM
            StopFunctionAudio(); // 기능
            StopMonsterAudio(); // 몬스터
        }
    }

    void StopBGMAudio()
    {
        b_TileMap.Stop();
        b_Story.Stop();
        foreach (AudioSource audio in b_Monsters)
        {
            audio.Stop();
        }
        b_Boss.Stop();
        b_Shop.Stop();
        b_Event.Stop();
        b_Rest.Stop();
        b_TileMap.Stop();
        b_Defeat.Stop();
    }
    void StopFunctionAudio()
    {
        fn_Button.Stop();
        fn_ButtonFail.Stop();
        fn_Potion.Stop();
        fn_Win.Stop();
        fn_GetItem.Stop();
        fn_Clear.Stop();
        fn_Convert.Stop();
        fn_Cannon.Stop();
        fn_HitMonster.Stop();    
    }
    void StopMonsterAudio()
    {
        f_Base.Stop();
        f_Cry.Stop();
        f_Jump.Stop();
        f_Roll.Stop();
        c_Bounce.Stop();
        c_Wave.Stop();
        c_Punch.Stop();
        c_Butt.Stop();
        m_Butt.Stop();
        m_Spin.Stop();
        m_Uper.Stop();
        cl_Dance.Stop();
        cl_Push.Stop();
        cl_Shot.Stop();
        ch_Bite.Stop();
        ch_Butt.Stop();
        ch_Eating.Stop();
        be_Laser.Stop();
        be_Multi.Stop();
        be_Aiming.Stop();
    }
}