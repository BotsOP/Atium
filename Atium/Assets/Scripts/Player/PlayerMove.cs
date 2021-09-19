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
            anim.SetBool("Attacking", true);
        }
        
        // anim.SetBool("Dodging", false);
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     anim.SetFloat("DodgeZ", 0);
        //     anim.SetFloat("DodgeX", 0);
        //     if (vertical != 0)
        //     {
        //         anim.SetFloat("DodgeZ", vertical);
        //     }
        //     if (horizontal != 0)
        //     {
        //         anim.SetFloat("DodgeX", horizontal);
        //     }
        //     anim.SetBool("Dodging", true);
        // }

        if (horizontal != 0 || vertical != 0)
        {
            float targetAngle = cam.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        }
    }

    public void DoneAttacking()
    {
        Debug.Log("done attacking");
        anim.SetBool("Attacking", false);
    }
}
