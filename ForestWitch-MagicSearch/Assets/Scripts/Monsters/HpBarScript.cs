using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBarScript : MonoBehaviour
{
    public Image healthBarFill;
    public TMP_Text healthPercent;

    private void Update()
    {
        // 체력바값을 현재 퍼센트로 업데이트
        healthPercent.text = ((int)(healthBarFill.fillAmount * 100)).ToString() + "%";
    }

    // 체력 값에 따른 체력바 업데이트
    public void UpdateHP(int currentHp, int maxHp)
    {
        if (currentHp > 0)
        {
            UpdateHealthBar(currentHp, maxHp);
        }
    }

    // 실제 체력바 업데이트
    void UpdateHealthBar(int currentHp, int maxHp)
    {
        float fillAmount = (float)currentHp / maxHp;
        healthBarFill.fillAmount = fillAmount;
    }

    // 체력바 재설정
    public void ResetHealthBar(int current, int max)
    {
        // 현재체력이 최대체력 이상일때 1.0으로
        if (current >= max)
        {
            healthBarFill.fillAmount = 1.0f;
        }
        else
        {
            // 현재체력의 최대체력에 대한 비율로 설정
            healthBarFill.fillAmount = (float)current / max;
        }
    }

    // 체력바를 특정위치로 이동
    public void MoveToYStart(float targetY, float time)
    {
        StartCoroutine(MoveToY(targetY, time));
    }

    // 체력바 이동
    IEnumerator MoveToY(float targetY, float time)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float elapsedTime = 0;
        float startY = rectTransform.anchoredPosition.y;
        while (elapsedTime < time)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / time);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetY);
    }
}
