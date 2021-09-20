using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClone : MonoBehaviour
{
    [SerializeField] private float timeBetweenActions = 1f;
    private float delay;
    private float startTime;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        delay = 0;
        startTime = Time.time;
    }

    private void Update()
    {
        if(delay < timeBetweenActions)
        {
            delay = Mathf.Lerp(delay, timeBetweenActions, (Time.time - startTime) / 30);
        }
    }

    public IEnumerator SetVelocity(float velocity)
    {
        yield return new WaitForSeconds(delay);
        anim.SetFloat("VelocityZ", velocity);
    }
    
    public IEnumerator SetAttack(bool value)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("Attacking", value);
    }

    public IEnumerator SetRotation(Quaternion rotation)
    {
        yield return new WaitForSeconds(delay);
        transform.rotation = rotation;
    }

    public IEnumerator SetPosition(Vector3 position)
    {
        yield return new WaitForSeconds(delay);
        transform.position = position;
    }
}
