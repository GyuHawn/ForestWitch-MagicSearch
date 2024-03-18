using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;

    public GameObject player;

    public Vector3 offset; // 플레이어 사이 거리
    public bool fix; // 카메라 고정 여부

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
    }

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        fix = true;
    }
 
    void Update()
    {
        if(playerMovement.currentTile < 5 && playerMovement.tile) // 타일맵 - 카메라 이동
        {
            offset = new Vector3(0, 8, -1.5f);
            transform.position = player.transform.position + offset;
        }
        else if (monsterMap.fireMoved && fix) // 몬스터맵 - 카메라 고정
        {
            fix = false;
            StartCoroutine(MonsterMapCamera());
        }
        
    }

    IEnumerator MonsterMapCamera()
    {
        yield return new WaitForSeconds(2);

        offset = new Vector3(0, 21f, -0.5f);
        transform.position = player.transform.position + offset;
    }
}
