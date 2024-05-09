using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // 회전 속도
    public float rotationSpeed;
    public bool x;
    public bool y;
    public bool z;

    void Update()
    {
        if (x)
        {
            // x축 기준으로 회전
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        else if(y)
        {
            // y축 기준으로 회전
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (z)
        {
            // z축 기준으로 회전
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        
    }
}
