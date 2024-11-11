using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BlinkingTriangle : MonoBehaviour
{
    public float blinkDuration = 3f; // Tiempo que el triángulo titila antes de activar la colisión
    private Collider2D triangleCollider;
    private SpriteShapeRenderer spriteRenderer;
    private bool isBlinking = true;

    void Start()
    {
        // Obtener el componente Collider2D y desactivarlo inicialmente
        triangleCollider = GetComponent<Collider2D>();
        
        triangleCollider.enabled = false; // Desactivar colisión inicialmente


        // Obtener el SpriteRenderer para controlar el titileo
        spriteRenderer = GetComponentInChildren<SpriteShapeRenderer>();

        StartCoroutine(Blink());
        
    }

    IEnumerator Blink()
    {
        float elapsed = 0f;
        bool visible = true;
        while (elapsed < blinkDuration && isBlinking)
        {
            spriteRenderer.color = new Color(1, 1, 1, visible ? 1f : 0.5f);
            visible = !visible; 
        }
        yield return new WaitForSeconds(0.2f); // Intervalo de titileo
        elapsed += 0.2f;

        spriteRenderer.color = new Color(1, 1, 1, 1); 
        isBlinking = false;

        triangleCollider.enabled = true;
    }

}
