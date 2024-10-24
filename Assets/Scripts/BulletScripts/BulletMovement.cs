using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float BulletSpeed = 50f;
    [SerializeField] GameObject Enemytarget;
    Rigidbody2D Rb;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Enemytarget = GameObject.FindWithTag("Enemy");
    }

    void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        transform.position = Vector2.MoveTowards(transform.position, Enemytarget.transform.position, BulletSpeed * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {                        
            Destroy(gameObject);      
    }
}
