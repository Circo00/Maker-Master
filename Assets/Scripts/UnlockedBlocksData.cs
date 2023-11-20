using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnlockedBlocksData
{
    public bool melee1 = true;
    public bool shoot2 = false;
    public bool shoot = false;
    public bool bullet = false;
    public bool bomb = false;
    public bool repeat10 = false;

    public UnlockedBlocksData()
    {
        
        
    }

    public void UnlockBlock(string blockname)
    {
        if (blockname == "melee2")
        {
            this.shoot2 = true;
        }
        if (blockname == "shoot")
        {
            this.shoot = true;
            this.bullet = true;
        }
        if (blockname == "bomb")
        {
            this.bomb = true;
        }
        if (blockname == "shoot2")
        {
            this.shoot2 = true;
        }
        if (blockname == "repeat10")
        {
            this.repeat10 = true;
        }
    }

    public bool GetUnlockedBlockData(string blockname)
    {
        if (blockname == "shoot2")
        {
            return this.shoot2;
        }
        if (blockname == "shoot")
        {
            return this.shoot;
        }
        if (blockname == "bullet")
        {
            return this.bullet;
        }
        if (blockname == "bomb")
        {
            return this.bomb;
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
