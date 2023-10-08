using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTemplate : Node
{
    private Transform _transform;
    private Transform target;

    public NodeTemplate(Transform transform, Transform target)
    {
        this._transform = transform;
        this.target = target;
        
    }

    public override NodeState Evaluate()
    {

        if (Vector3.Distance(transform.position, target.transform.position) < 5)
        {
            
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

    public override void ResetValues()
    {

    }
}
