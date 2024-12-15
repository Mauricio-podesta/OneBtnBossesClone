using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsButtons : MonoBehaviour
{
    public static LevelsButtons Instance;
    [SerializeField] private GameObject choiceCanvas;
    [SerializeField] private Button powerUpButton;
    [SerializeField] private Button noPowerUpButton;
    
    [SerializeField] private Button sceneChangeButton; // Botón para cambiar de escena
    [SerializeField] private string sceneToLoad;       // Nombre de la escena a cargar


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

        sceneChangeButton.gameObject.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void SelectOption(bool usePowerUpOption)
    {
        usePowerUp = usePowerUpOption;

        sceneChangeButton.gameObject.SetActive(true);

    }

   


}
