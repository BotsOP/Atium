using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : Sword
{
    [SerializeField] private float damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (attacking && other.GetComponent<IDamagable>() != null)
        {
            Debug.Log("HIT SOMEHTING");
            other.GetComponent<IDamagable>().TakeDamage(damage);
        }
    }
}
