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
            // Selecciona posiciones aleatorias y almacena los objetos instanciados
            List<Transform> selectedPathPoints = SelectRandomPathPoints();
            InstantiateObjects(selectedPathPoints);

            // Espera el tiempo especificado antes de destruir los objetos
            yield return new WaitForSeconds(repeatTime);

            // Destruye los objetos instanciados
            DestroyInstantiatedObjects();

            // Espera el tiempo adicional entre ciclos antes de instanciar nuevos objetos
            yield return new WaitForSeconds(delayBetweenCycles);
        }
    }

    // Método para seleccionar posiciones aleatorias del array PathPoints
    List<Transform> SelectRandomPathPoints()
    {
        int count = Random.Range(3, 6); // Número aleatorio entre 3 y 5
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

    // Método para instanciar objetos en las posiciones seleccionadas
    void InstantiateObjects(List<Transform> pathPoints)
    {
        foreach (Transform pathPoint in pathPoints)
        {
            GameObject instantiatedObject = Instantiate(box, pathPoint.position, Quaternion.identity);
            instantiatedObject.transform.SetParent(parentContainer.transform); // Asigna el objeto vacío como padre
            instantiatedObjects.Add(instantiatedObject); // Agrega el objeto instanciado a la lista
        }
    }

    // Método para destruir los objetos instanciados
    void DestroyInstantiatedObjects()
    {
        foreach (GameObject obj in instantiatedObjects)
        {
            Destroy(obj);
        }
        instantiatedObjects.Clear(); // Limpia la lista después de destruir los objetos
    }
}
