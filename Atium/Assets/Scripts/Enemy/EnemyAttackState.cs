using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float cooldownAfterAttack = 1.5f;
    
    private EnemyStateManager enemy;
    private Vector3 offset = new Vector3(0.1f, 0, 0.1f);
    private float timeAttack;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.anim.SetFloat("VelocityZ", 0);
        enemy.clone.StartCoroutine("SetVelocity", 0f);

        enemy.agent.SetDestination(enemy.transform.position);
        
        LookAtPlayer();
        
        enemy.anim.SetBool("Attacking", true);
        enemy.clone.StartCoroutine("SetAttack", true);

        timeAttack = Time.time;
        
        Debug.Log("ATTACKING" + Time.time);
    }

    public override void UpdateState()
    {
        if (timeAttack + cooldownAfterAttack < Time.time)
        {
            enemy.anim.SetBool("Attacking", false);
            enemy.clone.StartCoroutine("SetAttack", false);
            enemy.SwitchState(enemy.chaseState);
        }
    }

    private void LookAtPlayer()
    {
        Vector3 lookAt = new Vector3(enemy.chaseTarget.position.x, enemy.transform.position.y, enemy.chaseTarget.position.z);
        enemy.transform.LookAt(lookAt);
    }

}
