using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject Bulletprefab;         
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

    private void Update()
    {
        
    }

   
    IEnumerator Ishoot()
    {
        yield return new WaitForSeconds(2f); 

        while (true)
        {
            float randomAngle = Random.Range(-180f, 180f);
            float angleRad = randomAngle * Mathf.Deg2Rad;

            ShowTrajectory(angleRad);

          
            yield return new WaitForSeconds(1f);

           
            Shoot(angleRad);

          
            HideTrajectory();

          
            yield return new WaitForSeconds(0.5f);
        }
    }

   
    void Shoot(float angleRad)
    {
        GameObject newBullet = Instantiate(Bulletprefab, SpawnShootPosition.position, Quaternion.identity);
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();

     
      
        
        Vector2 shootDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

      
        newBulletRb.AddForce(shootDirection * BulletForce, ForceMode2D.Impulse);

       
        Destroy(newBullet, 2f);
    }

   
    void ShowTrajectory(float angleRad)
    {
        Vector3[] trajectoryPoints = new Vector3[numberOfPoints];

        // velocidad inicial del proyectil
       
        Vector2 initialVelocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * BulletForce;

       
        for (int i = 0; i < numberOfPoints; i++)
        {
            float time = i * 0.1f; // El tiempo entre cada punto

           
            float x = initialVelocity.x * time;
            float y = initialVelocity.y * time;

            // Asignamos la posici�n de cada punto de la trayectoria
            trajectoryPoints[i] = SpawnShootPosition.position + new Vector3(x, y, 0f);
        }

        // Actualizamos la posici�n de la trayectoria en el LineRenderer
        lineRenderer.positionCount = trajectoryPoints.Length;
        lineRenderer.SetPositions(trajectoryPoints);
    }

    // M�todo para ocultar la trayectoria de la bala
    void HideTrajectory()
    {
        lineRenderer.positionCount = 0; // Ocultamos la trayectoria desactivando el LineRenderer
    }
}
