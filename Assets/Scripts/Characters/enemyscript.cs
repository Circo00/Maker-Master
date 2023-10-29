using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class enemyscript : MonoBehaviour
{
    [Header("Movement")]
    public string playerTag = "Player";
    public float speed = 1000f;
    public float movespeed = 5f;
    Animator animator;

    private Transform playerTransform;
    private Rigidbody rb;
    private Collider childcollider;
    [Space(10)]

    [Header("Attack")]
    public int attackdamage = 50;
    GameObject player;
    PlayerHealthScript _healthscript;
    public float attackrange = 0.3f;

    public float attackdelay = 0.5f;



    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        
        //currenthealth = (int)maxhealth;
        player = GameObject.FindGameObjectWithTag(playerTag);
        _healthscript = player.GetComponent<PlayerHealthScript>();
        
        //skinnedmeshrenderer = GetComponentInChildren<SkinnedMeshRenderer>();

    }

    private void Update()
    {
        
        MovementAnimationController();
        SpeedControl();
        Attack();

    }

    private void FixedUpdate()
    {
        
        Movement();

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

    

    private void Movement()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = playerTransform.position - transform.position;
            direction.Normalize();

            // Apply the movement force to the enemy using Rigidbody
            rb.AddForce(direction.x * speed * Time.deltaTime, 0, direction.z * speed * Time.deltaTime);
        }
    }

    private void MovementAnimationController()
    {
        Vector3 viewdirection = new Vector3(playerTransform.position.x - transform.position.x, 0, playerTransform.position.z - transform.position.z);
        if (rb.velocity.magnitude >= 0.1)
        {
            transform.forward = viewdirection;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }


    private void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackrange))
        {

            //Debug.DrawRay(transform.position, transform.forward, color:Color.blue);
            PlayerHealthScript playerhealth = hit.collider.GetComponentInParent<PlayerHealthScript>();

            if (playerhealth != null && animator.GetBool("isAttacking") == false)
            {
                // Deal damage to the enemy
                animator.SetBool("isAttacking", true);
                Invoke("DamagePlayer", attackdelay);
                
                
                
            }
        }
    }

    private void DamagePlayer()
    {
        _healthscript.TakeDamage(attackdamage);
        animator.SetBool("isAttacking", false);
        CameraShaker.Instance.ShakeOnce(5f, 5f, .5f, .5f);

    }

}