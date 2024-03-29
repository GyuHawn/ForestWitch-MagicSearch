using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GetItem getItem;
    private RestScript restScript;
    private EventScript eventScript;
    private ShopScript shopScript;

    public GameObject player;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        getItem = GameObject.Find("Manager").GetComponent<GetItem>();
        eventScript = GameObject.Find("Manager").GetComponent<EventScript>();
        shopScript = GameObject.Find("Manager").GetComponent<ShopScript>();
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
                if (gameObject.CompareTag("Empy"))
                {
                    StartCoroutine(EmpyTile());
                }
                // 타일 관련
                if (gameObject.CompareTag("Item"))
                {
                    StartCoroutine(ItemTile());
                }
                else if (gameObject.CompareTag("Event"))
                {
                    StartCoroutine(EventTile());
                }
                else if (gameObject.CompareTag("Shop"))
                {
                    StartCoroutine(ShopTile());
                }
                else if (gameObject.CompareTag("Rest"))
                {
                    StartCoroutine(RestTile());
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

    IEnumerator EmpyTile()
    {
        yield return new WaitForSeconds(2.5f);

        playerMovement.moveNum = 1;
        Destroy(gameObject);
    }

    IEnumerator RestTile()
    {
        yield return new WaitForSeconds(2.5f);

        //playerMovement.moveNum = 6;
        restScript.restUI.SetActive(true);
        Destroy(gameObject);
    }

    IEnumerator ItemTile()
    {
        yield return new WaitForSeconds(2.5f);

        playerMovement.moveNum = 2;
        getItem.onItem = true;
        Destroy(gameObject);
    }

    IEnumerator EventTile()
    {
        yield return new WaitForSeconds(2.5f);

        playerMovement.moveNum = 3;
        eventScript.onEvent = true;
        Destroy(gameObject);
    }

    IEnumerator ShopTile()
    {
        yield return new WaitForSeconds(2.5f);

        playerMovement.moveNum = 4;
        shopScript.shopUI.SetActive(true);
        shopScript.rerollNum = 1;
        shopScript.ItemSetting();
        Destroy(gameObject);
    }

    IEnumerator PlayerTileReset()
    {
        yield return new WaitForSeconds(3f);

        playerMovement.currentTile = 0;
        Destroy(gameObject);
    }
}
