using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Referencias relacionadas con el tiempo
    [Header("Time Management")]
    [SerializeField] private TextMeshProUGUI timeText; // Referencia al componente de texto en la UI para el tiempo de juego

    // Referencias relacionadas con los canvas
    [Header("Victory and Defeat Canvases")]
    [SerializeField] private GameObject victoryCanvas; // Canvas de victoria
    [SerializeField] private GameObject defeatCanvas; // Canvas de derrota

    // Referencias relacionadas con la información de victoria
    [Header("Victory Canvas Elements")]
    [SerializeField] private TextMeshProUGUI victoryTimeText; // Texto para mostrar el tiempo en el canvas de victoria
    [SerializeField] private TextMeshProUGUI bestTimeText; // Texto para mostrar el mejor tiempo en el canvas de victoria
    [SerializeField] private TextMeshProUGUI messageText; // Texto para mostrar el mensaje de felicitaciones

    private float gameTime; // Tiempo de juego en segundos
    private float bestTime; // Mejor tiempo registrado

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
        gameTime = 0f; // Inicializa el tiempo de juego
    }

    private void UpdateGameTime()
    {
        gameTime += Time.deltaTime; // Incrementa el tiempo de juego
    }

    private void DisplayGameTime()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F); // Calcula los minutos
        int seconds = Mathf.FloorToInt(gameTime % 60F); // Calcula los segundos
        int milliseconds = Mathf.FloorToInt((gameTime * 1000) % 1000); // Calcula los milisegundos

        // Actualiza el texto en pantalla
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
        victoryTimeText.text = "Tiempo: " + currentTime;

        if (gameTime < bestTime)
        {
            bestTime = gameTime;
            SaveBestTime();
            messageText.text = "¡Felicidades! ¡Nuevo récord!";
        }
        else
        {
            messageText.text = "¡Buen trabajo!";
        }

        int bestMinutes = Mathf.FloorToInt(bestTime / 60F);
        int bestSeconds = Mathf.FloorToInt(bestTime % 60F);
        int bestMilliseconds = Mathf.FloorToInt((bestTime * 1000) % 1000);

        string bestTimeString = string.Format("{0:00}:{1:00}:{2:000}", bestMinutes, bestSeconds, bestMilliseconds);
        bestTimeText.text = "Mejor Tiempo: " + bestTimeString;
    }
}
