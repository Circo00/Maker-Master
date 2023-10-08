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
        RangedAttack();
        return NodeState.SUCCESS;
        
    }

    private void RangedAttack()
    {
        GameObject spawnedshootable = Instantiate(shootable, shootpos.position, shootpos.rotation);
        Rigidbody shootablerb = spawnedshootable.GetComponent<Rigidbody>();
        shootablerb.AddRelativeForce(Random.Range(shootingoffset, -shootingoffset), 0, shootableforce * Time.deltaTime, ForceMode.Impulse);

    }

    public override void ResetValues()
    {

    }
}
