using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    public GameObject ex;
    public Button exBtn;

    void Start()
    {
        exBtn.onClick.AddListener(OnEx);
    }

    void Update()
    {
        
    }
        
    void OnEx()
    {
        ex.SetActive(!ex.activeSelf);
    }
}
