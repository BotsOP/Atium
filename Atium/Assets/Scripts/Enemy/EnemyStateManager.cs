using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateManager : MonoBehaviour
{
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyWanderState wanderState = new EnemyWanderState();
    public EnemyAttackState attackState = new EnemyAttackState();
    
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Transform chaseTarget;
    
    public float distUntilAttack = 2;
    public EnemyClone clone;
    public float visionDistance = 5f;
    
    private EnemyBaseState currentState;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
        currentState = wanderState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState();

        //update clone
        clone.StartCoroutine("SetPosition", transform.position);
        clone.StartCoroutine("SetRotation", transform.rotation);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
