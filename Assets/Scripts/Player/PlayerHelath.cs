using UnityEngine;
using System;

public class PlayerHelath : MonoBehaviour
{
    private float Hp = 3;
    public event Action OnPlayerDeath;

    private void Start()
    {
        Debug.Log("Health: " + Hp);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage();

        }
    }
    public void TakeDamage()
    {
        Hp -= 1;
        if (Hp <= 0)
        {
            OnDeath();
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
