using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // 회전 속도
    public float rotationSpeed;

    void Update()
    {
        // y축 기준으로 회전
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
