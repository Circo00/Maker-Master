using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEquiptmentBuilder : MonoBehaviour
{
    public GameObject melee1;
    public GameObject melee2;
    public GameObject ranged1;
    public GameObject ranged2;
    public GameObject repeat10;




    // Start is called before the first frame update
    private void Awake()
    {
        UnlockedBlocksData unlockedblockdata = SaveSystem.LoadUnlockedBlock();
        if (unlockedblockdata.melee1)
        {
            melee1.SetActive(true);
        }
        else
        {
            melee1.SetActive(false);
        }
        if (unlockedblockdata.melee2)
        {
            melee2.SetActive(true);
        }
        else
        {
            melee2.SetActive(false);
        }
        if (unlockedblockdata.ranged1)
        {
            ranged1.SetActive(true);
        }
        else
        {
            ranged1.SetActive(false);
        }
        if (unlockedblockdata.ranged2)
        {
            ranged2.SetActive(true);
        }
        else
        {
            ranged2.SetActive(false);
        }
        if (unlockedblockdata.repeat10)
        {
            repeat10.SetActive(true);
        }
        else
        {
            repeat10.SetActive(false);
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
