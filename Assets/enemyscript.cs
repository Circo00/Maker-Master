using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public string playerTag = "Player";
    public float moveSpeed = 5f;
    Animator animator;

    private Transform playerTransform;
    private Rigidbody rb;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude >= 0.1)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = playerTransform.position - transform.position;
            direction.Normalize();

            // Apply the movement force to the enemy using Rigidbody
            rb.velocity = direction * moveSpeed;
        }
    }
}