using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    private bool Pause = false;
    public GameObject PausePanel;

    public void Awake()
    {
        PausePanel.SetActive(false);
        GetInstance();
    }
    public void GetInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void CambiarEstadoPausa()
    {
        Pause = !Pause;
        if (Pause)
        {
            PausarJuego();
        }
        else
        {
            ReanudarJuego();
        }
    }
    private void PausarJuego()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }

    
    private void ReanudarJuego()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
}
