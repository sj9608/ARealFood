using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodRotator : MonoBehaviour
{
    public float rotationSpeed; // 회전 속도
    public GameObject objectToRotate; // 회전시킬 오브젝트

    void Start()
    {
        objectToRotate = GetComponent<GameObject>();
    }

    
    void FixedUpdate()
    {
        objectToRotate.transform.rotation = Quaternion.Euler(0, Time.fixedDeltaTime * rotationSpeed, 0); // y축 으로 프레임마다 회전
    }
}
