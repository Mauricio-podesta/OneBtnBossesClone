using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BlinkingObject : MonoBehaviour
{
    public float blinkDuration = 3f; 
    private Collider2D ObjectCollider;
    private SpriteShapeRenderer spriteRenderer;
    private bool isBlinking = true;

    void Start()
    {
        ObjectCollider = GetComponent<Collider2D>();
        
        ObjectCollider.enabled = false; 

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

        ObjectCollider.enabled = true;
    }

   

}
