using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    public static bool movingForward = true;
    void Start()
    {
        
    }
    void Update()
    {
        Cambiodireccion();
    }
    void Cambiodireccion()
    {
        if (!PowerUp.canactivate && Input.GetMouseButtonDown(0))
        {
            movingForward = !movingForward;
        }
    }
}
