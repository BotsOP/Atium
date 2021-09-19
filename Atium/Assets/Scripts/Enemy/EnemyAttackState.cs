using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private EnemyStateManager enemy;
    
    private Vector3 offset = new Vector3(0.1f, 0, 0.1f);
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.anim.SetFloat("VelocityZ", 0);
        enemy.clone.StartCoroutine("SetVelocity", 0f);
        
        enemy.agent.SetDestination(enemy.transform.position);
        enemy.clone.StartCoroutine("SetDestination", enemy.transform.position + offset);
        
        Debug.Log("ATTACKING");
    }

    public override void UpdateState()
    {
        
    }
}
