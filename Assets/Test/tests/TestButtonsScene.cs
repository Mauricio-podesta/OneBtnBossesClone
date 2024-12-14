using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestButtonsScene
{
    private Buttons buttons;

    [SetUp]
    public void Setup2()
    {
        // Crear un objeto de prueba y añadir el componente Buttons
        var gameObject = new GameObject();
        buttons = gameObject.AddComponent<Buttons>();
    }

    [UnityTest]
    public System.Collections.IEnumerator SceneMenu_LoadsMainMenu()
    {
        // Cargar la escena "Main Menu"
        var asyncLoad = SceneManager.LoadSceneAsync("Main Menu");
        while (!asyncLoad.isDone)
        {
            yield return null; // Espera a que la escena termine de cargar
        }

        // Verificar que la escena activa es "Main Menu"
        Assert.AreEqual("Main Menu", SceneManager.GetActiveScene().name);
    }


    [UnityTest]
    public System.Collections.IEnumerator SceneGame_LoadsGameScene()
    {
        // Llama al método SceneGame()
        buttons.LevelOne();

        // Espera a que la escena "GameScene" cargue
        var asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncLoad.isDone)
        {
            yield return null; // Espera hasta que la escena termine de cargar
        }

        // Verifica que la escena activa es "GameScene"
        Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public System.Collections.IEnumerator ScenePlay1_LoadsLogin()
    {
        // Llama al método ScenePlay1()
        buttons.Play();

        // Espera a que la escena "Login" cargue
        var asyncLoad = SceneManager.LoadSceneAsync("Login");
        while (!asyncLoad.isDone)
        {
            yield return null; // Espera hasta que la escena termine de cargar
        }

        // Verifica que la escena activa es "Login"
        Assert.AreEqual("Login", SceneManager.GetActiveScene().name);
    }

}
