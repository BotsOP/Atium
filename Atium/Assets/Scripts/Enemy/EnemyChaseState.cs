using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private EnemyStateManager enemy;
    private int frameCount;
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.anim.SetFloat("VelocityZ", 2);
        enemy.clone.StartCoroutine("SetVelocity", 2f);
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.chaseTarget.position) < 3)
        {
            enemy.SwitchState(enemy.attackState);
        }

        if (frameCount % 20 == 0)
        {
            SetNewDestination();
            SetClonePos();
        }
        frameCount++;
    }
    
    private void SetNewDestination()
    {
        enemy.agent.SetDestination(enemy.chaseTarget.position);
    }
    
    private void SetClonePos()
    {
        enemy.clone.StartCoroutine("SetDestination", enemy.transform.position);
    }
}
