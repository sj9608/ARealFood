using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // 생성할 게임오브젝트
    private GameObject obj; // 집어넣을 게임 오브젝트
    private bool isExist = false; // 오브젝트 존재 판단
    private PlacementIndicator placementIndicator; // 평지 표시기

    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();

        if(placementIndicator != null) // 평면 감지 되었으면
        {
            objectToSpawn.SetActive(true); // 오브젝트 비주얼 활성화
        }
    }

    void Update()
    {
        // 터치를 했나 ? && ???????????
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (!isExist) // obj가 존재하지 않을 때 생성
            {
                obj = Instantiate(objectToSpawn,
                placementIndicator.transform.position,
                placementIndicator.transform.rotation);
                isExist = true;
            }
            else if(isExist) // 존재하면 위치만 바꿔줌
            {
                obj.transform.position = placementIndicator.transform.position;
                obj.transform.rotation = placementIndicator.transform.rotation;
            }
        }
    }
}
