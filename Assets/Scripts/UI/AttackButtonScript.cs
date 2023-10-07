using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonScript : MonoBehaviour
{
    GameObject player;
    private playermovement playerscript;
    private Button button;

    private float previoustime = 0;
    public float cooldowntime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<playermovement>();
        button = GetComponent<Button>();


    }

    // Update is called once per frame
    void Update()
    {
        button.onClick.AddListener(Attack);
    }

    void Attack()
    {
        playerscript.RunTree();
    }
}
