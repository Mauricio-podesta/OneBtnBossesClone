using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTriangle : EnemyAttackBase
{
    public Transform circleCenter;
    public float offsetDistance;

    public override IEnumerator RepeatAttack()
    {
        while (true)
        {
            Transform randomPathPoint = GetRandomPathPoint();
            Quaternion rotation = CalculateRotation(randomPathPoint);

            GameObject pooledObject = ActivatePooledObject(rotation);

            yield return new WaitForSeconds(repeatTime);

            DeactivatePooledObject(pooledObject);
        }
    }

    private Transform GetRandomPathPoint()
    {
        int randomIndex = Random.Range(0, playerMovement.PathPoints.Length);
        return playerMovement.PathPoints[randomIndex];
    }
    private Quaternion CalculateRotation(Transform targetPoint)
    {
        Vector3 direction = (targetPoint.position - circleCenter.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        return Quaternion.Euler(0, 0, angle);
    }
    private GameObject ActivatePooledObject(Quaternion rotation)
    {
        GameObject pooledObject = ObjectPoolingManager.Instance.GetPooledObject(poolObjectType);
        pooledObject.transform.position = circleCenter.position;
        pooledObject.transform.rotation = rotation;
        pooledObject.SetActive(true);
        return pooledObject;
    }
    private void DeactivatePooledObject(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
        ObjectPoolingManager.Instance.CoolObject(pooledObject, poolObjectType);
    }
}
    

