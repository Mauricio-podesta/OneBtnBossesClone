using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : EnemyAttackBase
{
    public float delayBetweenCycles = 1f;
    private List<GameObject> activeObjects = new List<GameObject>();
    public override IEnumerator RepeatAttack()
    {
        while (true)
        {
            List<Transform> selectedPathPoints = SelectRandomPathPoints();
            ActivateObjects(selectedPathPoints);

            yield return new WaitForSeconds(repeatTime);

            DeactivateObjects();

            yield return new WaitForSeconds(delayBetweenCycles);
        }
    }
    private List<Transform> SelectRandomPathPoints()
    {
        int count = Random.Range(3, 6);
        List<Transform> selectedPathPoints = new List<Transform>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, playerMovement.PathPoints.Length);
            } while (selectedPathPoints.Contains(playerMovement.PathPoints[randomIndex]));

            selectedPathPoints.Add(playerMovement.PathPoints[randomIndex]);
        }

        return selectedPathPoints;
    }
    private void ActivateObjects(List<Transform> pathPoints)
    {
        foreach (Transform pathPoint in pathPoints)
        {
            GameObject pooledObject = ObjectPoolingManager.Instance.GetPooledObject(poolObjectType);

            if (pooledObject != null)
            {
                pooledObject.transform.position = pathPoint.position;
                pooledObject.transform.rotation = Quaternion.identity;
                pooledObject.SetActive(true);

                activeObjects.Add(pooledObject);
            }
        }
    }
    private void DeactivateObjects()
    {
        foreach (GameObject obj in activeObjects)
        {
            obj.SetActive(false);
            ObjectPoolingManager.Instance.CoolObject(obj, poolObjectType);
        }
        activeObjects.Clear();
    }
}
