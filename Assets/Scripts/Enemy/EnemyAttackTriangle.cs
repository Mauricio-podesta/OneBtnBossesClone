using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTriangle : MonoBehaviour
{
    [SerializeField] private PoolObjectType poolObjectType;
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
        StartCoroutine(RepeatPoolAndReturn());
    }



    IEnumerator RepeatPoolAndReturn()
    {
        while (true)
        {
            // Elegir un punto aleatorio del PathPoints
            int randomIndex = Random.Range(0, playerMovement.PathPoints.Length);
            Transform randomPathPoint = playerMovement.PathPoints[randomIndex];

            // Calcular dirección y rotación
            Vector3 direction = (randomPathPoint.position - circleCenter.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            // Obtener el objeto del pool
            GameObject pooledObject = ObjectPoolingManager.Instance.GetPooledObject(poolObjectType);

            // Ajustar posición, rotación y activar el objeto
            pooledObject.transform.position = circleCenter.position;
            pooledObject.transform.rotation = rotation;
            pooledObject.SetActive(true);

            // Esperar el tiempo definido antes de desactivar el objeto
            yield return new WaitForSeconds(repeatTime);

            // Devolver el objeto al pool
            ObjectPoolingManager.Instance.CoolObject(pooledObject, poolObjectType);
        }
    }
}
    

