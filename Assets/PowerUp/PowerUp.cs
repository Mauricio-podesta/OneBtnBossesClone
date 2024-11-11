using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PowerUp : MonoBehaviour
{
    public PlayerMovement playerMovement;
    float charge  = 0;
    private bool previousDirection;
    [SerializeField] private float velocidadIncremento = 2f;

    void Start()
    {
        if (playerMovement != null)
        {
            previousDirection = playerMovement.movingForward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        charger();
        if (charge == 100 && previousDirection != playerMovement.movingForward)
        {

            playerMovement.movementSpeed += velocidadIncremento;
            previousDirection = playerMovement.movingForward;

        }

    }

    void charger()
    {
        charge += 2 * Time.deltaTime;
        charge = Mathf.Clamp(charge, 0f, 100);
    }

}
