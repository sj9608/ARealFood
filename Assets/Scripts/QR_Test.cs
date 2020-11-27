using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using ZXing;
using ZXing.QrCode.Internal;

public class QR_Test : MonoBehaviour
{
    public Camera ARCamera;
    public Camera QRCamera;

    public Text[] resulttexts;

    public GameObject menuPanel; // 메뉴패널 띄우기 (QR 인식 하면 띄울 패널오브젝트)
    private bool setQR;

    public void OnPreRender()
    {
        QRCamera.projectionMatrix = ARCamera.projectionMatrix;
        QRCamera.fieldOfView = ARCamera.fieldOfView;
        QRCamera.transform.localPosition = Vector3.zero;
        QRCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    public void Start()
    {
        QRCamera.enabled = false;
        Application.runInBackground = true;
    }
    private void Update()
    {
        //버튼 클릭 하면 값을 변경.
        if (QR_Button.clicked)
        {
            QRCamera.enabled = true;
            setQR = true;         
        }
    }
    public void OnPostRender()
    {
        if (setQR == true)
        {   
            
            //Create a new texture with the width and height of the screen
            Texture2D QRTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            QRTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            QRTexture.Apply();

            //QR코드 스캔
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
             
                var result = barcodeReader.Decode(QRTexture.GetPixels32(), QRTexture.width, QRTexture.height);
                
                if (result.Text == "FirstFood") // -> 이게 바코드(FirstFood로 QR 생성) 한거 
                {
                    if (result != null)
                    {
                        resulttexts[0].text = "인식 완료";
                        resulttexts[1].text = " ";
                        menuPanel.SetActive(true);  
                    }
                }
                else
                {
                    resulttexts[0].text = "없는 QR Code 입니다.";
                    setQR = false;
                    QRCamera.enabled = false;
                }
            }
            catch (Exception e)
            {
                resulttexts[0].text = "QR 코드가 없음!";
            }
            //한번 읽고 나서 다시 초기화 시켜줌.
            setQR = false;
            QRCamera.enabled = false;
        }
    }
}