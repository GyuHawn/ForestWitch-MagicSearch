using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
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

    void Arrow()
    {
        int index = ltemList.Count;
        GameObject arrowObject = Instantiate(arrowPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        arrowObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(arrowObject);
    }

    void Bag()
    {
        int index = ltemList.Count;
        GameObject bagObject = Instantiate(bagPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        bagObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bagObject);
    }

    void Bone()
    {
        int index = ltemList.Count;
        GameObject boneObject = Instantiate(bonePrifab, new Vector3(0, 0, 0), Quaternion.identity);
        boneObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(boneObject);
    }

    void Book()
    {
        int index = ltemList.Count;
        GameObject bookObject = Instantiate(bookPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        bookObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bookObject);
    }

    void Bow()
    {
        int index = ltemList.Count;
        GameObject bowObject = Instantiate(bowPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        bowObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bowObject);
    }

    void Crown()
    {
        int index = ltemList.Count;
        GameObject crownObject = Instantiate(crownPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        crownObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(crownObject);
    }

    void Dagger()
    {
        int index = ltemList.Count;
        GameObject daggerObject = Instantiate(daggerPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        daggerObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(daggerObject);
    }

    void Fish()
    {
        int index = ltemList.Count;
        GameObject fishObject = Instantiate(fishPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        fishObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(fishObject);
    }

    void Gold()
    {
        int index = ltemList.Count;
        GameObject goldObject = Instantiate(goldPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        goldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(goldObject);
    }

    void Grow()
    {
        int index = ltemList.Count;
        GameObject growObject = Instantiate(growPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        growObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(growObject);
    }

    void Hood()
    {
        int index = ltemList.Count;
        GameObject hoodObject = Instantiate(hoodPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        hoodObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(hoodObject);
    }

    public void Jewel()
    {
        int index = ltemList.Count;
        GameObject jeweldObject = Instantiate(jewelPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        jeweldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(jeweldObject);
    }

    public void Coin()
    {
        int index = ltemList.Count;
        GameObject coinObject = Instantiate(coinPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        coinObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(coinObject);
    }

    public void Mushroom()
    {
        int index = ltemList.Count;
        GameObject mushroomObject = Instantiate(mushroomPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        mushroomObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(mushroomObject);
    }

    public void Necklace()
    {
        int index = ltemList.Count;
        GameObject necklaceObject = Instantiate(necklacePrifab, new Vector3(0, 0, 0), Quaternion.identity);
        necklaceObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(necklaceObject);
    }

    public void Pick()
    {
        int index = ltemList.Count;
        GameObject pickObject = Instantiate(pickPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        pickObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(pickObject);
    }

    public void Ring()
    {
        int index = ltemList.Count;
        GameObject ringObject = Instantiate(ringPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        ringObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(ringObject);
    }

    public void Shield()
    {
        int index = ltemList.Count;
        GameObject shieldObject = Instantiate(shieldPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        shieldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(shieldObject);
    }

    public void Skull()
    {
        int index = ltemList.Count;
        GameObject skullObject = Instantiate(skullPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        skullObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(skullObject);
    }

    public void Sword()
    {
        int index = ltemList.Count;
        GameObject swordObject = Instantiate(swordPrifab, new Vector3(0, 0, 0), Quaternion.identity);
        swordObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(swordObject);
    }

}
