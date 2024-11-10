using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Path Movement")]
    [SerializeField] Transform[] PathPoints;
    private int PathPointsIndex = 0;
    [SerializeField] float distancebetweenpoint = 1f;
    [SerializeField] float radio = 5f;
    private LineRenderer lineRenderer;

    [Header("Movement Stats")]
    [SerializeField] float movementSpeed = 5f;
    private bool movingForward = true;

    string nombreEscena = "GameScene";
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (SceneManager.GetActiveScene().name == nombreEscena)
        { DistribuirObjetosCircularmente(); }
        if (PathPoints.Length > 0)
        {
            transform.position = PathPoints[PathPointsIndex].position;
        }
        DibujarLinea();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            movingForward = !movingForward;
        }
        Movement();
    }

    void DistribuirObjetosCircularmente()
    {
        int cantidad = Mathf.FloorToInt(2 * Mathf.PI * radio / distancebetweenpoint);
        PathPoints = new Transform[cantidad]; 

        for (int i = 0; i < cantidad; i++)
        {
            float angulo = i * Mathf.PI * 2 / cantidad;

            
            float x = Mathf.Cos(angulo) * radio;
            float y = Mathf.Sin(angulo) * radio;

            
            Vector3 posicion = new Vector3(x, y, 0) + transform.position;

           
            GameObject punto = new GameObject($"Point {i}");
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



    void Movement()
    {
        if (PathPoints.Length == 0) return;


        transform.position = Vector3.MoveTowards(transform.position, PathPoints[PathPointsIndex].position, movementSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, PathPoints[PathPointsIndex].position) < 0.1f)
        {
            if (movingForward)
            {
                PathPointsIndex++;
                if (PathPointsIndex >= PathPoints.Length)
                {
                    PathPointsIndex = 0;
                }
            }
            else
            {
                PathPointsIndex--;
                if (PathPointsIndex < 0)
                {
                    PathPointsIndex = PathPoints.Length - 1;
                }
            }
        }
    }
}

