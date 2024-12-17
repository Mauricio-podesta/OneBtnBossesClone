using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PowerUp : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float charge = 0;
    [SerializeField] private float speedincrease = 2f;
    [SerializeField] private float MaxVelocity = 15f;
    [SerializeField] private float Timecharge;
    [SerializeField] private float TimeDischarge;

    [Header("Referencias")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private Slider Powerupslide;

    public static bool canactivate = false;
    private bool isCooldown = false;
    private float normalvelocity;
    private Collider2D playerCollider;

    void Start()
    {
        UpdateHealthUI();
        normalvelocity = playerMovement.movementSpeed;
        playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();

        if (playerCollider == null)
        {
            Debug.LogWarning("Collider2D not found on object with label 'Player'");
        }
    }
    void Update()
    {
        if (canactivate && !isCooldown && charge > 0)
        {
            Discharge();
        }
        else if(!canactivate && !isCooldown)
        {
            charger();
        }
    }
    void Discharge()
    {
        charge -= (100 / TimeDischarge) * Time.deltaTime;
        charge = Mathf.Clamp(charge, 0f, 100f);
        UpdateHealthUI();

        if (charge <= 0)
        {
            canactivate = false;
            StartCoroutine(Cooldown());
        }

        playerCollider.enabled = false;
        playerMovement.movementSpeed = Mathf.Clamp(playerMovement.movementSpeed + speedincrease, 0f, MaxVelocity);
    }
    void charger()
    {
        charge += (100 / Timecharge) * Time.deltaTime;
        charge = Mathf.Clamp(charge, 0f, 100f);
        UpdateHealthUI();

        playerMovement.movementSpeed = normalvelocity;
        playerCollider.enabled = true;
    }
    void UpdateHealthUI()
    {
        Powerupslide.value = charge;
    }
    public void OnPowerUp(InputAction.CallbackContext context)
    {
        Debug.Log("OnPowerUp called: " + context.phase);

        if (context.performed && !isCooldown && charge > 0)
        {
            canactivate = true;
        }
        else if (context.canceled)
        {
            canactivate = false;
        }
    }
    private IEnumerator Cooldown() 
    {
        isCooldown = true; 
        
        yield return new WaitForSeconds(1f); 
    
        isCooldown = false; 
    }
}

