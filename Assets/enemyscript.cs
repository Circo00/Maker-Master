using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public string playerTag = "Player";
    public float speed = 1000f;
    public float movespeed = 5f;
    Animator animator;

    private Transform playerTransform;
    private Rigidbody rb;
    public int maxhealth = 100;
    private int currenthealth;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        currenthealth = maxhealth;
    }

    private void Update()
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
        
        SpeedControl();

        

    }

    private void FixedUpdate()
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
            Die();
        }
    }

}