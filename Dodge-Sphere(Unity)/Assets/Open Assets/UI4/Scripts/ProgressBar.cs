using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour{
    public GameObject mask;
    public float minWidth = 0;
    public float maxWidth = 100;

    public float _progress = 0;
    public float progress{
        set{
            _progress = value;
            if(mask != null){
                RectTransform rect = mask.GetComponent<RectTransform>();
                if(rect != null){
                    Vector2 size = rect.sizeDelta;
                    Vector2 position = rect.anchoredPosition;
                    size.x = (maxWidth - minWidth) * (_progress / 100) + minWidth;
                    position.x = size.x / 2;
                    rect.sizeDelta = size;
                    rect.anchoredPosition = position;
                }
            }
        }
        get{
            return _progress;
        }
    }
}
