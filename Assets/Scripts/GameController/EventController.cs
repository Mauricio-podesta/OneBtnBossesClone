using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController Instance;

    [Header("Health Management")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemyLife enemyHealth;

    [Header("Game Management")]
    private GameManager gameManager;

    [Header("UI Management")]
    [SerializeField] private PlayerHealthUI playerHealthUI;
    private void Awake()
    {
        GetInstance();
        gameManager = FindObjectOfType<GameManager>();
    }
    public void GetInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        enemyHealth.OnEnemyDeath += gameManager.HandleEnemyDeath;
        playerHealth.OnPlayerDeath += gameManager.HandlePlayerDeath;
        playerHealth.OnPlayerHurt += playerHealthUI.DecreaseHealth;
    }
    private void OnDisable()
    {
        enemyHealth.OnEnemyDeath -= gameManager.HandleEnemyDeath;
        playerHealth.OnPlayerDeath -= gameManager.HandlePlayerDeath;
        playerHealth.OnPlayerHurt -= playerHealthUI.DecreaseHealth;
    }

}
