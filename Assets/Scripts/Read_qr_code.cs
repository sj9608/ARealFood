using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using System;

class Read_qr_code : MonoBehaviour
{
    public RawImage raw_image_video;

    //camera texture
    private WebCamTexture cam_texture;

    //is reading qr_code
    private bool is_reading = true;

    void OnEnable()
    {
        try
        {
            is_reading = true;

            //init camera texture
            cam_texture = new WebCamTexture();

            cam_texture.Play();


            if (Application.platform == RuntimePlatform.Android)
            {
                
                raw_image_video.rectTransform.sizeDelta = new Vector2(Screen.width * cam_texture.width / (float)cam_texture.height, Screen.width);
                raw_image_video.rectTransform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                raw_image_video.rectTransform.sizeDelta = new Vector2(1080, 1080 * cam_texture.width / (float)cam_texture.height);
                raw_image_video.rectTransform.localScale = new Vector3(1, 1, 1);
                raw_image_video.rectTransform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                raw_image_video.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelWidth * cam_texture.height / (float)cam_texture.width);
                raw_image_video.rectTransform.localScale = new Vector3(1, 1, 1);
            }

            raw_image_video.texture = cam_texture;
            //}
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            throw;
        }
    }

    private float interval_time = 1f;
    private float time_stamp = 0;
    void Update()
    {
        if (is_reading)
        {
            time_stamp += Time.deltaTime;

            if (time_stamp > interval_time)
            {
                time_stamp = 0;

                try
                {

                    IBarcodeReader barcodeReader = new BarcodeReader();
                    // decode the current frame
                    var result = barcodeReader.Decode(cam_texture.GetPixels32(), cam_texture.width, cam_texture.height);
                    if (result != null)
                    {
                        is_reading = true;
                    }

                    is_reading = false;
                }
                catch (Exception ex)
                {
                    Debug.LogWarning(ex.Message);
                }
            }
        }
    }
}
