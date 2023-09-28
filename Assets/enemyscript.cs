using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class enemyscript : MonoBehaviour
{
    public string playerTag = "Player";
    public float speed = 1000f;
    public float movespeed = 5f;
    Animator animator;

    private Transform playerTransform;
    private Rigidbody rb;
    private Collider childcollider;
    public float maxhealth = 100f;
    private int currenthealth;

    public int attackdamage = 50;
    GameObject player;
    playermovement _playermovement;

    public float attackdelay = 0.5f;
    

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        childcollider = GetComponentInChildren<Collider>();
        currenthealth = (int)maxhealth;
        player = GameObject.FindGameObjectWithTag(playerTag);
        _playermovement = player.GetComponent<playermovement>();
    }

    private void Update()
    {
        if (animator.GetBool("isDying") == false)
        {
            MovementAnimationController();
            SpeedControl();
            Attack();
        }
        
        

        

    }

    private void FixedUpdate()
    {
        if(animator.GetBool("isDying") == false)
        {
            Movement();
        }
        
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

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damagepoint)
    {
        currenthealth -= damagepoint;
        Debug.Log("Damage Taken");
        if (currenthealth <= 0)
        {
            animator.SetBool("isDying", true);
            Invoke("Die", 5);
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

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag != "Ground" && animator.GetBool("isDying") == true)
        {
            Debug.Log("dhbajkndada");
            Physics.IgnoreCollision(collision.collider, childcollider, true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag != "Ground" && animator.GetBool("isDying") == true)
        {
            Debug.Log("dhbajkndada");
            Physics.IgnoreCollision(collision.collider, childcollider, true);
        }
    }

    private void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.3f))
        {
            

            playermovement player = hit.collider.GetComponentInParent<playermovement>();

            if (player != null && animator.GetBool("isAttacking") == false)
            {
                // Deal damage to the enemy
                animator.SetBool("isAttacking", true);
                Invoke("DamagePlayer", attackdelay);
                
                
                CameraShaker.Instance.ShakeOnce(.5f, 5f, .05f, .05f);
            }
        }
    }

    private void DamagePlayer()
    {
        _playermovement.TakeDamage(attackdamage);
        animator.SetBool("isAttacking", false);

    }

}