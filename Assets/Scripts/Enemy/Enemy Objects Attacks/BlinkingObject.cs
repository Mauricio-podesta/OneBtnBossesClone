using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BlinkingObject : MonoBehaviour
{
    public float blinkDuration = 3f; 
    private Collider2D objectCollider;
    private SpriteShapeRenderer spriteRenderer;
    private bool isBlinking = true;

    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        objectCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteShapeRenderer>();
    }

    void OnEnable()
    {
        // Reiniciar el estado del objeto cuando se activa desde el pool
        objectCollider.enabled = false; 
        isBlinking = true; 
        StartCoroutine(Blink());
    }



    IEnumerator Blink()
    {
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < blinkDuration && isBlinking)
        {
            spriteRenderer.color = new Color(255, 155, 155, visible ? 255f : 0.5f);
            visible = !visible;
            yield return new WaitForSeconds(0.2f);
            elapsed += 0.2f;
        }
        
        spriteRenderer.color = new Color(255, 155, 155, 255); 
        isBlinking = false;

        objectCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage();
        }
    }


}
