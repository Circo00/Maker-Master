using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    protected NodeState _nodestate;
    public NodeState nodestate { get { return _nodestate; } }

    public abstract NodeState Evaluate();

    public abstract void ResetValues();
}

public enum NodeState
{
    RUNNING, SUCCESS, FAILURE,
}
