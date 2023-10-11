using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonScript : MonoBehaviour
{
    GameObject player;
    private playermovement playerscript;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<playermovement>();
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        playerscript.RunTree();
    }
}
