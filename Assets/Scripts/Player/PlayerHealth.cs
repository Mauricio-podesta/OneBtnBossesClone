using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float Hp = 3;
    public event Action OnPlayerDeath;

   
    public void TakeDamage()
    {
        Hp -= 1;
        if (Hp <= 0)
        {
            OnDeath();
            Destroy(gameObject);
        }
        Debug.Log("Health: " + Hp);


    }


    private void OnDeath()
    {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath.Invoke();
        }
        else
        {
            Debug.Log("Null player health");
        }

    }

}
