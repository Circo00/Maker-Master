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

    public float attackrange;
    public int attackdamage;
    public float spreadangle = 30f;
    public int numrays = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.forward * attackrange, Color.red);
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
            Attack();
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

    private void Attack()
    {
        // Calculate the angle between each ray
        float angleStep = spreadangle / (numrays - 1);

        // Shoot multiple rays in different directions
        for (int i = 0; i < numrays; i++)
        {
            // Calculate the direction based on the current angle
            float currentAngle = -spreadangle / 2f + (angleStep * i);
            Quaternion rayRotation = Quaternion.Euler(0f, currentAngle, 0f);
            Vector3 rayDirection = rayRotation * transform.forward;

            // Perform a raycast in the current direction to detect enemies
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit, attackrange))
            {
                // Check if the raycast hit an enemy
                
                enemyscript enemy = hit.collider.GetComponentInParent<enemyscript>();
                
                if (enemy != null)
                {
                    // Deal damage to the enemy
                    
                    enemy.TakeDamage(attackdamage);
                }
            }

            // Draw the ray in the scene view for debugging purposes
            Debug.DrawRay(transform.position, rayDirection * attackrange, Color.red, 0.1f);
        }
    }
}
