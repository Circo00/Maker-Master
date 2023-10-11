using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackNode : Node
{
    private Transform shootpos;
    private GameObject shootable;
    private Rigidbody shootablerb;
    private float shootableforce;
    private float shootingoffset;

    private bool done = false;

    private bool doneattack = false;

    //timer 2
    private float previoustime2 = 0;
    private float cooldowntime2 = 0.2f;

    public RangedAttackNode(Transform shootpos, GameObject shootable, Rigidbody shootablerb, float shootableforce, float shootingoffset)
    {
        this.shootpos = shootpos;
        this.shootable = shootable;
        this.shootablerb = shootablerb;
        this.shootableforce = shootableforce;
        this.shootingoffset = shootingoffset;

    }

    public override NodeState Evaluate()
    {
        if(done == true) { return NodeState.SUCCESS; }
        if (doneattack == true && Time.time - previoustime2 >= cooldowntime2) { done = true; return NodeState.SUCCESS; }

        if(doneattack == false)
        {
            RangedAttack();
            doneattack = true;
            return NodeState.FAILURE;
        }

        return NodeState.FAILURE;
    }

    private void RangedAttack()
    {
        GameObject spawnedshootable = GameObject.Instantiate(shootable, shootpos.position, shootpos.rotation);
        Rigidbody shootablerb = spawnedshootable.GetComponent<Rigidbody>();
        shootablerb.AddRelativeForce(Random.Range(shootingoffset, -shootingoffset), 0, shootableforce * Time.deltaTime, ForceMode.Impulse);
        previoustime2 = Time.time;

    }

    public override void ResetValues()
    {
        done = false;
        doneattack = false;
    }
}
