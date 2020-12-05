using System.Threading;

using UnityEngine;
using UnityEngine.UI;

using ZXing;

public class MyQR : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel; // 메뉴패널
    private Thread qrThread; // QRcamera는 스레드 사용
    private Texture2D qrTexture; // QR 인식하는 텍스쳐
    private Rect rect; // 인식하는 사이즈
    private int W, H; // 스크린 사이즈
    private bool isQuit; // 어플 종료관련

    // public Text txt; // 바꿔줄 텍스트 디버그 용도

    public string LastResult; // Decode된 텍스트가 들어갈 변수
    public static bool shouldDecodeNow; // QR코드 디코드 해야하는 상황판단

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        qrThread.Abort();
    }

    /// <summary>
    /// Callback sent to all game objects before the application is quit.
    /// </summary>
    void OnApplicationQuit()
    {
        isQuit = true;
    }

    void Start()
    {
        W = Screen.width;
        H = Screen.height;

        rect = new Rect(0, 0, W, H);
        qrTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);


        LastResult = "";
        shouldDecodeNow = true;

        qrThread = new Thread(DecodeQR);
        qrThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldDecodeNow)
        {
            qrTexture.ReadPixels(rect, 0, 0, false);
            qrTexture.Apply();
        }
        if(LastResult == "FirstFood")
        {
            menuPanel.SetActive(true);
            DeActive();
        }
    }

    void DecodeQR()
    {
        var barcodeReader = new BarcodeReader();

        while (true)
        {
            if (isQuit)
                break;
            try
            {
                // decode the current frame
                var result = barcodeReader.Decode(qrTexture.GetPixels32(), W, H);

                if (result != null)
                {
                    LastResult = result.Text;
                    shouldDecodeNow = false;

                    // Debug.Log("디코딩 된 텍스트 값은 " + LastResult);
                    // txt.text = LastResult; // 디버그용
                }

                // Sleep a little bit and set the signal to get the next frame
                Thread.Sleep(200);
            }
            catch
            {

            }
        }
    }

    void DeActive()
    {
        this.gameObject.SetActive(false);
    }
}
