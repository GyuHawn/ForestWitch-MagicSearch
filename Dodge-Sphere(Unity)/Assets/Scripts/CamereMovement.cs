using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject player;

    public Vector3 offset;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
    }
 
    void Update()
    {
        if(playerMovement.currentTile != 5)
        {
            offset = new Vector3(0, 8, -1.5f);
        }
        else
        {
            offset = new Vector3(0, 31.75f, -1.5f);
        }

        transform.position = player.transform.position + offset;
    }
}
