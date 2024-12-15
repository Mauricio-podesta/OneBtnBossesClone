using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController Instance;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Vida EnemyHealth;

    private GameManager gameManager;
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
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        
        EnemyHealth.OnEnemyDeath += gameManager.HandleEnemyDeath;
        
        playerHealth.OnPlayerDeath += gameManager.HandlePlayerDeath;
        
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= gameManager.HandleEnemyDeath;
        
        playerHealth.OnPlayerDeath -= gameManager.HandlePlayerDeath;
         
    }
    
}
