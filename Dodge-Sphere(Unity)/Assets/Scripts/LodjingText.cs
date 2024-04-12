using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LodjingText : MonoBehaviour
{
    public List<string> lodingtexts = new List<string>();
    public TMP_Text text;

    void Start()
    {
        lodingtexts.Add("급하게 준비하는 중...");
        lodingtexts.Add("밥 먹는 중...");
        lodingtexts.Add("길을 잃었습니다...");
        lodingtexts.Add("숲으로 가는 중...");
        lodingtexts.Add("빗자루를 잃어버렸습니다...");
        lodingtexts.Add("책 보는 중...");
        lodingtexts.Add("이제 곳 도착입니다...");
        lodingtexts.Add("잠깐 쉬어갑니다...");

        TextUpdate();
    }

    void TextUpdate()
    {
        int num = Random.Range(0, lodingtexts.Count);

        text.text = lodingtexts[num];
    }

}
