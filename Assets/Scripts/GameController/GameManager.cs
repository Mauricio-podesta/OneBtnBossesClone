using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameObject victoryCanvas;
    public GameObject loseCanvas;

    private float gameTime = 0f;
    private float bestTime = 0f;
    private bool isPlayerAlive = true;
    private bool isEnemyAlive = true;

    void Start()
    {
        InitializeTimer();
        SubscribeToEvents();
        DeactivateCanvases();
    }

    void Update()
    {
        if (isPlayerAlive && isEnemyAlive)
        {
            UpdateGameTime();
        }
        else if (!isEnemyAlive)
        {
            SaveBestTime();
        }
    }

    private void InitializeTimer()
    {
        // Load the best time from PlayerPrefs
        bestTime = PlayerPrefs.GetFloat("BestGameTime", 0f);
    }

    private void SubscribeToEvents()
    {
        // Subscribe to player and enemy death events
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath += HandlePlayerDeath;
        }

        Vida enemyHealth = FindObjectOfType<Vida>();
        if (enemyHealth != null)
        {
            enemyHealth.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    private void UpdateGameTime()
    {
        gameTime += Time.deltaTime;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        int centiseconds = Mathf.FloorToInt((gameTime * 100) % 100);
        timerText.text = string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }

    private void SaveBestTime()
    {
        if (bestTime == 0 || gameTime < bestTime)
        {
            bestTime = gameTime;
            PlayerPrefs.SetFloat("BestGameTime", bestTime);
            PlayerPrefs.Save();
        }
    }

    private void HandlePlayerDeath()
    {
        isPlayerAlive = false;
        SaveGameData();
        ActivateCanvas(loseCanvas);
    }

    private void HandleEnemyDeath()
    {
        isEnemyAlive = false;
        SaveGameData();
        ActivateCanvas(victoryCanvas);
    }

    private void SaveGameData()
    {
        PlayerPrefs.SetFloat("GameTime", gameTime);
        PlayerPrefs.Save();
    }

    private void ActivateCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }

    private void DeactivateCanvases()
    {
        if (victoryCanvas != null) victoryCanvas.SetActive(false);
        if (loseCanvas != null) loseCanvas.SetActive(false);
    }
}
