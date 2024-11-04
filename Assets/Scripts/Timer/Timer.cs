using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    private float gameTime = 0f;
    private float gameBestTime = 0f;
    public TextMeshProUGUI timerText;
    private bool isPlayerAlive = true;
    private bool isEnemyAlive = true;
    private string gamescene;
    

    void Start()
    {
       
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        
         playerHealth.OnPlayerDeath += HandlePlayerDeath;

        Vida enemyHealt = FindObjectOfType<Vida>();

        enemyHealt.OnEnemyDeath += HandleEnemyDeath;

        gamescene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (isPlayerAlive || isEnemyAlive)
        {
            gameTime += Time.deltaTime;
            UpdateTimerUI();
        }
        UpdateBestTime();
    }
    void UpdateBestTime()
    {
        if (gameBestTime <= gameTime)
        {
            gameBestTime = gameTime;
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

        
        PlayerPrefs.SetFloat("GameTime", gameTime);
        PlayerPrefs.Save();

        PlayerPrefs.SetFloat("BestGameTime", gameBestTime);
        PlayerPrefs.Save();

        PlayerPrefs.SetString("GameScene", gamescene); 
        PlayerPrefs.Save();

        
        SceneManager.LoadScene("LoseScene");
    }
    private void HandleEnemyDeath()
    {
        isEnemyAlive = false;
      
        PlayerPrefs.SetFloat("GameTime", gameTime);
        PlayerPrefs.Save();
        
        PlayerPrefs.SetFloat("BestGameTime", gameBestTime);
        PlayerPrefs.Save();

        PlayerPrefs.SetString("GameScene", gamescene);
        PlayerPrefs.Save();
       
        SceneManager.LoadScene("VictoryScene");
    }
}
