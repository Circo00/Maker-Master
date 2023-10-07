using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    protected List<Node> nodes = new List<Node>();

    public Selector(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodestate = NodeState.RUNNING;
                    return _nodestate;
                    
                case NodeState.SUCCESS:
                    _nodestate = NodeState.SUCCESS;
                    return _nodestate;
                    
                case NodeState.FAILURE:
                    break;
            }
        }
        _nodestate = NodeState.FAILURE;
        return _nodestate;
    }
}
