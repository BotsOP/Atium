using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator anim;
    public Transform cam;
    public float turnSpeed = 0.1f;
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        anim.SetFloat("VelocityX", horizontal * 2);
        anim.SetFloat("VelocityZ", vertical * 2);

        if (Input.GetMouseButtonDown(0))
        {
            EventSystem.RaiseEvent(EventType.PLAYER_ATTACK);
            anim.SetBool("Attacking", true);
        }

        if (horizontal != 0 || vertical != 0)
        {
            float targetAngle = cam.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        }
    }

    public void DoneAttacking()
    {
        EventSystem.RaiseEvent(EventType.PLAYER_DONE_ATTACK);
        anim.SetBool("Attacking", false);
    }
}
