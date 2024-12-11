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
    public void SceneGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ScenePlay1()
    {
        SceneManager.LoadScene("Login");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SingIn()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}

