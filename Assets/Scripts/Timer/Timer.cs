using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    private float gameTime = 0f;
    [SerializeField] private TextMeshProUGUI timerText;
    private bool isPlayerAlive = true;

    [SerializeField] private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath += HandlePlayerDeath;
        }
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {

        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        int centiseconds = Mathf.FloorToInt((gameTime * 100) % 100);
        timerText.text = string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, centiseconds);

    }
    private void HandlePlayerDeath()
    {
        isPlayerAlive = false;
    }
}
