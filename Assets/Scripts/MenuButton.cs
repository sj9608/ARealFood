using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private ObjectSpawner objectToSpawn; // 
    private PlacementIndicator placementIndicator; // 배치 인디케이터
    public GameObject panelVisual; // 패널 비주얼
    public GameObject objectSpawner; // 오브젝트 스포너

    [SerializeField]
    private GameObject checkedButton; // 확인 버튼
    public static bool isChecked = false;  // 확인
    public static int menuNum = 0;
    public GameObject[] pre; // 프리팹


    private void Start()
    {
        objectToSpawn = FindObjectOfType<ObjectSpawner>(); // ObjectToSpawn 컴포넌트(스크립트) 활성 비활성에 쓰일 게임오브젝트
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        checkedButton.SetActive(false);
        objectSpawner.GetComponent<ObjectSpawner>().enabled = true;
    }

    public void click1() // 1번 메뉴 클릭
    {
        menuNum = 0; // 메뉴 번호
        isChecked = true; // 메뉴를 클릭했나 안했나 판단
        checkedButton.SetActive(true); // 배치하기 버튼을 활성화 한다.
    }

    public void click2() // 2번 메뉴 클릭
    {
        menuNum = 1; // 메뉴 번호
        isChecked = true; // 메뉴를 클릭했나 안했나 판단
        checkedButton.SetActive(true);
    }

    public void click3() // 2번 메뉴 클릭
    {
        menuNum = 2; // 메뉴 번호
        isChecked = true; // 메뉴를 클릭했나 안했나 판단
        checkedButton.SetActive(true);
    }

    public void CheckButton() // 배치하기 버튼을 눌렀을 때 
    {
        ChangePanel(); // 패널 변경함수 실행
    }

    void ChangePanel()
    {
        if (isChecked) // 사용자가 메뉴를 클릭한 상태이면
        {
            PlacementIndicator.isOnOff = false; // 메뉴패널 꺼짐판단.
            if (!PlacementIndicator.isOnOff)
            {
                panelVisual.GetComponent<PlacementIndicator>().visual[0].SetActive(false);
                panelVisual.GetComponent<PlacementIndicator>().visual[1].SetActive(true);
                objectToSpawn.enabled = true; // 오브젝트 스포너 
                checkedButton.SetActive(false); // 초기화 버튼 사라지게
                objectSpawner.GetComponent<ObjectSpawner>().enabled = true;
            }
        }
    }

    public void ReturnMenuPanel(bool getEscapeBtn) // 인디케이터 -> 메뉴패널
    {
        PlacementIndicator.isOnOff = true;
        isChecked = false;
        objectSpawner.GetComponent<ObjectSpawner>().enabled = false;
        panelVisual.GetComponent<PlacementIndicator>().visual[0].SetActive(true);
        panelVisual.GetComponent<PlacementIndicator>().visual[1].SetActive(false);
    }
}
