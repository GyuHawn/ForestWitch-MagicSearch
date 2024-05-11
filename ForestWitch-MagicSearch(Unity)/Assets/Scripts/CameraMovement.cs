using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;

    public GameObject player;

    public Vector3 offset; // �÷��̾� ���� �Ÿ�
    public bool fix; // ī�޶� ���� ����

    private void Awake()
    {
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
    }

    void Start()
    {    
        fix = true;
    }

    void FindPlayer()
    {
        if (player == null) // �÷��̾� ã��
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        FindPlayer(); // �÷��̾� ã��

        if (playerMovement.currentTile < 5 && playerMovement.tile) // Ÿ�ϸ� - ī�޶� �̵�
        {
            offset = new Vector3(0, 8, -1.5f);
            transform.position = player.transform.position + offset;
        }
        else if ((monsterMap.fireMoved || monsterMap.cactusMoved || monsterMap.mushMoved) && fix) // ���͸�1 - ī�޶� ����
        {
            fix = false;
            StartCoroutine(Stage1MonsterCamera());
        }
        else if ((monsterMap.chestMoved || monsterMap.beholderMoved || monsterMap.clownMoved) && fix) // ���͸�1 - ī�޶� ����
        {
            fix = false;
            StartCoroutine(Stage2MonsterCamera());
        }

    }

    IEnumerator Stage1MonsterCamera()
    {
        yield return new WaitForSeconds(2);

        offset = new Vector3(0, 21f, -0.95f);
        transform.position = player.transform.position + offset;
    }
    IEnumerator Stage2MonsterCamera()
    {
        yield return new WaitForSeconds(2);

        offset = new Vector3(0.4f, 25f, -0.5f);
        transform.position = player.transform.position + offset;
    }
}