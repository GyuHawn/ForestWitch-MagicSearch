using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConvert : MonoBehaviour
{
    public GameObject stageLoading;
    public GameObject monsterLoading;
    public GameObject bossLoading;
    public GameObject itemLoading;
    public GameObject eventLoading;
    public GameObject shopLoading;
    public GameObject restLoading;

    public void LoadingImage(GameObject loading, float time)
    {
        StartCoroutine(Loading(loading, time));
    }

    IEnumerator Loading(GameObject loading, float time)
    {
        loading.SetActive(true);

        yield return new WaitForSeconds(time);

        loading.SetActive(false);
    }
}
