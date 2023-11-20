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

        //reset all parental relationships before loading

        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Node Block");
        foreach(GameObject nodeblocks in taggedObjects)
        {
            nodeblocks.transform.SetParent(GameObject.Find("Background").transform);
        }

        //Loading action

        SkillParentSetting(skilldata.blocks, whenpress);

    }

    private void SkillParentSetting(List<NodeBlock> nodeblocklist, Transform parent)
    {
        
        

        foreach (NodeBlock nodeblock in nodeblocklist)
        {
            Transform currentchild = FindOrphanBlock(nodeblock.name);

            currentchild.SetParent(parent);

            if(nodeblock.name.Contains("Repeat"))
            {
                SkillParentSetting(nodeblock.children, currentchild);
            }
            if (nodeblock.name.Contains("Shoot"))
            {
                SkillParentSetting(nodeblock.children, currentchild);
            }
        }

    }

    private Transform FindOrphanBlock(string searchString)
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

    public SkillData ReturnSkillData()
    {
        SkillData skilldata = SaveSystem.LoadSkill();
        return skilldata;
    }

    public void ResetData()
    {
        SaveSystem.ResetData();
    }

}
