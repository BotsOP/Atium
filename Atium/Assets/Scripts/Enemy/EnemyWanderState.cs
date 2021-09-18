using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyWanderState : EnemyBaseState
{
    public static event Action<Vector3> updateWalkTo; 
    private Vector3 walkTo;
    public override void EnterState(EnemyStateManager enemy)
    {
        SetNewDestination(enemy);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (Vector3.Distance(enemy.agent.transform.position, walkTo) < 0.5f)
        {
            SetNewDestination(enemy);
        }
        enemy.anim.SetFloat("VelocityZ", 1);
        updateWalkTo?.Invoke(enemy.transform.position);
    }

    private void SetNewDestination(EnemyStateManager enemy)
    {
        walkTo = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        enemy.agent.SetDestination(walkTo);
        
    }
}
