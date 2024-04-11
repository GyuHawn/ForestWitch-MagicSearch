using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarScript : MonoBehaviour
{
    public Image healthBarFill;

    // 체력에 비례하여 fillAmount 업데이트 
    public void UpdateHP(int currentHp, int maxHp)
    {
        if (currentHp > 0)
        {
            UpdateHealthBar(currentHp, maxHp);
        }
    }

    void UpdateHealthBar(int currentHp, int maxHp) 
    {
        float fillAmount = (float)currentHp / maxHp; 
        healthBarFill.fillAmount = fillAmount;
    }

    public void ResetHealthBar() 
    {
        healthBarFill.fillAmount = 1.0f;
    }

    // UI위치 이동
    public void MoveToYStart(float targetY, float time)
    {
        StartCoroutine(MoveToY(targetY, time));
    }

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
