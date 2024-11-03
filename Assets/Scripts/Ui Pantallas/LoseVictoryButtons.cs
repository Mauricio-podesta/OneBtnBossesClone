using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseVictoryButtons : MonoBehaviour
{
    private string gamescene;
    private void Start()
    {
        if (PlayerPrefs.HasKey("GameScene"))
        {
            gamescene = PlayerPrefs.GetString("GameScene");
        }
    }
    public void SceneMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void SceneGame()
    {
        SceneManager.LoadScene(gamescene);
    }

}
