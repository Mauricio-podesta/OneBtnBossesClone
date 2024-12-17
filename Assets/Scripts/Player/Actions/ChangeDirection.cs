using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeDirection : MonoBehaviour
{
    public static bool movingForward = true;
    public void OnChangedirection()
    {
        if (!PowerUp.canactivate)
        {
            movingForward = !movingForward;
        }
    }
}
