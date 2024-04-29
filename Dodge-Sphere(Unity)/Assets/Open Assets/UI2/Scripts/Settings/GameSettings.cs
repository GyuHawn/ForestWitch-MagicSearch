using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettings{
    //VIDEO
    public ResolutionSerializable resolution = new ResolutionSerializable(Screen.currentResolution);
    public FullScreenMode window_mode = FullScreenMode.FullScreenWindow;
    public int graphic_quality = 3;

    //AUDIO
    public float volume = 0.5f;
}

[System.Serializable]
public class ResolutionSerializable{
    public int width, height;

    public ResolutionSerializable(){
        this.width = Screen.currentResolution.width;
        this.height = Screen.currentResolution.height;
    }

    public ResolutionSerializable(int _width, int _height){
        this.width = _width;
        this.height = _height;
    }

    public ResolutionSerializable(Resolution res){
        this.width = res.width;
        this.height = res.height;
    }

    public Resolution Get(){
        Resolution res = new Resolution();
        res.width = this.width;
        res.height = this.height;
        return res;
    }
}
