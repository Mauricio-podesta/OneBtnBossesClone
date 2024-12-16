using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float PlayerLife = 3;
    public event Action OnPlayerDeath;
    public event Action<int> OnPlayerHurt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Cubo"))
        {
            TakeDamage(1);
        }
    }
    public void TakeDamage(int damage)
    {
        PlayerLife -= damage;
        OnPlayerHurt?.Invoke((int)PlayerLife);
        if (PlayerLife <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
        Debug.Log("Health: " + PlayerLife);
    }
}
