using System.Collections;
using NUnit.Framework;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class IntegrationTest : InputTestFixture
{

    [UnityTest]
    public IEnumerator TestEnemyTakeDamageOnPlayerProjectileCollision()
    {
        SceneManager.LoadScene("Scenes/Level 1");

        yield return new WaitForSeconds(0.5f);

        GameObject playerProjectile = ObjectPoolingManager.Instance.GetPooledObject(PoolObjectType.PlayerBullet);
        GameObject Enemy = GameObject.FindWithTag("Enemy");
        Enemy.SetActive(true);

        EnemyLife EnemyHealt = GameObject.FindObjectOfType<EnemyLife>();
        float Hp = EnemyHealt.enemyLife;
        playerProjectile.transform.position = EnemyHealt.transform.position;
        playerProjectile.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        Assert.That(Hp > EnemyHealt.enemyLife);
    }

    [UnityTest]
    public IEnumerator TestTriangleAttackOnPlayerCollision()
    {
        SceneManager.LoadScene("Scenes/Level 1");

        yield return new WaitForSeconds(0.5f);

        GameObject EnemyTriangle = ObjectPoolingManager.Instance.GetPooledObject(PoolObjectType.Triangle);
        GameObject Player = GameObject.FindWithTag("Player");
        Player.SetActive(true);

        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        float Hp = playerHealth.PlayerLife;
        EnemyTriangle.transform.position = playerHealth.transform.position;
        EnemyTriangle.SetActive(true);

        yield return new WaitForSeconds(7f);

        Assert.That(Hp > playerHealth.PlayerLife);
    }
    //revisar
    [UnityTest]
    public IEnumerator TestDefeatOnPlayerDeath()
    {
        SceneManager.LoadScene("Scenes/Level 2");

        yield return new WaitForSeconds(0.5f);

        GameObject player = GameObject.FindWithTag("Player");
        player.SetActive(true);

        GameObject enemy = GameObject.FindWithTag("Enemy");
        enemy.SetActive(true);

        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
      

        if (playerHealth != null)
            playerHealth.TakeDamage(3);

        Assert.AreEqual(0, playerHealth.PlayerLife);
        Assert.AreEqual(0, Time.timeScale);

        yield return new WaitForSecondsRealtime(0.5f);
    }

    [UnityTest]
    public IEnumerator PlayerDamageOnEnemyBullet()
    {
        SceneManager.LoadScene("Scenes/Level 3");

        yield return new WaitForSeconds(0.5f);

        GameObject player = GameObject.FindWithTag("Player");
        player.SetActive(true);

        GameObject enemyProjectile = ObjectPoolingManager.Instance.GetPooledObject(PoolObjectType.EnemyBullet);


        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        float hp = playerHealth.PlayerLife;

        enemyProjectile.transform.position = playerHealth.transform.position;
        enemyProjectile.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        Assert.That(hp > playerHealth.PlayerLife);
    }

    [UnityTest]
    public IEnumerator TestVictoryOnEnemyDeath()
   {
       SceneManager.LoadScene("Scenes/Level 1");

       yield return new WaitForSeconds(0.5f);

       GameObject player = GameObject.FindWithTag("Player");
       player.SetActive(true);

       GameObject enemy = GameObject.FindWithTag("Enemy");
       enemy.SetActive(true);

       EnemyLife enemyLife = GameObject.FindObjectOfType<EnemyLife>();

       if (enemyLife != null)
           enemyLife.TakeDamage(20);

       Assert.AreEqual(0, enemyLife.enemyLife);
       Assert.AreEqual(0, Time.timeScale);

       yield return new WaitForSecondsRealtime(0.5f);
   }


}



