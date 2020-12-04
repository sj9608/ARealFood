using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    // public TextMeshProUGUI textMeshPro; // 디버그용 TMP
    private ObjectSpawner objectToSpawn;
    private PlacementIndicator placementIndicator;
    public GameObject paneVisual; // 패널 비주얼
    public GameObject objectSpawner;

    [SerializeField]
    private GameObject checkedButton; // 확인 버튼
    public static bool isChecked = false;  // 확인
    public static int menuNum = 0;

    // [SerializeField]
    // private GameObject spawnObject; // 생성할 오브젝트( object spawner에 존재하는거 )
    public GameObject[] pre; // 프리팹
    //public GameObject[] indicator; // 인디케이터( 메뉴판, 평지표시기 )

    private void Start()
    {
        // textMeshPro.GetComponent<TextMeshPro>(); // 디버그용 TMP
        objectToSpawn = FindObjectOfType<ObjectSpawner>();
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        // spawnObject = GetComponent<ObjectSpawner>().objectToSpawn;
        objectToSpawn.name = GameObject.FindGameObjectWithTag("Pizza").name;
        checkedButton.SetActive(false);
        objectSpawner.GetComponent<ObjectSpawner>().enabled = true;
    }

    public void click1()
    {
        menuNum = 0;
        checkedButton.SetActive(true);
    }

    public void click2()
    {
        menuNum = 1;
        checkedButton.SetActive(true);
    }

    public void CheckButton()
    {
        isChecked = true;
        ChangePanel();
    }

    void ChangePanel()
    {
        if (isChecked) //사용자가 음식을 최종 확인했을때 전환 작업
        {
            PlacementIndicator.isOnOff = false;
            if (!PlacementIndicator.isOnOff)
            {
                paneVisual.GetComponent<PlacementIndicator>().visual[0].SetActive(false);
                paneVisual.GetComponent<PlacementIndicator>().visual[1].SetActive(true);
                objectToSpawn.enabled = true;
                checkedButton.SetActive(false); //초기화 버튼 사라지게
                objectSpawner.GetComponent<ObjectSpawner>().enabled = true;
            }
        }
    }

    public void ReturnMenuPanel(bool getEscapeBtn)
    {
        paneVisual.GetComponent<PlacementIndicator>().visual[0].SetActive(true);
        paneVisual.GetComponent<PlacementIndicator>().visual[1].SetActive(false);
    }
    
    // public void ClickMenu()
    // {
    //     if (objectToSpawn.name == "Menu1")
    //     {

    //         isChecked_Num = 1;
    //         if (isChecked_Num != 1)
    //         {
    //             checkedButton.SetActive(false);
    //             isChecked_Num = 0;
    //         }
    //         else if (isChecked_Num == 1)
    //         {
    //             checkedButton.SetActive(true);

    //         }

    //     }
    //     if (objectToSpawn.name == "Ham" && isChecked_Num != 0)
    //     {
    //         isChecked_Num = 2;
    //         if (isChecked_Num != 2)
    //         {
    //             checkedButton.SetActive(false);
    //         }
    //         else if (isChecked_Num == 2)
    //         {
    //             checkedButton.SetActive(true);
    //         }
    //     }
    // }
}
