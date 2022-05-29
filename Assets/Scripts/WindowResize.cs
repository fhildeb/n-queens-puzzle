using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowResize : MonoBehaviour
{
    // Start is called before the first frame update
    public void maximizeScreen()
    {
        int newWidth = Screen.width+200;
        Screen.SetResolution(newWidth, (newWidth/16)*9, false);
    }

    public void minimizeScreen()
    {
        int newWidth = Screen.width-200;
        Screen.SetResolution(newWidth, (newWidth/16)*9, false);
    }
}
