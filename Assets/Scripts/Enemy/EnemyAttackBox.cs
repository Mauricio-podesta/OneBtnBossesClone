using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    [SerializeField] private GameObject box;
    private PlayerMovement playerMovement;

    public float repeatTime;
    public float startDelay;

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
        while(true)
        {
            // Determina la cantidad de posiciones aleatorias (entre 3 y 5)
            int count = Random.Range(3, 6);

            // Crea una lista para almacenar las posiciones seleccionadas
            List<Transform> selectedPathPoints = new List<Transform>();
            List<GameObject> instantiatedObjects = new List<GameObject>();

            // Selecciona posiciones aleatorias sin repetición
            for (int i = 0; i < count; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, playerMovement.PathPoints.Length);
                } while (selectedPathPoints.Contains(playerMovement.PathPoints[randomIndex]));

                selectedPathPoints.Add(playerMovement.PathPoints[randomIndex]);
            }

            // Instancia los objetos en las posiciones seleccionadas y almacena las referencias
            foreach (Transform pathPoint in selectedPathPoints)
            {
                GameObject instantiatedObject = Instantiate(box, pathPoint.position, Quaternion.identity);
                instantiatedObjects.Add(instantiatedObject);
            }

            // Espera el tiempo especificado antes de destruir los objetos
            yield return new WaitForSeconds(repeatTime);

            // Destruye los objetos instanciados específicamente
            foreach (GameObject obj in instantiatedObjects)
            {
                Destroy(obj);
            }
        }
    }
}
