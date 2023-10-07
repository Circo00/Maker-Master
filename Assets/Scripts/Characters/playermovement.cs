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

    private Node topnode;
    private bool treeenabled = false;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        shootablerb = shootable.GetComponent<Rigidbody>();
        ConstructBehaviourTree();


    }

    private void ConstructBehaviourTree()
    {
        EndOfSequenceNode endofsequencenode = new EndOfSequenceNode(this);
        RangedAttackNode rangedattacknode = new RangedAttackNode(shootpos, shootable, shootablerb, shootableforce, shootingoffset);
        MeleeAttackNode meleeattacknode = new MeleeAttackNode(animator, transform, attackrange, attackdamage, spreadangle, numrays, attackdelay);
        //StartMeleeAttackNode startmeleeattacknode = new StartMeleeAttackNode(attackdelay, animator);

        topnode = new Sequence(new List<Node> {meleeattacknode, rangedattacknode, endofsequencenode});;

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
            topnode.Evaluate();
        }

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
