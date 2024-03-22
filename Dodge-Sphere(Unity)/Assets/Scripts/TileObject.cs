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
        
    }

    
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        if (player != null)
        {
            if (gameObject.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
            {
                // 타일 관련
                if (gameObject.CompareTag("Rest"))
                {
                    StartCoroutine(CurrentTileNum(1));
                    StartCoroutine(PlayerTileReset());
                }
                else if (gameObject.CompareTag("Item"))
                {
                    StartCoroutine(CurrentTileNum(2));
                    StartCoroutine(PlayerTileReset());
                }
                else if (gameObject.CompareTag("Event"))
                {
                    StartCoroutine(CurrentTileNum(3));
                    StartCoroutine(PlayerTileReset());
                }
                else if (gameObject.CompareTag("Shop"))
                {
                    StartCoroutine(CurrentTileNum(4));
                    StartCoroutine(PlayerTileReset());
                }

                // 몬스터 관련
                if (gameObject.CompareTag("M_Fire")) // 불 형태 몬스터
                {
                    StartCoroutine(CurrentTileNum(5.1f));
                    StartCoroutine(PlayerTileReset());
                }
            }
        }
    }

    IEnumerator CurrentTileNum(float num)
    {
        yield return new WaitForSeconds(2.5f);

        playerMovement.currentTile = num;
    }

    IEnumerator PlayerTileReset()
    {
        yield return new WaitForSeconds(3f);

        playerMovement.currentTile = 0;
        Destroy(gameObject);
    }
}
