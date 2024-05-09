using System.Collections;
using UnityEngine;

public class CactusButt : MonoBehaviour
{
    void Start()
    {
        Invoke("SizeUp", 1.5f);
    }

    void SizeUp() 
    {
        StartCoroutine(SizeUpBulletOverTime(0.05f, 0.7f, 2f));
    }

    IEnumerator SizeUpBulletOverTime(float startSize, float endSize, float duration)
    {
        float time = 0;
        Vector3 startScale = new Vector3(startSize, startSize, startSize);
        Vector3 endScale = new Vector3(endSize, endSize, endSize);

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // 최종 스케일 설정
        transform.localScale = endScale;

        // 스케일이 최대로 커진 후 1초 대기
        yield return new WaitForSeconds(1);

        // 오브젝트 제거
        Destroy(gameObject);
    }
}
