using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float MaxHealth;

    [SerializeField]
    private UnityEvent OnDie;

    [SerializeField]
    private UnityEvent<float, float, LayerMask> OnDamageDecrease;

    [SerializeField]
    private LayerMask player;
    private float CurrentHealth { get; set; }
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void SetHealth(float damage)
    {
        CurrentHealth -= damage;

        OnDamageDecrease.Invoke(CurrentHealth, MaxHealth, player);

        if(CurrentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie?.Invoke();
    }
}
