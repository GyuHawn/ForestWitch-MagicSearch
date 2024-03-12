using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereMovement : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
    }
 
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
