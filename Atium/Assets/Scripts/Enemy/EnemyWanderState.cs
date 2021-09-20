using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyWanderState : EnemyBaseState
{
    private Vector3 walkTo;
    private int frameCount;
    private int playermask;
    private EnemyStateManager enemy;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;

        // if(!hasSubcribed)
        //     EventSystem.Subscribe(EventType.FOUND_PLAYER, FoundTarget);
        
        SetNewDestination();

        enemy.anim.SetFloat("VelocityZ", 1);
        enemy.clone.StartCoroutine("SetVelocity", 1f);

        playermask = 1 << 8;
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(enemy.agent.transform.position, walkTo) < 0.5f)
        {
            SetNewDestination();
        }

        LookForPlayer();
    }

    private void SetNewDestination()
    {
        walkTo = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        enemy.agent.SetDestination(walkTo);
    }

    private void LookForPlayer()
    {
        RaycastHit hit;
        Vector3 centerBody = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1, enemy.transform.position.z);
        if (Physics.Raycast(centerBody, enemy.transform.TransformDirection(Vector3.forward),out hit, enemy.visionDistance, playermask))
        {
            FoundTarget(hit.transform);
            
            enemy.SwitchState(enemy.chaseState);
            Debug.Log("FOUND PLAYER");
        }
    }

    private void FoundTarget(Transform target)
    {
        if (enemy.chaseTarget == null)
        {
            enemy.chaseTarget = target;
        }
    }
}
