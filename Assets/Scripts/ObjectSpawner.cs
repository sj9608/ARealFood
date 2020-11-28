﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // 생성할 게임오브젝트
    private PlacementIndicator placementIndicator; // 평지 표시기
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

    }

    void Update()
    {
        // 터치를 했나 ? && ???????????
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            GameObject obj = Instantiate(objectToSpawn, 
            placementIndicator.transform.position, 
            placementIndicator.transform.rotation);
        }
    }
}