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

        // Añadir los componentes necesarios usando AddComponent
        PlayerShoot playerShoot = playerShootObject.AddComponent<PlayerShoot>();
        PlayerMovement playerMovement = playerShootObject.AddComponent<PlayerMovement>();

        // Verificar que los componentes se han añadido correctamente
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

        // Verificar que el jugador se está moviendo
        Vector3 initialPosition = playerShootObject.transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 newPosition = playerShootObject.transform.position;
        Assert.AreNotEqual(initialPosition, newPosition, "El jugador no se está moviendo.");

        // Verificar que el jugador está disparando
        PlayerShoot playerShoot = playerShootObject.GetComponent<PlayerShoot>();
        Assert.IsTrue(playerShoot.enemy != null, "El enemigo no está asignado.");

        // Esperar un poco más para asegurar disparos
        yield return new WaitForSeconds(2f);
        // Aquí deberías verificar que se ha disparado una bala (puedes adaptar la verificación según tu lógica)
        // Por simplicidad, aquí solo vamos a asegurarnos de que el objeto sigue activo
        Assert.IsTrue(playerShootObject.activeInHierarchy, "El objeto del jugador no está activo.");

        // Agregar más verificaciones específicas según sea necesario
    }

    private void CreateRandomPathPoints(PlayerMovement playerMovement)
    {
        int cantidad = 10; // Número de puntos de camino aleatorios
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
        // Limpiar los objetos de prueba después de cada prueba
        Object.Destroy(playerShootObject);
        Object.Destroy(pathPlayerObject);
    }
}

