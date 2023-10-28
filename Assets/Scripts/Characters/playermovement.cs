using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class playermovement : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;

    [Header("Controller")]
    public Joystick joystick;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float movespeed = 10f;
    [Space(10)]

    [Header("Attacking")]
    public float attackrange;
    public int attackdamage;
    public float spreadangle = 30f;
    public int numrays = 5;
    public float attackdelay = 2;
    [Space(10)]

    [Header("Ranged Attack")]
    public Transform shootpos;
    public GameObject shootable;
    private Rigidbody shootablerb;
    public float shootableforce = 1000f;
    public float shootingoffset = 10f;
    [Space(10)]


    [Header("Repeated Ranged Attack")]
    public float firingrate = 0.1f;
    public int shootablecount = 10;
    float firetimer;

    private Transform skillholder;
    private Node topnode;
    private bool treeenabled = false;
    private Transform header;

    private List<Node> whenpressed = new List<Node>();



    public GameManager gamemanager;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        shootablerb = shootable.GetComponent<Rigidbody>();
        //skillholder = GameObject.Find("Skill Holder").transform;
        if(skillholder != null && skillholder.childCount != 0)
        {
            header = skillholder.GetChild(0);
        }

        SkillData skilldata = gamemanager.ReturnSkillData();

        //ConstructBehaviourTree();

        topnode = new Sequence(Constructor(skilldata.blocks));
        



    }

    private List<Node> Constructor(List<NodeBlock> nodeblocklist)
    {
        List<Node> outputlist = new List<Node>();

        foreach (NodeBlock nodeblock in nodeblocklist)
        {
            if (nodeblock.name.Contains("Melee"))
            {
                outputlist.Add(new MeleeAttackNode(animator, transform, attackrange, attackdamage, spreadangle, numrays, attackdelay));
            }
            else if (nodeblock.name.Contains("Ranged"))
            {
                outputlist.Add(new RangedAttackNode(shootpos, shootable, shootablerb, shootableforce, shootingoffset));
            }
            else if (nodeblock.name.Contains("Repeat"))
            {
                outputlist.Add(new ForLoopNode(10, Constructor(nodeblock.children)));
            }


        }


        return outputlist;
    }


    void Update()
    {
        float horizontal_value = joystick.Horizontal;
        float vertical_value = joystick.Vertical;
        

        rb.AddForce(horizontal_value * speed * Time.deltaTime, 0, vertical_value * speed * Time.deltaTime);

        Vector3 inputDir = new Vector3(horizontal_value, 0, vertical_value);
        if (inputDir.magnitude > 0.2)
        {
            transform.forward = inputDir;
        }
        SpeedControl();
        AnimationControl();

        if (treeenabled)
        {
            if (topnode.Evaluate() == NodeState.SUCCESS)
            {
                
                topnode.ResetValues();
                treeenabled = false;
            };
        }

        Debug.Log(animator.GetBool("isAttacking"));

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > movespeed)
        {
            Vector3 limitedVel = flatVel.normalized * movespeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void AnimationControl()
    {
        if (rb.velocity.magnitude > 0.2)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }


    private void RangedAttack()
    {
        GameObject spawnedshootable = Instantiate(shootable, shootpos.position, shootpos.rotation);
        Rigidbody shootablerb = spawnedshootable.GetComponent<Rigidbody>();
        shootablerb.AddRelativeForce(Random.Range(shootingoffset, -shootingoffset), 0, shootableforce * Time.deltaTime, ForceMode.Impulse);

    }

    private void RepeatedRangedAttack()
    {
        int shotshootablecount = 0;
        while (shotshootablecount < shootablecount)
        {
            Invoke("RangedAttack", shotshootablecount * firingrate);
            shotshootablecount += 1;
        }
        

        
    }

    public void RunTree()
    {
        treeenabled = true;
    }

    public void StopTree()
    {
        treeenabled = false;
    }




}
