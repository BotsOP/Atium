using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private float health = 10;
    [SerializeField] private UnityEvent isDead;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead?.Invoke();
            anim.SetBool("Dying", true);
        }
    }
    
    public void IsDead()
    {
        Destroy(gameObject, 1f);
    }
}
