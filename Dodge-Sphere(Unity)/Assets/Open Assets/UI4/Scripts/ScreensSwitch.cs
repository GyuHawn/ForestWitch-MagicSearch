using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreensSwitch : MonoBehaviour{
    public GameObject[] screens;

    public int MainId = 0;

    [HideInInspector]
    public static ScreensSwitch instance;

    [HideInInspector]
    public int current = 0;

    private bool isCancelEventInUse = false;

    void Awake(){
        instance = this;
    }

    void Start(){
        foreach(GameObject go in screens){
            RectTransform rect = go.GetComponent<RectTransform>();
            if(rect != null){
                rect.anchoredPosition = Vector2.zero;
            }
        }
        foreach(Button button in FindObjectsOfType<Button>()){
            button.gameObject.AddComponent(typeof(HighlightFix));
        }
        current = MainId;
        ShowOnly(MainId);
    }

    void Update(){
        if(Input.GetAxis("Cancel") == 1 && !isCancelEventInUse){
            int prev = screens[current].GetComponent<ScreenSettings>().previousScreen;
            if(prev != -1){
                isCancelEventInUse = true;
                ShowOnly(prev);
            }
        }else if(Input.GetAxis("Cancel") == 0){
            isCancelEventInUse = false;
        }
    }

    public void ShowOnly(int id){
        if(id < screens.Length){
            current = id;
            for(int i = 0; i < screens.Length; i++){
                if(i == id){
                    screens[i].SetActive(true);
                    continue;
                }
                screens[i].SetActive(false);
            }
        }
    }

    public void Add(int id){
        for(int i = 0; i < screens.Length; i++){
            if(i == id){
                screens[i].SetActive(true);
                current = id;
                return;
            }
        }
    }

    public void Remove(int id){
        for(int i = 0; i < screens.Length; i++){
            if(i == id){
                screens[i].SetActive(false);
                return;
            }
        }
    }

    public void Quit(){
        Application.Quit();
    }
}
