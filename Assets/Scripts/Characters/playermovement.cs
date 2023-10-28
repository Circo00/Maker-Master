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



        //ConstructBehaviourTree();

        topnode = new Sequence(Constructor(header));
        



    }

    private List<Node> Constructor(Transform parent)
    {
        List<Node> outputlist = new List<Node>();

        if (parent.childCount > 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {

                if (parent.GetChild(i).name.Contains("Melee"))
                {
                    outputlist.Add(new MeleeAttackNode(animator, transform, attackrange, attackdamage, spreadangle, numrays, attackdelay));
                }
                else if (parent.GetChild(i).name.Contains("Ranged"))
                {
                    outputlist.Add(new RangedAttackNode(shootpos, shootable, shootablerb, shootableforce, shootingoffset));
                }
                else if (parent.GetChild(i).name.Contains("Repeat"))
                {
                    outputlist.Add(new ForLoopNode(5, Constructor(parent.GetChild(i).transform)));
                }

            }
        }

        return outputlist;
    }

    private void ConstructBehaviourTree()
    {
        
        MeleeAttackNode meleeattacknode = new MeleeAttackNode(animator, transform, attackrange, attackdamage, spreadangle, numrays, attackdelay);
        //MeleeAttackNode meleeattacknode1 = new MeleeAttackNode(animator, transform, attackrange, attackdamage, spreadangle, numrays, attackdelay);
        RangedAttackNode rangedattacknode = new RangedAttackNode(shootpos, shootable, shootablerb, shootableforce, shootingoffset);
        //RangedAttackNode rangedattacknode1 = new RangedAttackNode(shootpos, shootable, shootablerb, shootableforce, shootingoffset);

        ForLoopNode forloopnode = new ForLoopNode(10, new List<Node> {meleeattacknode});

        if (header.childCount > 0)
        {
            for (int i = 0; i < header.childCount; i++)
            {

                if (header.GetChild(i).name.Contains("Melee"))
                {
                    whenpressed.Add(new MeleeAttackNode(animator, transform, attackrange, attackdamage, spreadangle, numrays, attackdelay));
                }
                else if (header.GetChild(i).name.Contains("Ranged"))
                {
                    whenpressed.Add(new RangedAttackNode(shootpos, shootable, shootablerb, shootableforce, shootingoffset));
                }

            }
        }

        
        
        



        topnode = new Sequence(whenpressed);

    }

    // Update is called once per frame
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
