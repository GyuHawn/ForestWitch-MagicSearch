using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour{
    public int ScreenId;
    public GameObject list;

    public Vector2[] positions;
    public int defaultSelected = 0;

    [HideInInspector]
    public int currentSelected;

    private bool releasedButton = true;

    public void Previous(){
        if(currentSelected > 0){
            currentSelected--;
            ChanGeListPosition(currentSelected);
        }
    }

    public void Next(){
        if(currentSelected < positions.Length - 1){
            currentSelected++;
            ChanGeListPosition(currentSelected);
        }
    }

    void Set(int id){
        currentSelected = id;
        ChanGeListPosition(id);
    }

    void Start(){
        currentSelected = defaultSelected;
        ChanGeListPosition(defaultSelected);
    }

    void ChanGeListPosition(int id){
        list.GetComponent<RectTransform>().anchoredPosition = positions[id];
    }

    void Update(){
        if(Input.GetAxis("Horizontal") != 0 && ScreensSwitch.instance.current == ScreenId){
            if(Input.GetAxis("Horizontal") > 0 && releasedButton){
                Next();
                releasedButton = false;
            }else if(Input.GetAxis("Horizontal") < 0 && releasedButton){
                Previous();
                releasedButton = false;
            }
        }else{
            releasedButton = true;
        }
    }
}
