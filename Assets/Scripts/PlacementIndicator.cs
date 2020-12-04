using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; // AR관련 레이캐스팅 기능 들어있음

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager; // AR session origin 오브젝트에 ARRaycastManager component 끌어오기 위함.
    public GameObject[] visual; // 올바른 평면일 경우에만 보여주기 위함. visual[0] 에는 메뉴판, [1]에는 인디케이터 표시예정

    public static bool isOnOff = true;

    void Start()
    {
        // 컴포넌트 가져오기
        rayManager = FindObjectOfType<ARRaycastManager>(); // 씬 내에서 ARRaycastManager 컴포넌트를 찾아서 가져옴
        visual[0] = transform.GetChild(0).gameObject;  // n 번째 자식의 게임오브젝트 컴포넌트 가져오기.       
        visual[1] = transform.GetChild(1).gameObject;  // n 번째 자식의 게임오브젝트 컴포넌트 가져오기.       
    }


    void Update()
    {
        // shooting AR raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>(); // 레이캐스트 히트를 리스트로 생성 
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if we hit an AR plane , update the position and rotation
        if (hits.Count > 0) // 레이캐스트가 평면에 부딛히면 부딛힌 갯수만큼 카운트해서 값을 넘겨줌 ( 안부딛히면 null값 반환 )
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
