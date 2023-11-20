using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEquiptmentBuilder : MonoBehaviour
{
    public GameObject melee1;
    
    public GameObject shoot;
    public GameObject shoot2;
    public GameObject bullet;
    public GameObject bomb;
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
        if (unlockedblockdata.shoot2)
        {
            shoot2.SetActive(true);
        }
        else
        {
            shoot2.SetActive(false);
        }
        if (unlockedblockdata.shoot)
        {
            shoot.SetActive(true);
        }
        else
        {
            shoot.SetActive(false);
        }
        if (unlockedblockdata.bullet)
        {
            bullet.SetActive(true);
        }
        else
        {
            bullet.SetActive(false);
        }
        if (unlockedblockdata.bomb)
        {
            bomb.SetActive(true);
        }
        else
        {
            bomb.SetActive(false);
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
