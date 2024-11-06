using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject Bulletprefab;
    [SerializeField] private Transform SpawnShootPosition;
   

    [Header("Stats")]
    [SerializeField] private float BulletForce;
    private LineRenderer lineRenderer;

    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
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
    // Update is called once per frame
    void Update()
    {
        
    }
    

    void Shoot()
    {
        GameObject newBullet = Instantiate(Bulletprefab, SpawnShootPosition.position, Quaternion.identity);
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();

        float randomAngle = Random.Range(-180f, 180f);

        float angleRad = randomAngle * Mathf.Deg2Rad;

        Vector2 shootDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));


        newBulletRb.AddForce(shootDirection * BulletForce, ForceMode2D.Impulse);
        Destroy(newBullet, 2f); 
    }
}
