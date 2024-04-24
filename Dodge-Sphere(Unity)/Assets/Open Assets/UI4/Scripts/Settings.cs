using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Settings : MonoBehaviour{
    [Header("VIDEO")]
    public Selector resolution;
    public Selector window_mode, graphic_quality;

    [Header("AUDIO")]
    public StepsSlider volume;

    void Start(){
        Load();
    }

    void Save(GameSettings settings){
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/settings.save");
        bf.Serialize(file, settings);
        file.Close();

        Debug.Log("Settings saved");
    }
    
    public void Load(){
        GameSettings settings = new GameSettings();

        if(File.Exists(Application.persistentDataPath + "/settings.save")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/settings.save", FileMode.Open);
            settings = (GameSettings)bf.Deserialize(file);
            file.Close();
        }

        SetSettings(settings);
        SetUIValues(settings);
    }

    public void SetSettings(GameSettings settings){
        //VIDEO
        Screen.SetResolution(settings.resolution.width, settings.resolution.height, settings.window_mode);
        QualitySettings.SetQualityLevel(settings.graphic_quality);

        //AUDIO
        AudioListener.volume = settings.volume;
    }

    public void SetUIValues(GameSettings settings){
        //VIDEO
        resolution.SetOption(resolution.GetOptionByString(settings.resolution.width + " x " + settings.resolution.height, 10));
        window_mode.SetOption((Screen.fullScreenMode == FullScreenMode.FullScreenWindow)? 0 : ((Screen.fullScreenMode == FullScreenMode.MaximizedWindow)? 1 : 2));
        graphic_quality.SetOption(settings.graphic_quality);

        //AUDIO
        volume.SetStep(Mathf.RoundToInt(settings.volume * 10f));
    }

    public IEnumerator GetUIValues(Action<GameSettings> func){
        GameSettings settings = new GameSettings();

        //VIDEO
        settings.resolution = new ResolutionSerializable();
            if(resolution.GetCurrentOption() == "AUTO"){
                FullScreenMode prev = Screen.fullScreenMode;
                Screen.fullScreenMode = FullScreenMode.Windowed;
                yield return 0;
                settings.resolution.width = Screen.currentResolution.width;
                settings.resolution.height = Screen.currentResolution.height;
                Screen.fullScreenMode = prev;
            }else{
                settings.resolution.width = int.Parse(resolution.GetCurrentOption().Split(new string[] { " x " }, StringSplitOptions.None)[0]);
                settings.resolution.height = int.Parse(resolution.GetCurrentOption().Split(new string[] { " x " }, StringSplitOptions.None)[1]);
            }
        settings.window_mode = (window_mode.currentOption == 0)? FullScreenMode.FullScreenWindow : ((window_mode.currentOption == 1)? FullScreenMode.MaximizedWindow : FullScreenMode.Windowed);
        settings.graphic_quality = graphic_quality.currentOption;

        //AUDIO
        settings.volume = volume.GetStep()/10f;

        func(settings);
    }
    
    public void Apply(){
        StartCoroutine(GetUIValues(settings => {
            SetSettings(settings);
            Save(settings);
        }));
    }
}
