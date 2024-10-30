using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject Bulletprefab;
    [SerializeField] private Transform SpawnShootPosition;
    [SerializeField] private GameObject Player;

    [Header("Stats")]
    [SerializeField] private float BulletForce;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(Bulletprefab, SpawnShootPosition.position, Quaternion.identity);
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
        Vector2 shootDirection = (Player.transform.position - transform.position).normalized;
        newBulletRb.AddForce(shootDirection * BulletForce, ForceMode2D.Impulse);
        Destroy(newBullet, 2f); 
    }
}
