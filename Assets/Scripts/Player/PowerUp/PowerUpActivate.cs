using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpActivate : MonoBehaviour
{
    [SerializeField] private GameObject powerUpObject; 
    [SerializeField] private GameObject jugador;       

    void Start()
    {
        // Verificar la decisión del jugador desde el Singleton
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
            Debug.LogWarning("No se encontró la instancia de LevelsButtons.");
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
            Debug.LogWarning("No se asignó un objeto para el Power-Up.");
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
                Debug.Log("El jugador ya tiene el script ChangeDirection.");
            }
        }
        else
        {
            Debug.LogWarning("No se asignó el objeto jugador.");
        }
    }
}
