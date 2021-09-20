using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private EnemyStateManager enemy;
    private int frameCount;
    private float startTime;
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.clone.StartCoroutine("SetVelocity", 2f);
        startTime = Time.time;
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.chaseTarget.position) < enemy.distUntilAttack)
        {
            enemy.SwitchState(enemy.attackState);
        }

        float VelocityZ = enemy.anim.GetFloat("VelocityZ");
        if (VelocityZ != 2)
        {
            enemy.anim.SetFloat("VelocityZ", Mathf.Lerp(VelocityZ, 2, (Time.time - startTime) / 10));
        }

        if (frameCount % 20 == 0)
        {
            SetNewDestination();
        }
        frameCount++;
    }
    
    private void SetNewDestination()
    {
        enemy.agent.SetDestination(enemy.chaseTarget.position);
    }

}
