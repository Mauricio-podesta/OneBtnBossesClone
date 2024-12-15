using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeDirection : MonoBehaviour
{
    public static bool movingForward = true;
    
    void Update()
    {
        
    }
    public void OnCambiodireccion()
    {
        if (!PowerUp.canactivate)
        {
            movingForward = !movingForward;
            
        }
    }
}
