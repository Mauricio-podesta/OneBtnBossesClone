using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    [SerializeField] private GameObject box;
    private PlayerMovement playerMovement;

    public float repeatTime;
    public float startDelay;
    public float delayBetweenCycles = 1f;

    public float fadeDuration = 1f;

    public GameObject parentContainer;

    private List<GameObject> instantiatedObjects = new List<GameObject>();

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
            InstantiateObjects(selectedPathPoints);

            yield return new WaitForSeconds(repeatTime);

            DestroyInstantiatedObjects();

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

    void InstantiateObjects(List<Transform> pathPoints)
    {
        foreach (Transform pathPoint in pathPoints)
        {
            GameObject instantiatedObject = Instantiate(box, pathPoint.position, Quaternion.identity);
            instantiatedObject.transform.SetParent(parentContainer.transform); 
            instantiatedObjects.Add(instantiatedObject); 
        }
    }

    void DestroyInstantiatedObjects()
    {
        foreach (GameObject obj in instantiatedObjects)
        {
            Destroy(obj);
        }
        instantiatedObjects.Clear(); 
    }
}
