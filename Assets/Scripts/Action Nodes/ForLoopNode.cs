using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLoopNode : Node
{
    private int n;
    protected List<Node> nodes = new List<Node>();

    private int counter;

    private float previoustime = 0;
    private float cooldowntime = 0.2f;

    public ForLoopNode(int n, List<Node> nodes)
    {
        this.n = n;
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        if (counter >= n) { return NodeState.SUCCESS;}
        if (Time.time - previoustime < cooldowntime) { return NodeState.FAILURE; }
        
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    _nodestate = NodeState.FAILURE;
                    return _nodestate;
            }
        }

        counter += 1;
        Debug.Log(counter);
        previoustime = Time.time;
        
        return NodeState.FAILURE;
    }

    public override void ResetValues()
    {
        counter = 0;
    }
}
