using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject ex; // º≥∏Ì√¢

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEx()
    {
        Debug.Log("s");
        ex.SetActive(!ex.activeSelf);
    }
}
