using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    private static string selectedScene; // Variable estática para mantener el nivel seleccionado

    // Método para seleccionar el nivel
    public void SelectScene(string sceneName)
    {
        selectedScene = sceneName;
       
    }

    // Método para cambiar a la escena seleccionada
    public void ChangeToSelectedScene()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            SceneManager.LoadScene(selectedScene);
        }
        
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}

