using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    private static string selectedLevel; // Variable estática para mantener el nivel seleccionado

    // Método para seleccionar el nivel
    public void SelectLevel(string levelName)
    {
        selectedLevel = levelName;
        Debug.Log("Nivel seleccionado: " + selectedLevel);
    }

    // Método para cambiar a la escena seleccionada
    public void ChangeToSelectedLevel()
    {
        if (!string.IsNullOrEmpty(selectedLevel))
        {
            SceneManager.LoadScene(selectedLevel);
        }
        else
        {
            Debug.LogWarning("No se ha seleccionado ningún nivel.");
        }
    }


}

