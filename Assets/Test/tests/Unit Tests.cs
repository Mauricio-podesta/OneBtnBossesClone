using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitTests : MonoBehaviour
{
    private SceneSelector sceneSelector;
    private GameObject currentCanvas;
    private GameObject targetCanvas;
    private SwitchCanvas switchCanvas;
    
    [SetUp]
    public void Setup()
    {
        GameObject gameObject = new GameObject();
        sceneSelector = gameObject.AddComponent<SceneSelector>();
       
        currentCanvas = new GameObject("CurrentCanvas");
        targetCanvas = new GameObject("TargetCanvas");
       
        switchCanvas = gameObject.AddComponent<SwitchCanvas>();

        switchCanvas.currentCanvas = currentCanvas;
        switchCanvas.targetCanvas = targetCanvas;

    }
    [Test]
    public void SelectScene_SetsSelectedScene()
    {
        sceneSelector.SelectScene("TestScene");

        Assert.AreEqual("TestScene", typeof(SceneSelector).GetField("selectedScene", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null));
    }
    [Test]
    public void Restart_RestartsCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        sceneSelector.Restart();

        Assert.AreEqual(currentScene, SceneManager.GetActiveScene().name);
        Assert.AreEqual(1f, Time.timeScale);
    }
    [Test]
    public void CanvasSwitcher_SwitchesCanvases()
    {
        currentCanvas.SetActive(true);
        targetCanvas.SetActive(false);

        switchCanvas.CanvasSwitcher();

        Assert.IsFalse(currentCanvas.activeSelf);
        Assert.IsTrue(targetCanvas.activeSelf);
    }
}
