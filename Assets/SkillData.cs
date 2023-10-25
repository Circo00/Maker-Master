using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillData: MonoBehaviour
{
    public List<string> skillstringlist = new List<string>();
    private Transform skillholder;
    private Transform header;
    private Transform whenpress;

    public void CallSkillConstructer()
    {
        skillholder = GameObject.Find("Skill Holder").transform;
        whenpress = GameObject.Find("When Pressed").transform;
        whenpress.transform.SetParent(skillholder);
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
        
        return outputlist;
    }

}
