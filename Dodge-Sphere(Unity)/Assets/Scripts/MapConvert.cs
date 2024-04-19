using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConvert : MonoBehaviour
{
    private AudioManager audioManager;

    public GameObject stageLoading;
    public GameObject monsterLoading;
    public GameObject bossLoading;
    public GameObject itemLoading;
    public GameObject eventLoading;
    public GameObject shopLoading;
    public GameObject restLoading;

    private float time = 0f;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void ConvertMap(int num)
    {
        switch (num)
        {
            case 0:
                audioManager.MonsterAudio();
                break;
            case 1:
                audioManager.BossAudio();
                break;
            case 2:
                audioManager.RestAudio();
                break;
            case 3:
                audioManager.ShopAudio();
                break;
            case 4:
                audioManager.EventAudio();
                break;
            case 5:
                break;
        }
    }

    public void ConvertLoading(GameObject loading, float time, int num)
    {
        float currentTime = Time.time;
        if (currentTime < this.time + 3f) {return;}
        this.time = currentTime;

        StartCoroutine(Loading(loading, time, num));
    }

    IEnumerator Loading(GameObject loading, float time, int num)
    {
        loading.SetActive(true);
        audioManager.ConvertAudio();
        yield return new WaitForSeconds(time);

        loading.SetActive(false);
        ConvertMap(num);
    }
}
