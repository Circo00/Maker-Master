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
        SkillData thingy = SaveSystem.LoadSkill();

        foreach(NodeBlock abcde in thingy.blocks)
        {
            Debug.Log(abcde.name);
        }
    }

}
