using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeBlock
{
    public string name;
    private List<NodeBlock> children = new List<NodeBlock>();

    public NodeBlock(string name, List<NodeBlock> children)
    {
        this.name = name;
        this.children = children;
    }
}


