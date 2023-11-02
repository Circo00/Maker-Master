using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnlockedBlocksData
{
    public bool melee1 = true;
    public bool melee2 = false;
    public bool ranged1 = false;
    public bool ranged2 = false;
    public bool repeat10 = false;

    public UnlockedBlocksData()
    {
        
        
    }

    public void UnlockBlock(string blockname)
    {
        if (blockname == "melee2")
        {
            this.melee2 = true;
        }
        if (blockname == "ranged1")
        {
            this.ranged1 = true;
        }
        if (blockname == "ranged2")
        {
            this.ranged2 = true;
        }
        if (blockname == "repeat10")
        {
            this.repeat10 = true;
        }
    }

    public bool GetUnlockedBlockData(string blockname)
    {
        if (blockname == "melee2")
        {
            return this.melee2;
        }
        if (blockname == "ranged1")
        {
            return this.ranged1;
        }
        if (blockname == "ranged2")
        {
            return this.ranged2;
        }
        if (blockname == "repeat10")
        {
            return this.repeat10;
        }
        else
        {
            return true;
        }
    }

}
