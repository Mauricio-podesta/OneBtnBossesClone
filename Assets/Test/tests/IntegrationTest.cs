using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class IntegrationTest
{
    private GameObject playerShootObject;
    private GameObject pathPlayerObject;
    private string testSceneName = "Level 1";
    private GameObject bulletPrefab;
    private GameObject spawnPositionObject;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        SceneManager.LoadScene(testSceneName);
        yield return new WaitForSeconds(1);

        bulletPrefab = Resources.Load<GameObject>("Prefabs/BulletPrefab");
        Assert.IsNotNull(bulletPrefab, "Bullet prefab not found in Resources/Prefabs.");

        playerShootObject = new GameObject("PlayerShoot");
        pathPlayerObject = new GameObject("PathPlayer");
        spawnPositionObject = new GameObject("SpawnPosition");

        PlayerShoot playerShoot = playerShootObject.AddComponent<PlayerShoot>();
        PlayerMovement playerMovement = playerShootObject.AddComponent<PlayerMovement>();

        Assert.IsNotNull(playerShoot, "PlayerShoot component is not added.");
        Assert.IsNotNull(playerMovement, "PlayerMovement component is not added.");

        playerShoot.enemy = pathPlayerObject;
        playerShoot.bulletType = PoolObjectType.PlayerBullet;
        playerShoot.spawnShootPosition = spawnPositionObject.transform;

        SoundManager soundManager = new GameObject("SoundManager").AddComponent<SoundManager>();

        pathPlayerObject.transform.position = new Vector3(1f, 1f, 0f);
        playerMovement.PathPlayer = pathPlayerObject.transform;
        playerMovement.PathPoints = new Transform[0];
    }

    [UnityTest]
    public IEnumerator PlayerMovementAndShootingTest()
    {
        yield return null;

        PlayerMovement playerMovement = playerShootObject.GetComponent<PlayerMovement>();
        CreateRandomPathPoints(playerMovement);

        Assert.IsTrue(playerMovement.PathPoints.Length > 0, "PathPoints were not created.");

        yield return new WaitForSeconds(2f);

        Vector3 initialPosition = playerShootObject.transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 newPosition = playerShootObject.transform.position;
        Assert.AreNotEqual(initialPosition, newPosition, "El jugador no se está moviendo.");

        PlayerShoot playerShoot = playerShootObject.GetComponent<PlayerShoot>();
        if (playerShoot != null && playerShoot.enemy != null && playerShoot.spawnShootPosition != null && SoundManager.Instance != null)
        {
            playerShoot.Shoot();
        }

        yield return new WaitForSeconds(2f);
        Assert.IsTrue(playerShootObject.activeInHierarchy, "El objeto del jugador no está activo.");
    }

    private void CreateRandomPathPoints(PlayerMovement playerMovement)
    {
        int cantidad = 10;
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
        Object.Destroy(playerShootObject);
        Object.Destroy(pathPlayerObject);
        Object.Destroy(spawnPositionObject);
    }
}



