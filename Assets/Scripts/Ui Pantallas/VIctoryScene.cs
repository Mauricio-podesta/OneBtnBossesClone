using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VIctoryScene : MonoBehaviour
{
    public TextMeshProUGUI gameTimeText; 
    public TextMeshProUGUI gameBestTimeText;
    public GameObject gameCongratulations;
    float gameTime;
    float gameBestTime;
    private void Start()
    {
        if (PlayerPrefs.HasKey("GameTime"))
        {
            gameTime = PlayerPrefs.GetFloat("GameTime");
            
        }
        if (PlayerPrefs.HasKey("BestGameTime"))
        {
            gameBestTime = PlayerPrefs.GetFloat("BestGameTime");
           
        }
        
        ShowVictoryScreen(gameTime,gameBestTime);
    }
    public void ShowVictoryScreen(float gameTime,float gameBestTime)
    {
        gameObject.SetActive(true);
        
        if (gameTime == gameBestTime)
            gameCongratulations.SetActive(true);
        
        DisplayGameTime(gameTime);
        DisplayBestGameTime(gameBestTime);
    }

    private void DisplayBestGameTime(float gameBestTime)
    {
        int minutes = Mathf.FloorToInt(gameBestTime / 60F);
        int seconds = Mathf.FloorToInt(gameBestTime % 60);
        int centiseconds = Mathf.FloorToInt((gameBestTime * 100) % 100);
        gameBestTimeText.text = string.Format("Best Time: {0:0}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }
    private void DisplayGameTime(float gameTime)
    {
        int minutes = Mathf.FloorToInt(gameTime / 60F);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        int centiseconds = Mathf.FloorToInt((gameTime * 100) % 100);

        gameTimeText.text = string.Format("Time: {0:0}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }
}
