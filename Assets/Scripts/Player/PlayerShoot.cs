using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject Bulletprefab;
    [SerializeField] private Transform SpawnShootPosition;
    [SerializeField] private GameObject Enemy;

    [Header("Stats")]
    [SerializeField] private float BulletForce;

    void Start()
    {
        Enemy = GameObject.FindWithTag("Enemy");
        StartCoroutine(Ishoot());
    }

    IEnumerator Ishoot()
    {
        yield return new WaitForSeconds(2f);
       
        while (true) 
        {
           
            Shoot();

           
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Update()
    {   
    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(Bulletprefab, SpawnShootPosition.position, Quaternion.identity);
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
        
        Vector2 shootDirection = (Enemy.transform.position - transform.position).normalized;
        newBulletRb.AddForce(shootDirection * BulletForce, ForceMode2D.Impulse);
        Destroy(newBullet, 2f);
    }
}
