using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class IntegrationTest : InputTestFixture
{
    private GameObject player;
    private PlayerMovement playerMovement;

    [SetUp]
    public void Setup()
    {
        // Crear un objeto de prueba para el jugador
        player = new GameObject();

        // A�adir el script de PlayerMovement
        playerMovement = player.AddComponent<PlayerMovement>();

        // Crear un PathPlayer vac�o para pasar a la referencia del script
        GameObject pathPlayer = new GameObject("PathPlayer");
        playerMovement.PathPlayer = pathPlayer.transform;

        // Definir algunos valores para las pruebas
        playerMovement.radio = 5f;
        playerMovement.distancebetweenpoint = 1f;
    }

    [Test]
    public void TestDistribuirObjetosCircularmente()
    {
        // Ejecutar la funci�n para distribuir los puntos
        playerMovement.DistribuirObjetosCircularmente();

        // Verificar que PathPoints no est� vac�o
        Assert.IsNotEmpty(playerMovement.PathPoints, "PathPoints should not be empty.");

        // Verificar que los puntos est�n distribuidos de manera circular
        foreach (var point in playerMovement.PathPoints)
        {
            Assert.AreEqual(point.position.z, 0, "All points should be in the X-Y plane.");
            Assert.AreNotEqual(point.position.x, player.transform.position.x, "Points should be distributed along the circle.");
        }
    }

    [Test]
    public void TestMovement()
    {
        // Asumir que PathPoints ya se han distribuido
        playerMovement.DistribuirObjetosCircularmente();

        // Guardar la posici�n inicial
        Vector3 initialPosition = player.transform.position;

        // Ejecutar un paso de movimiento
        playerMovement.Movement();

        // Verificar que la posici�n haya cambiado (el jugador se debe mover a un punto cercano)
        Assert.AreNotEqual(initialPosition, player.transform.position, "Player should have moved to the next point.");
    }

    [Test]
    public void TestDibujarLinea()
    {
        // Asegurarse de que el LineRenderer est� presente
        LineRenderer lineRenderer = player.GetComponent<LineRenderer>();
        Assert.IsNotNull(lineRenderer, "LineRenderer should be attached to the player.");

        // Ejecutar el m�todo de dibujar la l�nea
        playerMovement.DibujarLinea();

        // Verificar que el LineRenderer tenga la cantidad correcta de puntos
        Assert.AreEqual(playerMovement.PathPoints.Length, lineRenderer.positionCount, "LineRenderer should have the same number of points as PathPoints.");
    }

    [TearDown]
    public void TearDown()
    {
        // Eliminar el objeto de prueba despu�s de cada test
        Object.Destroy(player);
    }
}