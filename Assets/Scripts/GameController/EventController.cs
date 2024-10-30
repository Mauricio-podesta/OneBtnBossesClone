using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] private PlayerHelath playerHealth;
    private void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnEnemyDeath += HandleEnemyDeath;
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
            playerHealth.OnEnemyDeath -= HandleEnemyDeath;
        }
        else
        {
            Debug.Log("Null player health");
        }
    }

    private void HandleEnemyDeath()
    {

        Debug.Log("Enemy has died!");
    }
}
