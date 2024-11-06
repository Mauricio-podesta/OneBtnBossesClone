using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShowdirecction : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int cantidadDepuntos;
    
    
    Transform[] PathPoints;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        PathPoints = new Transform[cantidadDepuntos];
    }

  
    void Update()
    {
      
    }

    void Distribuirpuntos()
    {
        for (int i = 0; i < PathPoints.Length; i++)
        {
            float x = PathPoints[i].position.x;
            float y = PathPoints[i].position.y;
            
            Vector3 posicion = new Vector3(x, y, 0) + transform.position;
            GameObject punto = new GameObject($"PointBullet {i}");
            punto.transform.position = posicion;
            PathPoints[i] = punto.transform;
        }

    }
    void DibujarLinea()
    {
        if (lineRenderer == null || PathPoints.Length == 0) return;

        lineRenderer.positionCount = PathPoints.Length;
        for (int i = 0; i < PathPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, PathPoints[i].position);
        }
    }
}
