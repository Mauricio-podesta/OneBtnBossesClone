using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referencias")]
    public Transform[] PathPoints;
    [SerializeField] public Transform PathPlayer;

    [Header("Stats")]
    [SerializeField]public float movementSpeed = 5f;
    [SerializeField] float radio = 5f;
    [SerializeField] float distancebetweenpoint = 1f;

    public static bool movingForward = true;

    private int PathPointsIndex = 0;

    private LineRenderer lineRenderer;

    // Usado solo el nivel 1 para que se genere el circulo
    string nombreEscena = "Level 1";

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

            if (PathPlayer != null)
            {
                punto.transform.SetParent(PathPlayer);
            }

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
            if (ChangeDirection.movingForward)
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

