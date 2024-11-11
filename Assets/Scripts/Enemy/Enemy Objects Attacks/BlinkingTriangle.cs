using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BlinkingTriangle : MonoBehaviour
{
    public float blinkDuration = 3f; 
    private Collider2D triangleCollider;
    private SpriteShapeRenderer spriteRenderer;
    private bool isBlinking = true;

    void Start()
    {
        triangleCollider = GetComponent<Collider2D>();
        
        triangleCollider.enabled = false; 

        spriteRenderer = GetComponentInChildren<SpriteShapeRenderer>();

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

        triangleCollider.enabled = true;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(!isBlinking && collision.CompareTag("Player")) 
    //    {
    //        Debug.Log("Daño a player");
    //    }
    //}

}
