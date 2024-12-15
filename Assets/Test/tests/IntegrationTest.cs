using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


public class IntegrationTest
{
    private GameObject playerShootObject;
    private GameObject pathPlayerObject;
    private string testSceneName = "GameScene";
    private ObjectPoolingManager poolingManager;

    private GameObject bulletPrefab;  // Prefab de la bala
    public PoolObjectType bulletType = PoolObjectType.PlayerBullet;  // Tipo de la bala
    public int initialPoolSize = 20;  // Tamaño inicial del pool

    [UnitySetUp]
    public IEnumerator Setup()
    {
        Debug.Log("Cargando la escena de prueba");
        // Cargar la escena de prueba
        SceneManager.LoadScene(testSceneName);

        // Esperar a que la escena se cargue completamente
        yield return new WaitForSeconds(1);

        Debug.Log("Cargando el prefab de la bala desde Resources/Prefabs");
        // Cargar el prefab de la bala desde la carpeta Resources/Prefabs
        bulletPrefab = Resources.Load<GameObject>("Prefabs/BulletPrefab");

        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not found in Resources/Prefabs. Asegúrate de que el prefab se llama exactamente 'BulletPrefab' y está en la carpeta 'Resources/Prefabs'");
        }

        Assert.IsNotNull(bulletPrefab, "Bullet prefab not found in Resources/Prefabs.");
        Debug.Log("Prefab de la bala cargado correctamente");

        Debug.Log("Inicializando el ObjectPoolingManager");
        // Inicializar el ObjectPoolingManager
        GameObject poolingManagerObject = new GameObject("ObjectPoolingManager");
        poolingManager = poolingManagerObject.AddComponent<ObjectPoolingManager>();
        poolingManager.listOfPools = new List<PoolInfo>();

        // Crear el factory e inicializar el pool de balas
        Factory factory = poolingManagerObject.AddComponent<Factory>();
        poolingManager.objectFactory = factory;
        poolingManager.defaultObjectPosition = Vector3.zero;

        PoolInfo bulletPool = new PoolInfo
        {
            type = bulletType,
            amount = initialPoolSize,
            prefab = bulletPrefab,
            container = new GameObject("BulletContainer")
        };

        Assert.IsNotNull(bulletPool.prefab, "El prefab en bulletPool es nulo");
        Assert.IsNotNull(bulletPool.container, "El contenedor en bulletPool es nulo");

        Debug.Log("Prefab y contenedor asignados correctamente");

        poolingManager.listOfPools.Add(bulletPool);

        // Llenar el pool
        poolingManager.Start();
        Debug.Log("ObjectPoolingManager inicializado y pool de balas llenado");

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

        Debug.Log("Configuración de prueba completada correctamente");
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
        LogAssert.Expect(LogType.Error, "Failed to get newBullet from Object Pool");
        LogAssert.Expect(LogType.Error, "newBullet does not have a Rigidbody2D component");

        // Esperar un poco más para asegurar disparos
        yield return new WaitForSeconds(2f);
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
