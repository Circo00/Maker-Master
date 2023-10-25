using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData: MonoBehaviour
{
    public List<string> skillstringlist = new List<string>();
    private Transform skillholder;
    private Transform header;

    public void CallSkillConstructer()
    {
        skillholder = GameObject.Find("Skill Holder").transform;
        if (skillholder != null && skillholder.childCount != 0)
        {
            header = skillholder.GetChild(0);
        }
        Constructor(header);
    }

    private List<string> Constructor(Transform parent)
    {
        List<string> outputlist = new List<string>();

        if (parent.childCount > 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {

                if (parent.GetChild(i).name.Contains("Melee"))
                {
                    outputlist.Add("Melee");
                }
                else if (parent.GetChild(i).name.Contains("Ranged"))
                {
                    outputlist.Add("Ranged");
                }
                else if (parent.GetChild(i).name.Contains("Repeat"))
                {
                    outputlist.Add("Repeat");
                }

            }
        }

        Debug.Log(outputlist);
        return outputlist;
    }

}
