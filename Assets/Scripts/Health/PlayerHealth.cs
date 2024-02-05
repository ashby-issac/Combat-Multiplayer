using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private LayerMask heartsLayer;

    private PlayerAnimations playerAnimations;
    private InputManager inputManager;
    private bool hitObstacle = false;

    public static bool IsAlive = false;
    public static bool IsFull = true;

    public delegate void PlayerDamagedHandler(float currentHealth, float maxHealth);
    public static event PlayerDamagedHandler OnPlayerDamaged;

    new void OnEnable()
    {
        base.OnEnable();
        IsAlive = true;
    }

    new void Start()
    {
        base.Start();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    void Update()
    {
        if (!IsAlive)
            return;

        if (capsuleCollider.IsTouchingLayers(hazardsLayer) && !hitObstacle)
        {
            ReduceHealth(this);
            hitObstacle = true;
        }
        else if (!capsuleCollider.IsTouchingLayers(hazardsLayer))
        {
            hitObstacle = false;
        }
    }

    void AddHealth()
    {
        if (currentHealth < maxHealth)
            currentHealth++;

        IsFull = currentHealth == maxHealth;
    }

    private void ReduceHealth(PlayerHealth playerHealth)
    {
        if (playerHealth == this)
        {
            currentHealth--;
            IsFull = false;
            if (currentHealth < 1)
            {
                playerAnimations?.PlayDeathAnim(false);
                IsAlive = false;
                Invoke("DisableObj", 3f);
            }
            OnPlayerDamaged?.Invoke(currentHealth, maxHealth);
        }
    }

    void PlayerReset()
    {
        gameObject.SetActive(true);
    }

    void DisableObj() => gameObject.SetActive(false);
}
