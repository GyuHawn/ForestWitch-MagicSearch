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

    // fillAmount 초기화
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
        float elapsedTime = 0;
        float startY = transform.position.y;
        while (elapsedTime < time)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / time);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z); // 최종 위치 확정
    }

}
