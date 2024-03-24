using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private PlayerItem playerItem;

    private void Awake()
    {
        playerItem = GameObject.Find("Player").GetComponent<PlayerItem>();
    }

    void Start()
    {

    }


    void Update()
    {

    }

    public void Arrow()
    {
        playerItem.arrow = true;
    }
    public void Bag()
    {
        playerItem.bag = true;
    }
    public void Bone()
    {
        playerItem.bone = true;
    }
    public void Book()
    {
        playerItem.book = true;
    }
    public void Bow()
    {
        playerItem.bow = true;
    }
    public void Crown()
    {
        playerItem.crown = true;
    }
    public void Dagger()
    {
        playerItem.dagger = true;
    }
    public void Fish()
    {
        playerItem.fish = true;
    }
    public void Gold()
    {
        playerItem.gold = true;
    }
    public void Grow()
    {
        playerItem.grow = true;
    }
    public void Hood()
    {
        playerItem.hood = true;
    }
    public void Jewel()
    {
        playerItem.jewel = true; 
    }
    public void Coin()
    {
        playerItem.coin = true;
    }
    public void Mushroom()
    {
        playerItem.mushroom = true; 
    }
    public void Necklace()
    {
        playerItem.necklace = true;
    }
    public void Pick()
    {
        playerItem.pick = true;
    }
    public void Ring()
    {
        playerItem.ring = true;
    }
    public void Shield()
    {
        playerItem.shield = true;
    }
    public void Skull()
    {
        playerItem.skull = true;
    }
    public void Sword()
    {
        playerItem.sword = true;
    }
}
