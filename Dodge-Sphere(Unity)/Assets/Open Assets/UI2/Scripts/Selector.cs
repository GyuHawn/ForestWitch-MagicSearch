using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selector : MonoBehaviour{
    public GameObject textObject;
    public string[] options;

    //[HideInInspector]
    public int currentOption;

    public void Next(){
        if(currentOption < options.Length - 1){
            currentOption++;
            SetOption(currentOption);
        }
    }

    public void Previous(){
        if(currentOption > 0){
            currentOption--;
            SetOption(currentOption);
        }
    }

    public void SetOption(int id){
        currentOption = id;
        textObject.GetComponent<TextMeshProUGUI>().text = options[id];
    }

    public string GetOption(int id){
        return options[id];
    }

    public string GetCurrentOption(){
        return options[currentOption];
    }

    public int GetOptionByString(string s, int ifNull = 0){
        for(int i = 0; i < options.Length; i++){
            if(options[i] == s){
                return i;
            }
        }
        return ifNull;
    }
}
