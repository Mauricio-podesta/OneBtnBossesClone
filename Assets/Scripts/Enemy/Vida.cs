using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    private float Hp = 10;
    [SerializeField] private Slider healthSliderEnemy;

    private void Start()
    {
        UpdateHealthUI();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
           
        }
    }
    public void TakeDamage()
    {
        Hp -= 1;
        if (Hp <= 0)
        {
            Destroy(gameObject);         
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        Hp = Mathf.Clamp(Hp, 0, 100);
        healthSliderEnemy.value = Hp;

    }


}
