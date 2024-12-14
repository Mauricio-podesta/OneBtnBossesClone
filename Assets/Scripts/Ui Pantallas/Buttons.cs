using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void SceneMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void LevelOne()
    {
        SceneManager.LoadScene("GameScene");
    }
    

    public void Play()
    {
        SceneManager.LoadScene("Login");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

