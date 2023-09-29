using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

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

    public float attackdelay = 2;



    //healthbar************************************************************************************************

    public float maxhealth = 100f;
    private int currenthealth;
    [SerializeField] private Healthbar healthbar;

    //healthbar************************************************************************************************


    //characterflash*******************************************************************************************

    SkinnedMeshRenderer skinnedmeshrenderer;
    public float blinkintensity;
    public float blinkduration;
    float blinktimer;

    //characterflash*******************************************************************************************


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        currenthealth = (int)maxhealth;
        healthbar.UpdateHealthbar(maxhealth, currenthealth);

        skinnedmeshrenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currenthealth);
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
            Invoke("Attack", attackdelay);


        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        

        SpeedControl();
        AnimationControl();

        blinktimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinktimer / blinkduration);
        float intensity = (lerp * blinkintensity) + 1.0f;
        skinnedmeshrenderer.material.color = Color.white * intensity;
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
        //Debug.Log("Attack!");
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
        CameraShaker.Instance.ShakeOnce(.5f, 5f, .1f, .1f);
    }

    public void TakeDamage(int damagepoint)
    {
        currenthealth -= damagepoint;
        healthbar.UpdateHealthbar(maxhealth, currenthealth);

        blinktimer = blinkduration;

        Debug.Log("Damage Taken");
        if (currenthealth <= 0)
        {
            animator.SetBool("isDying", true);
            Invoke("Die", 2);
        }
    }

    private void Die()
    {
        rb.AddForce(0, 1, 0);
    }

    

}
