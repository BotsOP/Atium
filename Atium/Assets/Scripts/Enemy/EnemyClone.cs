using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClone : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public IEnumerator SetDestination(Vector3 walkTo)
    {
        yield return new WaitForSeconds(1f);
        agent.SetDestination(walkTo);
    }

    public IEnumerator SetVelocity(float velocity)
    {
        yield return new WaitForSeconds(1f);
        anim.SetFloat("VelocityZ", velocity);
    }
}
