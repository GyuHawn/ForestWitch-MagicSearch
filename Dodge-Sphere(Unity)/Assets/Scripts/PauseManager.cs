using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool isPaused = false; // 게임 일시정지

    public GameObject pause;
    public GameObject play;


    void Update()
    {
        if (isPaused)
        {
            pause.SetActive(false);
            play.SetActive(true);
        }
        else
        {
            pause.SetActive(true);
            play.SetActive(false);
        }
    }

    public void GamePause()
    {
        isPaused = !isPaused; // 일시정지 상태 반전

        if (isPaused)
        {
            Time.timeScale = 0; // 일시정지
        }
        else
        {
            Time.timeScale = 1; // 일시정지 해제
        }
    }
}
