using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node : MonoBehaviour
{
    protected NodeState _nodestate;
    public NodeState nodestate { get { return _nodestate; } }

    public abstract NodeState Evaluate();
}

public enum NodeState
{
    RUNNING, SUCCESS, FAILURE,
}
