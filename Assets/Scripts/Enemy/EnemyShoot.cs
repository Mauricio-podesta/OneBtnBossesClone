using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PoolObjectType bulletType;
    [SerializeField] private Transform SpawnShootPosition;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Stats")]
    [SerializeField] private float BulletForce;
    [SerializeField] private int numberOfPoints = 50;

    private void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        StartCoroutine(Ishoot());
    }
    IEnumerator Ishoot()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            float randomAngle = Random.Range(-180f, 180f);
            float angleRad = randomAngle * Mathf.Deg2Rad;
            ShowTrajectory(angleRad);

            yield return new WaitForSeconds(0.5f);

            Shoot(angleRad);

            HideTrajectory();

            yield return new WaitForSeconds(0.3f);
        }
    }
    void Shoot(float angleRad)
    {
       
        GameObject newBullet = ObjectPoolingManager.Instance.GetPooledObject(bulletType);

        if (newBullet != null) 
        {
            newBullet.transform.position = SpawnShootPosition.position;
            newBullet.transform.rotation = Quaternion.identity;
            newBullet.SetActive(true);

            Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();

            Vector2 shootDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            newBulletRb.velocity = Vector2.zero; 
            newBulletRb.AddForce(shootDirection * BulletForce, ForceMode2D.Impulse);
  
            StartCoroutine(DisableBulletAfterTime(newBullet, 2f));
        }
    }
    IEnumerator DisableBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        
        ObjectPoolingManager.Instance.CoolObject(bullet, bulletType);
    }
    void ShowTrajectory(float angleRad)
    {
        Vector3[] trajectoryPoints = new Vector3[numberOfPoints];

        Vector2 initialVelocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * BulletForce;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float time = i * 0.1f; 

            float x = initialVelocity.x * time;
            float y = initialVelocity.y * time;
            
            trajectoryPoints[i] = SpawnShootPosition.position + new Vector3(x, y, 0f);
        }
       
        lineRenderer.positionCount = trajectoryPoints.Length;
        lineRenderer.SetPositions(trajectoryPoints);
    } 
    void HideTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}
