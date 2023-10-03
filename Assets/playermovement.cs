using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class playermovement : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;

    [Header("Controller")]
    public Joystick joystick;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float movespeed = 10f;
    [Space(10)]

    [Header("Attacking")]
    public float attackrange;
    public int attackdamage;
    public float spreadangle = 30f;
    public int numrays = 5;
    public float attackdelay = 2;
    

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

    public void AttackRequest()
    {
        animator.SetBool("isAttacking", true);
        Invoke("Attack", attackdelay);
    }

    private void Attack()
    {
        //Deal damage to the enemy
        // Calculate the angle between each ray
        float angleStep = spreadangle / (numrays - 1);
        // Shoot multiple rays in different directions
        Debug.Log("Attack");
        //CameraShaker.Instance.ShakeOnce(1f, 1f, 1f, 1f);
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
                EnemyHealthScript enemyhealth = hit.collider.GetComponentInParent<EnemyHealthScript>();
                if (enemyhealth != null)
                {
                    // Deal damage to the enemy
                    
                    enemyhealth.TakeDamage(attackdamage);
                }
            }
            // Draw the ray in the scene view for debugging purposes
            Debug.DrawRay(transform.position, rayDirection * attackrange, Color.red, 0.1f);
        }
        CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, .1f);
        animator.SetBool("isAttacking", false);

    }

    

    

}
