using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestButtons
{
    private GameObject currentCanvas;
    private GameObject targetCanvas;
    private SwitchCanvas switchCanvas;
    // A Test behaves as an ordinary method
    [SetUp]
    public void Setup()
    {
        // Crear objetos de prueba
        currentCanvas = new GameObject("CurrentCanvas");
        targetCanvas = new GameObject("TargetCanvas");

        // A�adir el componente SwitchCanvas al objeto de prueba
        var gameObject = new GameObject();
        switchCanvas = gameObject.AddComponent<SwitchCanvas>();

        // Asignar los objetos de prueba al script
        switchCanvas.currentCanvas = currentCanvas;
        switchCanvas.targetCanvas = targetCanvas;
    }

    [Test]
    public void CanvasSwitcher_SwitchesCanvases()
    {
        // Activar el canvas actual y desactivar el canvas objetivo
        currentCanvas.SetActive(true);
        targetCanvas.SetActive(false);

        // Llamar al m�todo que estamos probando
        switchCanvas.CanvasSwitcher();

        // Verificar que el canvas actual est� desactivado y el canvas objetivo est� activado
        Assert.IsFalse(currentCanvas.activeSelf);
        Assert.IsTrue(targetCanvas.activeSelf);
    }
}
