using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public Sprite[] lodingBarColors;
    public GameObject lodingBarImage;

    [SerializeField]
    Image lodingBar;

    private static string nextScene;

    public static void LoadNextScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loding");
    }

    void Start()
    {
        ChangeLodingBarColor();
        StartCoroutine(LoadSceneProcess());
    }

    void ChangeLodingBarColor()
    {
        int index = Random.Range(0, lodingBarColors.Length);
        Image bar = lodingBarImage.GetComponent<Image>();

        bar.sprite = lodingBarColors[index];
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                lodingBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                lodingBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if (lodingBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
