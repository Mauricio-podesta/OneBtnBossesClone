using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController Instance;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Vida EnemyHealth;
    public void GetInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //Aca el método se aplica.
    private void Awake()
    {
        GetInstance();
    }



    private void OnEnable()
    {
        if (EnemyHealth != null)
        {
            EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
        }
        else
        {
            Debug.Log("Null Enemy health");
        }

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
        if (EnemyHealth != null)
        {
            EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;
        }
        else
        {
            Debug.Log("Null Enemy health");
        }


        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath -= HandlePlayerDeath;
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
    private void HandlePlayerDeath()
    {

        Debug.Log("Player has died!");

    }
}
