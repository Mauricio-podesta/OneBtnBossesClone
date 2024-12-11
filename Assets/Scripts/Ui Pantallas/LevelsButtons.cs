using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsButtons : MonoBehaviour
{
    public static LevelsButtons Instance;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button powerUpButton;  
    [SerializeField] private Button noPowerUpButton;
    [SerializeField] private Button cancelButton;

    private AudioSource audioSource;

    public bool usePowerUp { get; private set; }
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
    private void Awake()
    {
        GetInstance();

        if (powerUpButton != null)
        {
            powerUpButton.onClick.AddListener(() => SelectOption(true));
        }
        if (noPowerUpButton != null)
        {
            noPowerUpButton.onClick.AddListener(() => SelectOption(false));
        }
        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(CloseChoicePanel);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void ShowChoicePanel()
    {
        if (choicePanel != null)
        {
            choicePanel.SetActive(true);
        }
    }
    private void CloseChoicePanel()
    {
        if (choicePanel != null)
        {
            choicePanel.SetActive(false);
        }
    }
    
        private void SelectOption(bool usePowerUpOption)
        {
            usePowerUp = usePowerUpOption;

            if (usePowerUp)
            {
                Debug.Log("Movimiento con Power-Up seleccionado");
            }
            else
            {
                Debug.Log("Movimiento sin Power-Up seleccionado");
            }

            ScenePlay();
        }

    

    public void ScenePlay()
    {
        SceneManager.LoadScene("GameScene");
    }
}
