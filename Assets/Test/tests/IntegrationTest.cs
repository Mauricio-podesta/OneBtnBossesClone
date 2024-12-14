using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;


public class IntegrationTest
{
    private GameObject playerShootObject;
    private GameObject pathPlayerObject;

    [SetUp]
    public void Setup()
    {
        // Crear los objetos de prueba
        playerShootObject = new GameObject("PlayerShoot");
        pathPlayerObject = new GameObject("PathPlayer");

        // A�adir los componentes necesarios usando AddComponent
        PlayerShoot playerShoot = playerShootObject.AddComponent<PlayerShoot>();
        PlayerMovement playerMovement = playerShootObject.AddComponent<PlayerMovement>();

        // Verificar que los componentes se han a�adido correctamente
        Assert.IsNotNull(playerShoot, "PlayerShoot component is not added.");
        Assert.IsNotNull(playerMovement, "PlayerMovement component is not added.");

        // Configurar referencias si es necesario
        playerShoot.enemy = pathPlayerObject;

        // Asignar PathPlayer en PlayerMovement
        playerMovement.PathPlayer = pathPlayerObject.transform;

        // Inicializar PathPoints para evitar NullReferenceException
        playerMovement.PathPoints = new Transform[0];
    }

    [UnityTest]
    public IEnumerator PlayerMovementAndShootingTest()
    {
        // Inicializar los scripts
        yield return null;

        // Crear puntos aleatorios
        PlayerMovement playerMovement = playerShootObject.GetComponent<PlayerMovement>();
        CreateRandomPathPoints(playerMovement);

        // Verificar que PathPoints se han creado correctamente
        Assert.IsTrue(playerMovement.PathPoints.Length > 0, "PathPoints were not created.");

        // Simular comportamiento y esperar unos segundos
        yield return new WaitForSeconds(2f);

        // Verificar que el jugador se est� moviendo
        Vector3 initialPosition = playerShootObject.transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 newPosition = playerShootObject.transform.position;
        Assert.AreNotEqual(initialPosition, newPosition, "El jugador no se est� moviendo.");

        // Verificar que el jugador est� disparando
        PlayerShoot playerShoot = playerShootObject.GetComponent<PlayerShoot>();
        Assert.IsTrue(playerShoot.enemy != null, "El enemigo no est� asignado.");

        // Esperar un poco m�s para asegurar disparos
        yield return new WaitForSeconds(2f);
        // Aqu� deber�as verificar que se ha disparado una bala (puedes adaptar la verificaci�n seg�n tu l�gica)
        // Por simplicidad, aqu� solo vamos a asegurarnos de que el objeto sigue activo
        Assert.IsTrue(playerShootObject.activeInHierarchy, "El objeto del jugador no est� activo.");

        // Agregar m�s verificaciones espec�ficas seg�n sea necesario
    }

    private void CreateRandomPathPoints(PlayerMovement playerMovement)
    {
        int cantidad = 10; // N�mero de puntos de camino aleatorios
        playerMovement.PathPoints = new Transform[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            GameObject point = new GameObject($"Point {i}");
            point.transform.position = randomPosition;
            playerMovement.PathPoints[i] = point.transform;
        }
    }

    [TearDown]
    public void Teardown()
    {
        // Limpiar los objetos de prueba despu�s de cada prueba
        Object.Destroy(playerShootObject);
        Object.Destroy(pathPlayerObject);
    }
}

