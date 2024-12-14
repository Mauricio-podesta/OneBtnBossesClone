using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    [SerializeField] private string sceneToLoad;

    public void SceneMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void LevelOne()
    {
        SceneManager.LoadScene("Level 1");
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

