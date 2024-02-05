using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected CapsuleCollider2D capsuleCollider;
    [SerializeField] protected Rigidbody2D enemyRb;
    [SerializeField] protected LayerMask hazardsLayer;
    [SerializeField] protected float maxHealth; // 3 hearts
    [SerializeField] protected float damageHit;

    protected float currentHealth; // 3 hearts
    protected bool isOnHazard = false;

    protected void OnEnable()
    {
        currentHealth = maxHealth;
    }

    protected void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyRb.bodyType = RigidbodyType2D.Dynamic;
    }

    protected bool IsTouchingHazard()
    {
        isOnHazard = true;
        return capsuleCollider.IsTouchingLayers(hazardsLayer);
    }
}
