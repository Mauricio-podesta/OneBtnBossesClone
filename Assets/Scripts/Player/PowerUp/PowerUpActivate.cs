using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpActivate : MonoBehaviour
{
    [SerializeField] private GameObject powerUpObject; 
    [SerializeField] private GameObject jugador;       

    void Start()
    {
        if (MovementChoiceButtons.Instance != null)
        {
            if (MovementChoiceButtons.Instance.usePowerUp)
            {
                ActivatePowerUp();
            }
            else
            {
                AddChangeDirection();
            }
        }
        else
        {
            Debug.LogWarning("The LevelsButtons instance was not found.");
        }
    }
    private void ActivatePowerUp()
    {
        if (powerUpObject != null)
        {
            powerUpObject.SetActive(true); 
   
        }
        else
        {
            Debug.LogWarning("An object was not assigned for the Power-Up.");
        }
    }

   
    private void AddChangeDirection()
    {
        if (jugador != null)
        {
            if (jugador.GetComponent<ChangeDirection>() == null)
            {
                jugador.AddComponent<ChangeDirection>(); 
               
            }
            else
            {
                Debug.Log("The player already has the ChangeDirection script.");
            }
        }
        else
        {
            Debug.LogWarning("The player object was not assigned.");
        }
    }
}
