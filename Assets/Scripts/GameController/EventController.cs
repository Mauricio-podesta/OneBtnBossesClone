using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController Instance;

    // Referencias relacionadas con la salud del jugador y del enemigo
    [Header("Health Management")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Vida enemyHealth;

    // Referencia al GameManager
    [Header("Game Management")]
    private GameManager gameManager;

    // Referencia a la UI de salud del jugador
    [Header("UI Management")]
    [SerializeField] private PlayerHealthUI playerHealthUI;

    private void Awake()
    {
        GetInstance();
        gameManager = FindObjectOfType<GameManager>(); // Encuentra el GameManager en la escena
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
            DontDestroyOnLoad(gameObject);
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
