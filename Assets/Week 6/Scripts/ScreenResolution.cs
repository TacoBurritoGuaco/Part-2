using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    public int width;
    public int height;

    public void newWidth(int w)
    {
        width = w;
    }
    public void newHeight(int h)
    {
        height = h;
    }

    public void setRes()
    {
        Screen.SetResolution(width, height, false);
    }
}
