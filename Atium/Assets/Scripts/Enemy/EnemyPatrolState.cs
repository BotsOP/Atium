using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyPatrolState : EnemyBaseState
{
    private const float AGENT_WALK_SPEED = 2;
    private const float ANIM_WALK_SPEED = 1;
    
    private Vector3 walkTo;
    private int playermask;

    private float startTime;
    private int currentPatrolIndex;
    private bool walking;
    private bool waiting;
    private bool patrolForward;
    private bool firstInitialized;

    private EnemyStateManager enemy;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;

        if(!firstInitialized)
        {
            firstInitialized = true;
            currentPatrolIndex = enemy.startingPatrolIndex;
        }

        // if(!hasSubcribed)
        //     EventSystem.Subscribe(EventType.FOUND_PLAYER, FoundTarget);
        
        SetNewDestination();

        playermask = 1 << 8;
    }

    public override void UpdateState()
    {
        if (enemy.agent.remainingDistance < 0.3f)
        { 
            if(walking)
            {
                walking = false;

                if (enemy.waitAtPoints)
                {
                    waiting = true;
                    startTime = Time.time;
                }
                else
                {
                    ChangePatrolPoint();
                    SetNewDestination();
                }
            }

            SmoothWalkingTransition(0, 0);
            SmoothRotation();

        }
        else
        {
            SmoothWalkingTransition(ANIM_WALK_SPEED, AGENT_WALK_SPEED);
        }

        if (waiting && Time.time - startTime > enemy.waitingTime)
        {
            waiting = false;

            ChangePatrolPoint();
            SetNewDestination();
        }

        LookForPlayer();
    }

    private void ChangePatrolPoint()
    {
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber <= enemy.switchProbability)
        {
            patrolForward = !patrolForward;
        }

        if(patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % enemy.patrolsPoints.Length;
        }
        else
        {
            currentPatrolIndex--;
            if(currentPatrolIndex < 0)
            {
                currentPatrolIndex = enemy.patrolsPoints.Length - 1;
            }
        }
    }

    private void SetNewDestination()
    {
        walkTo = enemy.patrolsPoints[currentPatrolIndex].position;
        enemy.agent.SetDestination(walkTo);
        walking = true;
    }


    private void LookForPlayer()
    {
        //change this to fov radius
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

    private void SmoothWalkingTransition(float animSpeed, float agentSpeed)
    {
        float VelocityZ = enemy.anim.GetFloat("VelocityZ");
        if (VelocityZ != animSpeed)
        {
            VelocityZ = Mathf.Lerp(VelocityZ, animSpeed, (Time.time - startTime) / 7);
            enemy.anim.SetFloat("VelocityZ", VelocityZ);
            enemy.clone.StartCoroutine("SetVelocity", VelocityZ);
            enemy.agent.speed = Mathf.Lerp(enemy.agent.speed, agentSpeed, (Time.time - startTime) / 5);
        }
    }

    private void SmoothRotation()
    {
        float rotationSpeed = 15;
        Vector3 newAngle;
        newAngle.x = Mathf.LerpAngle(enemy.transform.eulerAngles.x, enemy.patrolsPoints[currentPatrolIndex].eulerAngles.x, (Time.time - startTime) / rotationSpeed);
        newAngle.y = Mathf.LerpAngle(enemy.transform.eulerAngles.y, enemy.patrolsPoints[currentPatrolIndex].eulerAngles.y, (Time.time - startTime) / rotationSpeed);
        newAngle.z = Mathf.LerpAngle(enemy.transform.eulerAngles.z, enemy.patrolsPoints[currentPatrolIndex].eulerAngles.z, (Time.time - startTime) / rotationSpeed);
        enemy.transform.eulerAngles = newAngle;
    }
}
