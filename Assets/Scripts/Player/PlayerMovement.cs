using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform[] PathPoints;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float radio = 5f;
    [SerializeField] float distancebetweenpoint = 1f;
    private int PathPointsIndex = 0;
    private bool movingForward = true;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DistribuirObjetosCircularmente();
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
        PathPoints = new Transform[cantidad]; // Inicializar el array

        for (int i = 0; i < cantidad; i++)
        {
            // Calcular el ángulo en radianes
            float angulo = i * Mathf.PI * 2 / cantidad;

            // Calcular la posición en el círculo (solo en el plano XY)
            float x = Mathf.Cos(angulo) * radio;
            float y = Mathf.Sin(angulo) * radio;

            // Crear una nueva posición
            Vector3 posicion = new Vector3(x, y, 0) + transform.position;

            // Crear un objeto vacío y asignarlo a PathPoints
            GameObject punto = new GameObject($"Point {i}");
            punto.transform.position = posicion;
            PathPoints[i] = punto.transform; // Asignar el transform al array
        }
    }

    void DibujarLinea()
    {
        if (lineRenderer == null || PathPoints.Length == 0) return;

        lineRenderer.positionCount = PathPoints.Length; // Establecer la cantidad de puntos en la línea
        for (int i = 0; i < PathPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, PathPoints[i].position); // Establecer la posición de cada punto
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

