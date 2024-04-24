using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsSlider : MonoBehaviour{
    public GameObject barMask;

    public float filledWidth;

    [HideInInspector]
    public int currentStep;

    public void Up(){
        if(currentStep < 10){
            currentStep++;
            SetStep(currentStep);
        }
    }

    public void Down(){
        if(currentStep > 0){
            currentStep--;
            SetStep(currentStep);
        }
    }

    public void SetStep(int step){
        currentStep = step;
        Vector2 sizeDelta = barMask.GetComponent<RectTransform>().sizeDelta;
        sizeDelta.x = filledWidth * (step / 10f);
        barMask.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    public int GetStep(){
        return currentStep;
    }
}
