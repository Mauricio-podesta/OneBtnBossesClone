using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TesteSceneSelectors
{
    private SceneSelector sceneSelector;

    [SetUp]
    public void Setup()
    {
        // Crear un GameObject y agregar el componente SceneSelector
        GameObject gameObject = new GameObject();
        sceneSelector = gameObject.AddComponent<SceneSelector>();
    }

    [Test]
    public void SelectScene_SetsSelectedScene()
    {
        // Act
        sceneSelector.SelectScene("TestScene");

        // Assert
        Assert.AreEqual("TestScene", typeof(SceneSelector).GetField("selectedScene", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null));
    }

    [Test]
    public void Restart_RestartsCurrentScene()
    {
        // Arrange
        string currentScene = SceneManager.GetActiveScene().name;

        // Act
        sceneSelector.Restart();

        // Assert
        Assert.AreEqual(currentScene, SceneManager.GetActiveScene().name);
        Assert.AreEqual(1f, Time.timeScale);
    }
}
