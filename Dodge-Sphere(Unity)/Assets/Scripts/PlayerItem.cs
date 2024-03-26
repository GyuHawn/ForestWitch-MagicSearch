using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameSetting gameSetting;
    private Portions portions;

    public GameObject[] ItemPos; // 획득 아이템 표시 위치
    public List<GameObject> ltemList = new List<GameObject>(); // 획득 아이템

    public GameObject arrowPrifab; // 아이템 프리템
    public bool arrow; // 아이템 획득 여부
    private bool onArrow; // 기능 중복실행 방지

    public GameObject bagPrifab;
    public bool bag;
    private bool onBag;

    public GameObject bonePrifab;
    public bool bone;
    private bool onBone;

    public GameObject bookPrifab;
    public bool book;
    private bool onBook;

    public GameObject bowPrifab;
    public bool bow;
    private bool onBow;

    public GameObject crownPrifab;
    public bool crown;
    private bool onCrown;

    public GameObject daggerPrifab;
    public bool dagger;
    private bool onDagger;

    public GameObject fishPrifab;
    public bool fish;
    private bool onFish;

    public GameObject goldPrifab;
    public bool gold;
    private bool onGold;

    public GameObject growPrifab;
    public bool grow;
    private bool onGrow;

    public GameObject hoodPrifab;
    public bool hood;
    private bool onHood;

    public GameObject jewelPrifab;
    public bool jewel;
    private bool onJewel;

    public GameObject coinPrifab;
    public bool coin;
    private bool onCoin;

    public GameObject mushroomPrifab;
    public bool mushroom;
    private bool onMushroom;

    public GameObject necklacePrifab;
    public bool necklace;
    private bool onNecklace;

    public GameObject pickPrifab;
    public bool pick;
    private bool onPick;

    public GameObject ringPrifab;
    public bool ring;
    private bool onRing;

    public GameObject shieldPrifab;
    public bool shield;
    private bool onShield;

    public GameObject skullPrifab;
    public bool skull;
    private bool onSkull;

    public GameObject swordPrifab;
    public bool sword;
    private bool onSword;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameSetting = GameObject.Find("Manager").GetComponent<GameSetting>();
        portions = GameObject.Find("Manager").GetComponent<Portions>();
    }

    void Update()
    {
        if (arrow && !onArrow)
        {
            onArrow = true;
            Arrow();
        }
        if (bag && !onBag)
        {
            onBag = true;
            Bag();
        }
        if (bone && !onBone)
        {
            onBone = true;
            Bone();
        }
        if (book && !onBook)
        {
            onBook = true;
            Book();
        }
        if (bow && !onBow)
        {
            onBow = true;
            Bow();
        }
        if (crown && !onCrown)
        {
            onCrown = true;
            Crown();
        }
        if (dagger && !onDagger)
        {
            onDagger = true;
            Dagger();
        }
        if (fish && !onFish)
        {
            onFish = true;
            Fish();
        }
        if (gold && !onGold)
        {
            onGold = true;
            Gold();
        }
        if (grow && !onGrow)
        {
            onGrow = true;
            Grow();
        }
        if (hood && !onHood)
        {
            onHood = true;
            Hood();
        }
        if (jewel && !onJewel)
        {
            onJewel = true;
            Jewel();
        }
        if (coin && !onCoin)
        {
            onCoin = true;
            Coin();
        }
        if (mushroom && !onMushroom)
        {
            onMushroom = true;
            Mushroom();
        }
        if (necklace && !onNecklace)
        {
            onNecklace = true;
            Necklace();
        }
        if (pick && !onPick)
        {
            onPick = true;
            Pick();
        }
        if (ring && !onRing)
        {
            onRing = true;
            Ring();
        }
        if (shield && !onShield)
        {
            onShield = true;
            Shield();
        }
        if (skull && !onSkull)
        {
            onSkull = true;
            Skull();
        }
        if (sword && !onSword)
        {
            onSword = true;
            Sword();
        }
    }

    void Arrow() // 대포 포탄의 속도가 증가 합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject arrowObject = Instantiate(arrowPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        arrowObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(arrowObject);

        // 아이템 기능
        playerMovement.arrow = true;
    }
    
    void Bag() // 물약이 장비 창이 1개 증가합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject bagObject = Instantiate(bagPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        bagObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bagObject);

        // 아이템 기능
        playerMovement.bag = true;
    }

    void Bone() // 최대 체력이 3증가 합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject boneObject = Instantiate(bonePrifab, new Vector3(0, 0, 0), Quaternion.identity);
        boneObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(boneObject);

        // 아이템 기능
        playerMovement.maxHealth += 3; // 최대 체력 3증가
        playerMovement.currentHealth += 3; // 증가한 만큼 회복
    }

    void Book() // 상점 아이템을 1회 초기화할 수 있습니다. (상점 미추가)
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject bookObject = Instantiate(bookPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        bookObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bookObject);

        // 아이템 기능
    }

    void Bow() // 대포의 최대 총알 수가 1감소 합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject bowObject = Instantiate(bowPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        bowObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bowObject);

        // 아이템 기능
        playerMovement.bow = true;
    }

    void Crown() // 최대 체력이 3증가합니다, 이동속도가 1증가합니다, 체력이 5회복됩니다, 300코인을 획득합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject crownObject = Instantiate(crownPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        crownObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(crownObject);

        // 아이템 기능
        playerMovement.maxHealth += 3; // 최대 체력 5증가
        playerMovement.currentHealth += 8; // 증가한 수치 + 5 회복
        playerMovement.moveSpd++; // 이동속도 1증가
        playerMovement.money += 300; // 300코인 추가
    }

    void Dagger() // 대포 공격력이 1증가 합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject daggerObject = Instantiate(daggerPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        daggerObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(daggerObject);

        // 아이템 기능
        playerMovement.dagger = true;
    }

    void Fish() // 상점에서 1회 무료로 아이템을 구매할 수 있습니다. (상점 미추가)
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject fishObject = Instantiate(fishPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        fishObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(fishObject);

        // 아이템 기능
    }

    void Gold() // 1000코인을 획득합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject goldObject = Instantiate(goldPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        goldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(goldObject);

        // 아이템 기능
        playerMovement.money += 1000; // 1000코인 추가
    }

    void Grow() // 이동속도가 1증가합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject growObject = Instantiate(growPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        growObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(growObject);

        // 아이템 기능
        playerMovement.moveSpd++; // 이동속도 1증가
    }

    void Hood() // 방어력이 1증가합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject hoodObject = Instantiate(hoodPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        hoodObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(hoodObject);

        // 아이템 기능
        playerMovement.defence++; // 방어력 1증가
    }

    public void Jewel() // 1500코인을 획득합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject jeweldObject = Instantiate(jewelPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        jeweldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(jeweldObject);

        // 아이템 기능
        playerMovement.money += 1500; // 1500코인 추가
    }

    public void Coin() // 500코인을 획득합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject coinObject = Instantiate(coinPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        coinObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(coinObject);

        // 아이템 기능
        playerMovement.money += 500; // 500코인 추가
    }

    public void Mushroom() // 이동속도가 2증가합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject mushroomObject = Instantiate(mushroomPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        mushroomObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(mushroomObject);

        // 아이템 기능
        playerMovement.moveSpd += 2; // 이동속도 2증가
    }

    public void Necklace() // 전투 시작 시 체력을 3회복 합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject necklaceObject = Instantiate(necklacePrifab, new Vector3(0, 0, 0), Quaternion.identity);
        necklaceObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(necklaceObject);

        // 아이템 기능
        playerMovement.necklace = true;
    }

    public void Pick() // 확률적으로 2배의 대미지를 입힙니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject pickObject = Instantiate(pickPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        pickObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(pickObject);

        // 아이템 기능
        playerMovement.pick = true;
    }

    public void Ring() // 전투 시작 시 체력을 2회복 합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject ringObject = Instantiate(ringPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        ringObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(ringObject);

        // 아이템 기능
        playerMovement.ring = true;
    }

    public void Shield() // 전투당 1번 공격을 방어합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject shieldObject = Instantiate(shieldPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        shieldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(shieldObject);

        // 아이템 기능
        playerMovement.shield = true;
    }

    public void Skull() // 최대 체력이 5증가 합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject skullObject = Instantiate(skullPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        skullObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(skullObject);

        // 아이템 기능
        playerMovement.maxHealth += 5; // 최대 체력 5증가
        playerMovement.currentHealth += 5; // 증가한 만큼 회복
    }

    public void Sword() // 대포 공격력이 2증가 합니다.
    {
        // 획득 아이템 표시
        int index = ltemList.Count;
        GameObject swordObject = Instantiate(swordPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        swordObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(swordObject);

        // 아이템 기능
        playerMovement.sword = true;
    }
}
