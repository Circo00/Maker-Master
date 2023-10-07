using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfSequenceNode : Node
{
    private playermovement _playermovement;

    public EndOfSequenceNode(playermovement _playermovement)
    {
        this._playermovement = _playermovement;

    }

    public override NodeState Evaluate()
    {
        _playermovement.StopTree();
        return NodeState.SUCCESS;
    }
}
