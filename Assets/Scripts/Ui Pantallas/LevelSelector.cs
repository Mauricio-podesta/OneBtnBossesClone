using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    private static string selectedLevel; // Variable est�tica para mantener el nivel seleccionado

    // M�todo para seleccionar el nivel
    public void SelectLevel(string levelName)
    {
        selectedLevel = levelName;
        Debug.Log("Nivel seleccionado: " + selectedLevel);
    }

    // M�todo para cambiar a la escena seleccionada
    public void ChangeToSelectedLevel()
    {
        if (!string.IsNullOrEmpty(selectedLevel))
        {
            SceneManager.LoadScene(selectedLevel);
        }
        else
        {
            Debug.LogWarning("No se ha seleccionado ning�n nivel.");
        }
    }


}

