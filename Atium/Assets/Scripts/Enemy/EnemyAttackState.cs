using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float cooldownAfterAttack = 1.5f;
    
    private EnemyStateManager enemy;
    private float timeAttack;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        
        enemy.agent.ResetPath();
        enemy.agent.isStopped = true;
        enemy.agent.updateRotation = false;
        enemy.agent.updatePosition = false;

        enemy.anim.SetFloat("VelocityZ", 0);
        enemy.clone.StartCoroutine("SetVelocity", 0f);

        enemy.agent.SetDestination(enemy.transform.position);
        
        LookAtPlayer();
        
        enemy.anim.SetBool("Attacking", true);
        enemy.clone.StartCoroutine("SetAttack", true);

        timeAttack = Time.time;
    }

    public override void UpdateState()
    {
        if (timeAttack + cooldownAfterAttack < Time.time)
        {
            enemy.anim.SetBool("Attacking", false);
            enemy.clone.StartCoroutine("SetAttack", false);
            
            enemy.agent.isStopped = false;
            enemy.agent.updateRotation = true;
            enemy.agent.updatePosition = true;

            enemy.SwitchState(enemy.chaseState);
        }
    }

    private void LookAtPlayer()
    {
        Vector3 lookAt = new Vector3(enemy.chaseTarget.position.x, enemy.transform.position.y, enemy.chaseTarget.position.z);
        enemy.transform.LookAt(lookAt);
    }

}
