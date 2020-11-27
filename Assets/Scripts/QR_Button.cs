using UnityEngine;
public class QR_Button : MonoBehaviour
{
    public static bool clicked = false;
    void LateUpdate()
    {
        clicked = false;
    }
    public void Click()
    {
        clicked = true;
    }
}