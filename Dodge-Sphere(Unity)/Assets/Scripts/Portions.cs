using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portions : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject[] portionPos;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        ItemSetting();
    }

    void ItemSetting()
    {
        if (!playerMovement.bag)
        {
            portionPos[1].SetActive(false);
        }
        else
        {
            portionPos[1].SetActive(true);
        }
    }
}
