using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform[] PathPoints;
    [SerializeField] float movementSpeed = 5f;
    private int PathPointsIndex = 0;
    private bool movingForward = true;

    void Start()
    {
        transform.position = PathPoints[PathPointsIndex].transform.position;

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            movingForward = !movingForward;
        }
        Movement();
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

