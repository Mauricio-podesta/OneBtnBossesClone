using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour
{
    public GameObject currentCanvas;
    public GameObject targetCanvas;
    public void CanvasSwitcher()
    {
        if (currentCanvas != null) currentCanvas.SetActive(false); 
        if (targetCanvas != null) targetCanvas.SetActive(true);    
    }
}
