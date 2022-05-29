using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowResize : MonoBehaviour
{
    // Start is called before the first frame update
    public void maximizeScreen()
    {
        int biggerWidth = Screen.width+200;
        Screen.SetResolution(biggerWidth, (biggerWidth/16)*9, false);
    }

    public void minimizeScreen()
    {
        int smallerWidth = Screen.width-200;
        Screen.SetResolution(smallerWidth, (smallerWidth/16)*9, false);
    }
}
