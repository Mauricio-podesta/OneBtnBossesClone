using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyLife : MonoBehaviour
{
    public float enemyLife = 20;
  
    [SerializeField] private Slider healthSliderEnemy;
    public event Action OnEnemyDeath;
    private void Start()
    {
        UpdateHealthUI();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
           
        }
    }
    public void TakeDamage(int damage)
    {
        enemyLife -= damage;
        if (enemyLife <= 0)
        {
            OnDeath();
            Destroy(gameObject);         
        }
        UpdateHealthUI();
    }
    void UpdateHealthUI()
    {
        enemyLife = Mathf.Clamp(enemyLife, 0, 100);
        healthSliderEnemy.value = enemyLife;

    }
    private void OnDeath()
    {
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath.Invoke();
        }   
    }
}
