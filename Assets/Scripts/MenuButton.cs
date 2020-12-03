using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Text txt;

    public void ChangeText()
    {
        txt.text = "성공";
    }
}
