using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateManager : MonoBehaviour
{
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyPatrolState wanderState = new EnemyPatrolState();
    public EnemyAttackState attackState = new EnemyAttackState();
    
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Transform chaseTarget;
    
    public float distUntilAttack = 2;
    public float visionDistance = 5f;
    public float switchProbability = 0.2f;
    public bool waitAtPoints = true;
    public float waitingTime = 5;
    public int startingPatrolIndex;
    public Transform[] patrolsPoints; 
    public EnemyClone clone;
    
    private EnemyBaseState currentState;
    private bool isAlive = true;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.updatePosition = true;
        agent.updateRotation = true;

        currentState = wanderState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState();

        //update clone
        if (isAlive)
        {
            clone.StartCoroutine("SetPosition", transform.position);
            clone.StartCoroutine("SetRotation", transform.rotation);
        }
    }

    public void enemyDied()
    {
        isAlive = false;
        Destroy(gameObject, 2);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
