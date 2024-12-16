using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
   public void OnPause()
    {       
        PauseManager.Instance.TogglePauseState();         
    }
}
