using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    private float gameTime = 0f;
    public TextMeshProUGUI timerText;
    private bool isPlayerAlive = true;


    void Start()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath += HandlePlayerDeath;
        }
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            gameTime += Time.deltaTime;
            UpdateTimerUI();
        }
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
