using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateSkillTreeString()
    {

        List<string> skillstringlist = new List<string>();
        Transform skillholder = GameObject.Find("Skill Holder").transform;
        Transform whenpress = GameObject.Find("When Pressed").transform;
        whenpress.transform.SetParent(skillholder);
        if (skillholder != null && skillholder.childCount != 0)
        {
            Transform header = skillholder.GetChild(0);
            Constructor(header);
        }


        List<Node> Constructor(Transform parent)
        {
            List<Node> outputlist = new List<Node>();            
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    if (parent.GetChild(i).name.Contains("Melee"))
                    {
                        //outputlist.Add();
                    }
                    else if (parent.GetChild(i).name.Contains("Ranged"))
                    {
                        //outputlist.Add();
                    }
                    else if (parent.GetChild(i).name.Contains("Repeat"))
                    {
                        //outputlist.Add();
                    }
                }
            }
            return outputlist;
        }       
    }

}
