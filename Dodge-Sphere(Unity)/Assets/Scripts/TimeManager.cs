using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text currnetTimerText; // 진행시간 텍스트

    private float currentTime = 0f; // 진행 시간

    void Start()
    {
        currentTime = 0f; // 진행시간 초기화
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // 분, 초 변환
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        currnetTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
