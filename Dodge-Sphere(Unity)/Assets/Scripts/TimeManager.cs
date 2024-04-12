using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    private ClearInfor clearInfor;

    public TMP_Text currnetTimerText; // 진행시간 텍스트

    public float currentTime = 0f; // 진행 시간

    private void Awake()
    {
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
    }

    void Start()
    {
        currentTime = 0f; // 진행시간 초기화
    }

    void Update()
    {
        if (!clearInfor.result) // 결과창 표시 중이 아닐때
        {
            currentTime += Time.deltaTime;

            // 분, 초 변환
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            currnetTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
    }
}
