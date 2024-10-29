using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    private float Hp = 10;
    
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
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            Destroy(gameObject);         
        }
    }

}
