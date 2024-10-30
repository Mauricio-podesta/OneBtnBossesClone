using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    private float Hp = 3;
    public event Action OnPlayerDeath;

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
        Destroy(gameObject);
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
