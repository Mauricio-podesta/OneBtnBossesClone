using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float Hp = 3;
    public event Action OnPlayerDeath;
    public event Action<int> OnPlayerHurt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Cubo"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        Hp -= 1;
        OnPlayerHurt?.Invoke((int)Hp);
        if (Hp <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
        Debug.Log("Health: " + Hp);
    }

}
