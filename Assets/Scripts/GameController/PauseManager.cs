using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    private bool isPause = false;
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
        }
    }
    public void TogglePauseState()
    {
        isPause = !isPause;
        if (isPause)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
}
