using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;
using DG.Tweening;

public class MyQR : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel; // 메뉴패널
    private Thread qrThread; // QRcamera는 스레드 사용
    private Texture2D qrTexture; // QR 인식하는 텍스쳐
    private Rect rect; // 인식하는 사이즈
    private int W, H; // 스크린 사이즈
    private bool isQuit; // 어플 종료관련
    private bool isQrDetected = false; // Qr이 감지 되었는가 ?
    private float timer; // 타이머
    private float waitingTime = 2.0f; // 대기 시간
    public TextMeshProUGUI qrMessage; // TMP QR 메세지  (화면 중앙 하단부)
    public string LastResult; // Decode된 텍스트가 들어갈 변수

    void OnDestroy()
    {
        qrThread.Abort();
    }

    void OnApplicationQuit()
    {
        isQuit = true;
    }

    void Start()
    {
        W = Screen.width;
        H = Screen.height;

        rect = new Rect(0, 0, W, H);
        qrTexture = new Texture2D(W, H, TextureFormat.RGB24, false);
        
        LastResult = "";

        qrThread = new Thread(DecodeQR);
        qrThread.Start();
    }

    void Update()
    {
        /* QR 인식 */
        qrTexture.ReadPixels(rect, 0, 0, false);
        qrTexture.Apply();

        /* QR 감지 */
        DetectQR(); 
    }

    private void DetectQR()
    {
        if (isQrDetected) // QR이 감지 되었고
        {
            if (LastResult == "FirstFood") // 디코드한 결과 text가 지정해놓은 text일 경우
            {
                // 텍스트 값 변경 
                qrMessage.text = "인식 완료";
                // 딜레이 코드 넣기 
                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    menuPanel.SetActive(true);
                    DeActive(); // QR 패널 비활성화
                }
            }
            else
            {
                qrMessage.text = "등록되지 않은 코드입니다.";
                // 딜레이 코드 넣기 (2초)
                timer += Time.deltaTime;
                if (timer > waitingTime) // 2초 뒤엔 다시 QR메세지를 원상태로 돌려준다.
                {
                    qrMessage.text = "QR코드를 중앙에 위치 시키십시오.";
                    timer = 0;
                    isQrDetected = false; // Qr감지가 안된 상태로 돌린다.
                }

            }
        }
    }

    void DecodeQR() // Qr코드를 디코드
    {
        var barcodeReader = new BarcodeReader(); // ZXing 라이브러리의 BarcodeReader 형 변수 barcodeReader 

        while (true)
        {
            if (isQuit)
                break;
            try
            {
                // decode the current frame
                var result = barcodeReader.Decode(qrTexture.GetPixels32(), W, H); // ZXing 라이브러리의 Result형 변수 result에 인식된 범위의 텍스쳐의 픽셀을 읽고 해독

                if (result != null) // 결과 값이 도출이 되면 (Qr인식에 성공하면)
                {
                    LastResult = result.Text; // 결과값의 텍스트를 넘겨줌.
                    isQrDetected = true; // Qr감지판단을 참으로 함. (qr 인식 됌)
                }

                // Sleep a little bit and set the signal to get the next frame
                Thread.Sleep(200); // 스레드를 재운다. 0.2초 뒤
            }
            catch
            {
            }
        }
    }

    void DeActive()
    {
        this.gameObject.SetActive(false); // 현재 스크립트가 붙은 오브젝트 즉 qrPanel 비활성화
    }
}
