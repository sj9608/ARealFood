using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectToSpawns; // 생성할 게임오브젝트
    private GameObject obj; // 집어넣을 게임 오브젝트
    private bool isExist = false; // 오브젝트 존재 판단
    private PlacementIndicator placementIndicator; // 평지 표시기
    public static bool getEscBtn = false; // 뒤로가기버튼 체크
    public GameObject menuButton; // UI Panel

    void OnEnable() // 오브젝트 스포너 스크립트가 활성화가 되면
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>(); // 스크립트 찾아서 컴포넌트 속성값 넣어줌.

        if (placementIndicator != null) // 평면 감지 되었으면
        {
            objectToSpawns[MenuButton.menuNum].SetActive(true); // 오브젝트 비주얼 활성화
        }
    }

    void Update()
    {
        // 터치 입력 감지 && 첫 번 쨰 클릭(GetButtonDown 느낌)
        if (MenuButton.isChecked == true)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                if (!isExist) // obj가 존재하지 않을 때 생성
                {
                    obj = Instantiate(objectToSpawns[MenuButton.menuNum],
                        placementIndicator.transform.position,
                        placementIndicator.transform.rotation); // obj에 해당하는 메뉴 넘버의 값을 생성과 동시에 인디케이터의 위치, 회전값에 따라 생성시켜줌
                    isExist = true; // obj가 존재 하는 상태로 판단
                }
                else if (isExist) // 존재하면 위치만 바꿔줌
                {
                    obj.transform.position = placementIndicator.transform.position;
                    obj.transform.rotation = placementIndicator.transform.rotation;
                }
            }
        }

        // 안드로이드 플랫폼에서
        if (Application.platform == RuntimePlatform.Android)
        {
            // 뒤로가기 버튼 눌렀을 때
            if (Input.GetKey(KeyCode.Escape))
            {
                /* obj 가리기 */
                obj.SetActive(false);
                isExist = false;
                MenuButton.menuNum = 0;
                /* obj 가리기 */

                getEscBtn = true;
                menuButton.GetComponent<MenuButton>().ReturnMenuPanel(getEscBtn);
            }
        }
    }
}
