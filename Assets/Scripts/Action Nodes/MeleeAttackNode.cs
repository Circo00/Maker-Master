using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackNode : Node
{
    private Animator animator;
    private Transform _transform;
    private float attackrange;
    private int attackdamage;
    private float spreadangle = 30f;
    private int numrays = 5;

    private float previoustime = 0;
    private float cooldowntime;

    private bool waitingtoattack = false;
    private bool done = false;

    public MeleeAttackNode(Animator animator, Transform transform, float attackrange, int attackdamage, float spreadangle, int numrays, float cooldowntime)
    {
        this.animator = animator;
        this._transform = transform;
        this.attackrange = attackrange;
        this.attackdamage = attackdamage;
        this.spreadangle = spreadangle;
        this.numrays = numrays;
        this.cooldowntime = cooldowntime;

    }

    public override NodeState Evaluate()
    {
        if (done == true) { return NodeState.SUCCESS; }

        if (!waitingtoattack)
        {
            animator.SetBool("isAttacking", true);
            previoustime = Time.time;
            waitingtoattack = true;
            return NodeState.FAILURE;
        }
        else if (waitingtoattack && Time.time - previoustime >= cooldowntime)
        {
            Attack();
            waitingtoattack = false;
            done = true;
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }

    }

    private void Attack()
    {
        float angleStep = spreadangle / (numrays - 1);
        for (int i = 0; i < numrays; i++)
        {
            float currentAngle = -spreadangle / 2f + (angleStep * i);
            Quaternion rayRotation = Quaternion.Euler(0f, currentAngle, 0f);
            Vector3 rayDirection = rayRotation * _transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(_transform.position, rayDirection, out hit, attackrange))
            {
                EnemyHealthScript enemyhealth = hit.collider.GetComponentInParent<EnemyHealthScript>();
                if (enemyhealth != null)
                {
                    enemyhealth.TakeDamage(attackdamage);
                }
            }
            Debug.DrawRay(_transform.position, rayDirection * attackrange, Color.red, 0.1f);
        }

        animator.SetBool("isAttacking", false);

    }

    public override void ResetValues()
    {
        done = false;
    }
}
