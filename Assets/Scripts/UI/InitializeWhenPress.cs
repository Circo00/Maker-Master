using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeWhenPress : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gamemanager;

    void Start()
    {
        gamemanager.LoadSkill();
    }

    
}
