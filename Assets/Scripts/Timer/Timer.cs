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
       
        if (PlayerPrefs.HasKey("BestGameTime"))
        {
            gameBestTime = PlayerPrefs.GetFloat("BestGameTime");

        }
        Debug.Log(gameBestTime);
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
        if (!isEnemyAlive) 
        {
            UpdateBestTime();
            
            Debug.Log(gameBestTime);
        }
       
    }
    void UpdateBestTime()
    {
        if (gameBestTime > gameTime || gameBestTime == 0)
        {
            gameBestTime = gameTime;
            PlayerPrefs.SetFloat("BestGameTime", gameBestTime);
            PlayerPrefs.Save();
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

        PlayerPrefs.SetString("GameScene", gamescene);
        PlayerPrefs.Save();


        SceneManager.LoadScene("LoseScene");
    }
    private void HandleEnemyDeath()
    {
        isEnemyAlive = false;

        PlayerPrefs.SetFloat("GameTime", gameTime);
        PlayerPrefs.Save();

       

        PlayerPrefs.SetString("GameScene", gamescene);
        PlayerPrefs.Save();

        SceneManager.LoadScene("VictoryScene");
    }
}
