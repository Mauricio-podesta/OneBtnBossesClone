using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    [SerializeField] private PoolObjectType boxType;
    private PlayerMovement playerMovement;

    public float repeatTime;
    public float startDelay;
    public float delayBetweenCycles = 1f;

    public float fadeDuration = 1f;


    private List<GameObject> activeObjects = new List<GameObject>();
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
            List<Transform> selectedPathPoints = SelectRandomPathPoints();
            ActivateObjects(selectedPathPoints);

            yield return new WaitForSeconds(repeatTime);

            DeactivateObjects();

            yield return new WaitForSeconds(delayBetweenCycles);
        }
    }

    List<Transform> SelectRandomPathPoints()
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

    void ActivateObjects(List<Transform> pathPoints)
    {
        foreach (Transform pathPoint in pathPoints)
        {
            GameObject pooledObject = ObjectPoolingManager.Instance.GetPooledObject(boxType);

            if (pooledObject != null)
            {
                pooledObject.transform.position = pathPoint.position;
                pooledObject.transform.rotation = Quaternion.identity;
                pooledObject.SetActive(true);

                activeObjects.Add(pooledObject); // Registrar el objeto activado
            }
        }
    }

    void DeactivateObjects()
    {
        foreach (GameObject obj in activeObjects)
        {
            obj.SetActive(false); // Desactivar el objeto
            ObjectPoolingManager.Instance.CoolObject(obj, boxType); // Retornar al pool
        }
        activeObjects.Clear(); // Limpiar la lista de objetos activos
    }
    
}
