using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject player;

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
        if (gameObject.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
        {
            // 타일 관련
            if (gameObject.CompareTag("Rest"))
            {
                playerMovement.currentTile = 1;
                StartCoroutine(PlayerTileReset());
            }
            else if (gameObject.CompareTag("Item"))
            {
                playerMovement.currentTile = 2;
                StartCoroutine(PlayerTileReset());
            }
            else if (gameObject.CompareTag("Event"))
            {
                playerMovement.currentTile = 3;
                StartCoroutine(PlayerTileReset());
            }
            else if (gameObject.CompareTag("Shop"))
            {
                playerMovement.currentTile = 4;
                StartCoroutine(PlayerTileReset());
            }

            // 몬스터 관련
            if (gameObject.CompareTag("M_Fire")) // 불 형태 몬스터
            {
                playerMovement.currentTile = 5.1f;
                StartCoroutine(PlayerTileReset());
            }
        }
    }

    IEnumerator PlayerTileReset()
    {
        yield return new WaitForSeconds(1f);

        playerMovement.currentTile = 0;
        Destroy(gameObject);
    }
}
