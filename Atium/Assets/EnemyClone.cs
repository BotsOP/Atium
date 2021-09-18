using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClone : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private bool hasWaited;
    private Vector3 walkTo;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        EnemyWanderState.updateWalkTo += waitDestination;
        Invoke("Wait", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasWaited)
            anim.SetFloat("VelocityZ", 1);
    }

    private void Wait()
    {
        hasWaited = true;
    }

    private void waitDestination(Vector3 _walkTo)
    {
        walkTo = _walkTo;
        Invoke("SetNewDestination", 1f);
    }

    private void SetNewDestination()
    {
        agent.SetDestination(walkTo);
        Debug.Log(walkTo);
    }
}
