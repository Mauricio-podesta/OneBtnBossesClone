using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementChoiceButtons : MonoBehaviour
{
    public static MovementChoiceButtons Instance;
    [SerializeField] private GameObject choiceCanvas;
    [SerializeField] private Button powerUpButton;
    [SerializeField] private Button noPowerUpButton;
    
    [SerializeField] private Button sceneChangeButton; 
       
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

        powerUpButton.onClick.AddListener(() => SelectOption(true));
        
        noPowerUpButton.onClick.AddListener(() => SelectOption(false));
        
        sceneChangeButton.gameObject.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void SelectOption(bool usePowerUpOption)
    {
        usePowerUp = usePowerUpOption;

        sceneChangeButton.gameObject.SetActive(true);

    }
}
