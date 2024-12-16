using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Time Management")]
    [SerializeField] private TextMeshProUGUI timeText; 
    
    [Header("Victory and Defeat Canvases")]
    [SerializeField] private GameObject victoryCanvas; 
    [SerializeField] private GameObject defeatCanvas;

    [Header("Victory Canvas Elements")]
    [SerializeField] private TextMeshProUGUI victoryTimeText; 
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI messageText;

    private float gameTime;
    private float bestTime;

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
    private void Awake()
    {
        GetInstance();
        Time.timeScale = 1;    
    }
    void Start()
    {
        InitializeGameTime();

        victoryCanvas.SetActive(false); 
        defeatCanvas.SetActive(false);

        LoadBestTime();
    }
    void Update()
    {
        UpdateGameTime();
        DisplayGameTime();
    }
    private void InitializeGameTime()
    {
        gameTime = 0f; 
    }
    private void UpdateGameTime()
    {
        gameTime += Time.deltaTime; 
    }
    private void DisplayGameTime()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F); 
        int seconds = Mathf.FloorToInt(gameTime % 60F); 
        int milliseconds = Mathf.FloorToInt((gameTime * 1000) % 1000); 

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
    private void LoadBestTime()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
    }
    private void SaveBestTime()
    {
        PlayerPrefs.SetFloat("BestTime", bestTime);
    }
    public void HandleEnemyDeath()
    {
        Time.timeScale = 0f;
        victoryCanvas.SetActive(true);
        DisplayVictoryInfo();
    }
    public void HandlePlayerDeath()
    {
        Time.timeScale = 0f;
        defeatCanvas.SetActive(true);
    }
    private void DisplayVictoryInfo()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60F);
        int milliseconds = Mathf.FloorToInt((gameTime * 1000) % 1000);

        string currentTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        victoryTimeText.text = "Time: " + currentTime;

        if (gameTime < bestTime)
        {
            bestTime = gameTime;
            SaveBestTime();
            messageText.text = "¡Congratulations! ¡New Record!";
        }
        else
        {
            messageText.text = "¡Good Job!";
        }

        int bestMinutes = Mathf.FloorToInt(bestTime / 60F);
        int bestSeconds = Mathf.FloorToInt(bestTime % 60F);
        int bestMilliseconds = Mathf.FloorToInt((bestTime * 1000) % 1000);

        string bestTimeString = string.Format("{0:00}:{1:00}:{2:000}", bestMinutes, bestSeconds, bestMilliseconds);
        bestTimeText.text = "Best Time: " + bestTimeString;
    }
}
