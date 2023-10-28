using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public List<NodeBlock> blocks = new List<NodeBlock>();

    public SkillData()
    {
        
        
        Transform whenpress = GameObject.Find("When Pressed").transform;
        
        if (whenpress != null)
        {
            
            blocks = Constructor(whenpress);
            
        }


        List<NodeBlock> Constructor(Transform parent)
        {
            List<NodeBlock> outputlist = new List<NodeBlock>();
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    if (parent.GetChild(i).name.Contains("Melee"))
                    {
                        outputlist.Add( new NodeBlock("Melee", new List<NodeBlock>()) );
                    }
                    else if (parent.GetChild(i).name.Contains("Ranged"))
                    {
                        outputlist.Add( new NodeBlock("Ranged", new List<NodeBlock>()) );
                    }
                    else if (parent.GetChild(i).name.Contains("Repeat 10"))
                    {
                        outputlist.Add( new NodeBlock("Repeat 10", Constructor(parent.GetChild(i))) );
                    }
                }
            }
            return outputlist;
        }
    }

}
