using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTemplate : Node
{
    private Transform transform;
    private Transform target;

    public NodeTemplate(Transform transform, Transform target)
    {
        this.transform = transform;
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
}
