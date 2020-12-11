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
    public GameObject panelVisual; // 패널 비주얼
    public GameObject objectSpawner;

    [SerializeField]
    private GameObject checkedButton; // 확인 버튼
    public static bool isChecked = false;  // 확인
    public static int menuNum = 0;
    public GameObject[] pre; // 프리팹


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
                panelVisual.GetComponent<PlacementIndicator>().visual[0].SetActive(false);
                panelVisual.GetComponent<PlacementIndicator>().visual[1].SetActive(true);
                objectToSpawn.enabled = true;
                checkedButton.SetActive(false); // 초기화 버튼 사라지게
                objectSpawner.GetComponent<ObjectSpawner>().enabled = true;
            }
        }
    }

    public void ReturnMenuPanel(bool getEscapeBtn)
    {
        panelVisual.GetComponent<PlacementIndicator>().visual[0].SetActive(true);
        panelVisual.GetComponent<PlacementIndicator>().visual[1].SetActive(false);
    }
}
