using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{

    private Animator animator;
    private playermovement _playermovement;

    [Header("Healthbar")]
    public float maxhealth = 100f;
    private int currenthealth;
    [SerializeField] private Healthbar healthbar;
    [Space(10)]

    [Header("Damage Blink")]
    SkinnedMeshRenderer skinnedmeshrenderer;
    public float blinkintensity;
    public float blinkduration;
    float blinktimer;
    [Space(10)]

    [Header("Scene Management")]
    public SceneManagerScript scenemanager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _playermovement = GetComponent<playermovement>();

        currenthealth = (int)maxhealth;
        healthbar.UpdateHealthbar(maxhealth, currenthealth);

        skinnedmeshrenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isDying") == true) { return; }
        //Debug.Log(currenthealth);
        
        blinktimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinktimer / blinkduration);
        float intensity = (lerp * blinkintensity) + 1.0f;
        if(skinnedmeshrenderer != null)
        {
            skinnedmeshrenderer.material.color = Color.white * intensity;
        }
        
    }

    public void TakeDamage(int damagepoint)
    {
        currenthealth -= damagepoint;
        healthbar.UpdateHealthbar(maxhealth, currenthealth);

        blinktimer = blinkduration;

        //Debug.Log("Damage Taken");
        if (currenthealth <= 0)
        {
            _playermovement.enabled = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isDying", true);
            Invoke("Die", 2);
        }
    }

    private void Die()
    {
        scenemanager.ChangeScene("Main Menu");
    }
}
