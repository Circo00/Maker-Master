using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfSequenceNode : Node
{
    private playermovement _playermovement;
    private List<Node> nodes = new List<Node>();

    public EndOfSequenceNode(playermovement _playermovement, List<Node> nodes)
    {
        this._playermovement = _playermovement;
        this.nodes = nodes;

    }

    public override NodeState Evaluate()
    {
        _playermovement.StopTree();
        foreach (var node in nodes)
        {
            node.ResetValues();
        }
        return NodeState.SUCCESS;
    }

    public override void ResetValues()
    {

    }
}
