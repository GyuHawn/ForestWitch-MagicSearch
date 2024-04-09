using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonScript : MonoBehaviour
{
    private ClownMonster clownMonster;
    public bool real;


    private void Awake()
    {
        clownMonster = GameObject.Find("ClownMonster").GetComponent<ClownMonster>();
    }

    void DeleteMonsters()
    {
        foreach(GameObject monster in clownMonster.summonMonsters)
        {
            Destroy(monster);
        }
        clownMonster.summonMonsters.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (real)
            {
                clownMonster.defenceEffect.SetActive(false);
                clownMonster.isSummon = false;
                DeleteMonsters();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
