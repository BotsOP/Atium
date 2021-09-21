using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClone : MonoBehaviour
{
    public float timeBetweenActions = 1f;
    public bool newOrders;
    [HideInInspector] public float delay;
    private float startTime;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator SetVelocity(float velocity)
    {
        yield return new WaitForSeconds(delay);
        anim.SetFloat("VelocityZ", velocity);
    }
    
    public IEnumerator SetAttack(bool value)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("Attacking", value);
    }

    public IEnumerator SetRotation(Quaternion rotation)
    {
        newOrders = false;
        yield return new WaitForSeconds(delay);
        if(!newOrders)
            transform.rotation = rotation;
        newOrders = true;
    }

    public IEnumerator SetPosition(Vector3 position)
    {
        yield return new WaitForSeconds(delay);
        if(!newOrders)
            transform.position = position;
    }
}
