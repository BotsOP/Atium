using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 1;
    [SerializeField] private float dashLength = 1;
    [SerializeField] private float dashCooldown = 1;
    private bool isDashing;
    private Animator anim;
    private float horizontal;
    private float vertical;
    private Vector3 dashDirection;
    
    private float lastDash;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        anim.SetBool("Dodging", false);
        
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && Time.time > lastDash + dashCooldown)
        {
            StartCoroutine("Dash");
            lastDash = Time.time;
        }

        if (isDashing)
        {
            transform.Translate(dashDirection * Time.deltaTime * dashSpeed);
        }
    }

    private IEnumerator Dash()
    {
        anim.SetFloat("DodgeZ", 0);
        anim.SetFloat("DodgeX", 0);
        
        
        if (horizontal != 0)
        {
            anim.SetFloat("DodgeX", horizontal);
            if (horizontal > 0)
            {
                anim.SetFloat("DodgeX", 1);
                dashDirection = Vector3.right;
            }
            else
            {
                anim.SetFloat("DodgeX", -1);
                dashDirection = Vector3.left;
            }
            anim.SetBool("Dodging", true);
        }
        else if (vertical != 0)
        {
            if (vertical > 0)
            {
                anim.SetFloat("DodgeZ", 1);
                dashDirection = Vector3.forward;
            }
            else
            {
                anim.SetFloat("DodgeZ", -1);
                dashDirection = Vector3.back;
            }
            anim.SetBool("Dodging", true);
        }
        
        isDashing = true;
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
    }
}
