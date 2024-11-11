using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTriangle : MonoBehaviour
{
    [SerializeField] private GameObject triangle;
    private PlayerMovement playerMovement;

    public float repeatTime;
    public float startDelay;

    public Transform circleCenter;
    public float offsetDistance;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        Invoke(nameof(StartCoroutineWithDelay), startDelay);
    }

    void StartCoroutineWithDelay()
    {
        StartCoroutine(RepeatInstantiateAndDestroy());
    }



    IEnumerator RepeatInstantiateAndDestroy()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, playerMovement.PathPoints.Length);
            Transform randomPathPoint = playerMovement.PathPoints[randomIndex];

            Vector3 direction = (randomPathPoint.position - circleCenter.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f; 
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Vector3 spawnPosition = circleCenter.position;

            GameObject instantiatedObject = Instantiate(triangle, spawnPosition, rotation);

            yield return new WaitForSeconds(repeatTime);

            Destroy(instantiatedObject);
        }
    }
}
    

