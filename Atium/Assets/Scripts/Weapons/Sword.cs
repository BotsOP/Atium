using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    protected bool attacking;
    protected virtual void OnEnable()
    {
        EventSystem.Subscribe(EventType.PLAYER_ATTACK, Attacking);
        EventSystem.Subscribe(EventType.PLAYER_DONE_ATTACK, DoneAttacking);
    }
    
    protected virtual void OnDisable()
    {
        EventSystem.Unsubscribe(EventType.PLAYER_ATTACK, Attacking);
        EventSystem.Unsubscribe(EventType.PLAYER_DONE_ATTACK, DoneAttacking);
    }

    protected virtual void Attacking()
    {
        attacking = true;
    }

    protected virtual void DoneAttacking()
    {
        attacking = false;
    }
}
