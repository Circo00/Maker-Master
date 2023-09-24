using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    public Joystick joystick;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float movespeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal_value = joystick.Horizontal;
        float vertical_value = joystick.Vertical;

        rb.AddForce(horizontal_value * speed * Time.deltaTime, 0, vertical_value * speed * Time.deltaTime);

        Vector3 inputDir = new Vector3(horizontal_value, 0, vertical_value);
        if (inputDir.magnitude > 0.2)
        {
            transform.forward = inputDir;
        }
        
        if(Input.touchCount > 1)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        SpeedControl();
        AnimationControl();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > movespeed)
        {
            Vector3 limitedVel = flatVel.normalized * movespeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void AnimationControl()
    {
        if (rb.velocity.magnitude > 0.2)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
