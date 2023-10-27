using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public List<NodeBlock> blocks = new List<NodeBlock>();

    public SkillData()
    {
        
        Transform skillholder = GameObject.Find("Skill Holder").transform;
        Transform whenpress = GameObject.Find("When Pressed").transform;
        whenpress.transform.SetParent(skillholder);
        if (skillholder != null && skillholder.childCount != 0)
        {
            Transform header = skillholder.GetChild(0);
            blocks = Constructor(header);
            
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
                    else if (parent.GetChild(i).name.Contains("Repeat"))
                    {
                        outputlist.Add( new NodeBlock("Repeat", Constructor(parent.GetChild(i))) );
                    }
                }
            }
            return outputlist;
        }
    }

}
