using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    private static string selectedScene; 
    public void SelectScene(string sceneName)
    {
        selectedScene = sceneName;
    }
    public void ChangeToSelectedScene()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(selectedScene);
        }  
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }


}

