using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VIctoryScene : MonoBehaviour
{
    public TextMeshProUGUI gameTimeText; // Arrastra el objeto de texto de tiempo en el Inspector

    public void ShowVictoryScreen(float gameTime)
    {
        gameObject.SetActive(true); // Activa la pantalla de victoria
        DisplayGameTime(gameTime);
    }

    private void DisplayGameTime(float gameTime)
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        int centiseconds = Mathf.FloorToInt((gameTime * 100) % 100);

        gameTimeText.text = string.Format("Tiempo de juego: {0:0}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }
}
