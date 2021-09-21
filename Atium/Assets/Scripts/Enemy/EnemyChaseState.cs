using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private const float AGENT_RUN_SPEED = 4;
    private const float ANIM_RUN_SPEED = 2;
    
    private EnemyStateManager enemy;
    private int frameCount;
    private float startTime;
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.clone.StartCoroutine("SetVelocity", ANIM_RUN_SPEED);
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
            enemy.anim.SetFloat("VelocityZ", Mathf.Lerp(VelocityZ, ANIM_RUN_SPEED, (Time.time - startTime) / 10));
            enemy.agent.speed = Mathf.Lerp(enemy.agent.speed, AGENT_RUN_SPEED, (Time.time - startTime) / 5);
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
