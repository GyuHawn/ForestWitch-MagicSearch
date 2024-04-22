using System.Collections;
using UnityEngine;

public class FaintBullet : MonoBehaviour
{
    public float speed;
    private GameObject player; // 따라갈 플레이어

    void Start()
    {
        player = GameObject.Find("Player");

        speed = 4f;

        Destroy(gameObject, 8f);  // 5초 후 삭제
    }

    void Update()
    {
        if (player != null) // 플레이어를 따라다님
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
