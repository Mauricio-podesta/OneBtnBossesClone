using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class PowerUp : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float charge = 0;
    [SerializeField] private float velocidadIncremento = 2f;
    [SerializeField] private float MaxVelocity = 15f;
    [SerializeField] private float Timecharge;
    [SerializeField] private float TimeDischarge;

    [Header("Referencias")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private Slider Powerupslide;

    //datos privados
    public static bool canactivate = false;
    float normalvelocity;
    private Collider2D playerCollider;

    void Start()
    {
        UpdateHealthUI();

        normalvelocity = playerMovement.movementSpeed;

        playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
       
        if (playerCollider == null)
        {
            Debug.LogWarning("No se encontr� el Collider2D en el objeto con el tag 'Player'");
        }
        
     
    }

    void Update()
    {
        if (canactivate)
        {
            Discharge();
            
        }
        else
        {
            charger();

        }
    }

    void Discharge()
    {
        charge -= (100/TimeDischarge) * Time.deltaTime;
        charge = Mathf.Clamp(charge, 0f, 100f);
        UpdateHealthUI();

        if (charge <= 0) 
        {
            canactivate = false;
        }

        playerCollider.enabled = false;
        playerMovement.movementSpeed = Mathf.Clamp(playerMovement.movementSpeed + velocidadIncremento, 0f, MaxVelocity);
    }
    void charger()
    {

        charge += (100/Timecharge) * Time.deltaTime;
        //maximo de la carga
        charge = Mathf.Clamp(charge, 0f, 100f);
        UpdateHealthUI();
        
        playerMovement.movementSpeed = normalvelocity;
        playerCollider.enabled = true;
    }
    void UpdateHealthUI()
    {
        Powerupslide.value = charge;

    }
    public void OnPowerUp()
    {
        if (charge >= 100)
        {
            canactivate = true;
        }
    }

}
