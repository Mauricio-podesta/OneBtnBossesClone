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

        Vida EnemyHealt = GameObject.FindObjectOfType<Vida>();
        float Hp = EnemyHealt.Hp;
        playerProjectile.transform.position = EnemyHealt.transform.position;
        playerProjectile.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        Assert.That(Hp > EnemyHealt.Hp);
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
        float Hp = playerHealth.Hp;
        EnemyTriangle.transform.position = playerHealth.transform.position;
        EnemyTriangle.SetActive(true);

        yield return new WaitForSeconds(7f);

        Assert.That(Hp > playerHealth.Hp);
    }
    //revisar
    [UnityTest]
    public IEnumerator DamagePlayerOnEnemyObstacle()
    {
        SceneManager.LoadScene("Scenes/Level 2");

        yield return new WaitForSeconds(0.5f);

        GameObject enemySquare = ObjectPoolingManager.Instance.GetPooledObject(PoolObjectType.Square);

        GameObject player = GameObject.FindWithTag("Player");

        PlayerMovement movementSpeed = GameObject.FindObjectOfType<PlayerMovement>();
        movementSpeed.movementSpeed = 0;

        player.SetActive(true);

        
        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        float hp = playerHealth.Hp;

        enemySquare.transform.position = playerHealth.transform.position;

        yield return new WaitForSeconds(4f);

        Assert.That(hp > playerHealth.Hp);
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
        float hp = playerHealth.Hp;

        enemyProjectile.transform.position = playerHealth.transform.position;
        enemyProjectile.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        Assert.That(hp > playerHealth.Hp);
    }

    [UnityTest]
    public IEnumerator TestVcitoryOnEnemyDeath()
   {
       SceneManager.LoadScene("Scenes/Level 1");

       yield return new WaitForSeconds(0.5f);

       GameObject player = GameObject.FindWithTag("Player");
       player.SetActive(true);

       GameObject enemy = GameObject.FindWithTag("Enemy");
       enemy.SetActive(true);

       Vida enemyLife = GameObject.FindObjectOfType<Vida>();

       if (enemyLife != null)
           enemyLife.TakeDamage(20);

       Assert.AreEqual(0, enemyLife.Hp);
       Assert.AreEqual(0, Time.timeScale);

       yield return new WaitForSecondsRealtime(0.5f);
   }


}



