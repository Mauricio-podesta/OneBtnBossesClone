using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Transform heartsContainer;
    public GameObject[] hearts;
    private int heartsCount;

    private void Start()
    {
        InitializeHearts();
    }

    private void InitializeHearts()
    {
        heartsCount = heartsContainer.childCount;
        hearts = new GameObject[heartsCount];

        for (int i = 0; i < heartsCount; i++)
        {
            hearts[i] = heartsContainer.GetChild(i).gameObject;
        }
    }

    public void DecreaseHealth(int playerCurrentHealth)
    {
        if (playerCurrentHealth >= 0 && playerCurrentHealth < hearts.Length)
        {
            hearts[playerCurrentHealth].SetActive(false);
        }
    }
}
