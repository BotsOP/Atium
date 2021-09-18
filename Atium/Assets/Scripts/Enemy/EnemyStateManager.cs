using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateManager : MonoBehaviour
{
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyWanderState wanderState = new EnemyWanderState();
    public EnemyAttackState attackState = new EnemyAttackState();
    
    public NavMeshAgent agent;
    public Animator anim;

    public NavMeshAgent agentClone;
    public Animator animClone;

    public float timeBetweenClone;
    
    private EnemyBaseState currentState;
    
    void Start()
    {
        currentState = wanderState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
