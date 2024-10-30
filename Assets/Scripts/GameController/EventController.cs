using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    private void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath += HandlePlayerDeath;
        }
        else
        {
            Debug.Log("Null player health");
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath -= HandlePlayerDeath;
        }
        else
        {
            Debug.Log("Null player health");
        }
    }

    private void HandlePlayerDeath()
    {

        Debug.Log("Player has died!");

    }
}
