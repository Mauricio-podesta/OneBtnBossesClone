using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PoolObjectType bulletType;
    [SerializeField] private Transform spawnShootPosition;
    [SerializeField] private GameObject enemy;
    [SerializeField] AudioClip shootsound;

    [Header("Stats")]
    [SerializeField] private float bulletForce;

    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
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
        // Obtener la bala del Object Pooling Manager
        GameObject newBullet = ObjectPoolingManager.Instance.GetPooledObject(bulletType);

        // Configurar la posición y activarla
        if (newBullet != null)
        {
            newBullet.transform.position = spawnShootPosition.position;
            newBullet.transform.rotation = Quaternion.identity;
            newBullet.SetActive(true);

            Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
            SoundManager.Instance.PlaySound(shootsound, this.transform.position);
            if (newBulletRb != null)
            {
                // Calcular la dirección de disparo hacia el enemigo
                Vector2 shootDirection = (enemy.transform.position - transform.position).normalized;

                // Aplicar fuerza a la bala
                newBulletRb.velocity = Vector2.zero; // Reiniciar la velocidad antes de aplicar la nueva fuerza
                newBulletRb.AddForce(shootDirection * bulletForce, ForceMode2D.Impulse);
            }

            // Desactivarla tras un tiempo usando una corrutina para devolverla al pool
            StartCoroutine(ReturnBulletToPool(newBullet, bulletType, 2f));
        }
    }

    IEnumerator ReturnBulletToPool(GameObject bullet, PoolObjectType type, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Devuelve el objeto al Object Pool
        ObjectPoolingManager.Instance.CoolObject(bullet, type);
    }
}
