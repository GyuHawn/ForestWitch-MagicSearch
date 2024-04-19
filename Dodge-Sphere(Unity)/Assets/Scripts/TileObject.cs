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
    private MonsterMap monsterMap;
    private MapConvert mapConvert;
    private AudioManager audioManager;

    public GameObject player;

    

    private void Awake()
    {
        getItem = GameObject.Find("Manager").GetComponent<GetItem>();
        eventScript = GameObject.Find("Manager").GetComponent<EventScript>();
        shopScript = GameObject.Find("Manager").GetComponent<ShopScript>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        restScript = GameObject.Find("Manager").GetComponent<RestScript>();
        mapConvert = GameObject.Find("Manager").GetComponent<MapConvert>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
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
                if (gameObject.CompareTag("M_Fire")) // 불 몬스터
                {
                    mapConvert.ConvertLoading(mapConvert.bossLoading, 3f, 2);
                    StartCoroutine(CurrentTileNum(5.1f));
                    StartCoroutine(PlayerTileReset());
                }
                else if (gameObject.CompareTag("M_Cactus")) // 선인장 몬스터
                {
                    mapConvert.ConvertLoading(mapConvert.monsterLoading, 3f, 1);
                    StartCoroutine(CurrentTileNum(5.2f));
                    StartCoroutine(PlayerTileReset());
                }
                else if (gameObject.CompareTag("M_Mush")) // 버섯 몬스터
                {
                    mapConvert.ConvertLoading(mapConvert.monsterLoading, 3f, 1);
                    StartCoroutine(CurrentTileNum(5.3f));
                    StartCoroutine(PlayerTileReset());
                }
                else if (gameObject.CompareTag("M_Chest")) // 상자 몬스터
                {
                    mapConvert.ConvertLoading(mapConvert.monsterLoading, 3f, 1);
                    StartCoroutine(CurrentTileNum(5.4f));
                    StartCoroutine(PlayerTileReset());
                }
                else if (gameObject.CompareTag("M_Beholder")) // 주시자 몬스터
                {
                    mapConvert.ConvertLoading(mapConvert.monsterLoading, 3f, 1);
                    StartCoroutine(CurrentTileNum(5.5f));
                    StartCoroutine(PlayerTileReset());
                }
                else if (gameObject.CompareTag("M_Clown")) // 광대 몬스터
                {
                    mapConvert.ConvertLoading(mapConvert.bossLoading, 3f, 2);
                    StartCoroutine(CurrentTileNum(5.6f));
                    StartCoroutine(PlayerTileReset());
                }
            }
        }
    }

    IEnumerator CurrentTileNum(float num)
    {
        yield return new WaitForSeconds(1f);

        playerMovement.currentTile = num;
    }

    IEnumerator EmpyTile()
    {
        yield return new WaitForSeconds(2f);

        playerMovement.moveNum = 1;
        Destroy(gameObject);
    }

    IEnumerator RestTile()
    {
        mapConvert.ConvertLoading(mapConvert.restLoading, 3f, 2);
        yield return new WaitForSeconds(2.5f);

        //audioManager.RestAudio();

        playerMovement.isRest = true;
        playerMovement.moveNum = 6;
        restScript.restUI.SetActive(true);
        Destroy(gameObject);
    }

    IEnumerator ItemTile()
    {
        mapConvert.ConvertLoading(mapConvert.itemLoading, 3f, 5);
        yield return new WaitForSeconds(2.5f);

        playerMovement.isItem = true;
        playerMovement.moveNum = 2;
        getItem.onItem = true;
        Destroy(gameObject);
    }

    IEnumerator EventTile()
    {
        mapConvert.ConvertLoading(mapConvert.eventLoading, 3f, 4);
        yield return new WaitForSeconds(2.5f);

        //audioManager.EventAudio();

        playerMovement.isEvent = true;
        playerMovement.moveNum = 3;
        eventScript.onEvent = true;
        Destroy(gameObject);
    }

    IEnumerator ShopTile()
    {
        mapConvert.ConvertLoading(mapConvert.shopLoading, 3f, 3);
        yield return new WaitForSeconds(2.5f);

       // audioManager.ShopAudio();

        playerMovement.isShop = true;
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
