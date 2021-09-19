using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClone : MonoBehaviour
{
    [SerializeField] private float timeBetweenActions = 1f;
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
        yield return new WaitForSeconds(timeBetweenActions * 2);
        agent.SetDestination(walkTo);
    }

    public IEnumerator SetVelocity(float velocity)
    {
        yield return new WaitForSeconds(timeBetweenActions);
        anim.SetFloat("VelocityZ", velocity);
    }
    
    public IEnumerator SetAttack(bool value)
    {
        yield return new WaitForSeconds(timeBetweenActions);
        anim.SetBool("Attacking", value);
    }

    public IEnumerator LookAtTarget(Vector3 lookAt)
    {
        yield return new WaitForSeconds(timeBetweenActions);
        transform.LookAt(lookAt);
    }
}
