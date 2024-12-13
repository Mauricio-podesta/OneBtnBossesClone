using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float BulletSpeed = 50f;
    [SerializeField] GameObject Enemytarget;
    [SerializeField] private PoolObjectType bulletType;

    void Start()
    {
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
        ObjectPoolingManager.Instance.CoolObject(gameObject, bulletType);
    }
}
