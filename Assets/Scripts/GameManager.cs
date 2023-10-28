using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void SaveSkill()
    {
        SaveSystem.SaveSkill();
            
    }

    public void LoadSkill()
    {
        SkillData skilldata = SaveSystem.LoadSkill();
        Transform whenpress = GameObject.Find("When Pressed").transform;
        SkillParentSetting(skilldata.blocks, whenpress);

        //return skilldata;
    }

    public void SkillParentSetting(List<NodeBlock> nodeblocklist, Transform parent)
    {
        
        

        foreach (NodeBlock nodeblock in nodeblocklist)
        {
            Transform currentchild = FindNodeBlockWithNoParent(nodeblock.name);

            currentchild.SetParent(parent);

            if(nodeblock.name.Contains("Repeat"))
            {
                SkillParentSetting(nodeblock.children, currentchild);
            }
        }

    }

    private Transform FindNodeBlockWithNoParent(string searchString)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Node Block");

        foreach (GameObject obj in taggedObjects)
        {
            if (obj.transform.parent.tag != "Node Block" && obj.name.Contains(searchString))
            {
                return obj.transform;
            }
        }

        return null;
    }

}
