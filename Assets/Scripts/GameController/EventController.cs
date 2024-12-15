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
    
    private void Awake()
    {
        GetInstance();
    }



    private void OnEnable()
    {
        
        EnemyHealth.OnEnemyDeath += HandleEnemyDeath;
        
        playerHealth.OnPlayerDeath += HandlePlayerDeath;
        
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= HandleEnemyDeath;
        
        playerHealth.OnPlayerDeath -= HandlePlayerDeath;
         
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
