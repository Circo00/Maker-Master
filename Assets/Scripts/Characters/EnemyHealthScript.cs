using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{

    Collider childcollider;
    private enemyscript _enemyscript;

    [Header("Health")]
    public float maxhealth = 100f;
    private int currenthealth;
    Animator animator;
    [Space(10)]

    [Header("Damage Blink")]
    SkinnedMeshRenderer skinnedmeshrenderer;
    public float blinkintensity;
    public float blinkduration;
    float blinktimer;

    [Header("Floating Text")]
    public GameObject floatingtext;

    [Header("Audio")]
    public GameObject footstep;
    public AudioSource audiosource;
    public AudioClip coinaudio;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        childcollider = GetComponentInChildren<Collider>();
        currenthealth = (int)maxhealth;
        animator = GetComponentInChildren<Animator>();
        skinnedmeshrenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _enemyscript = GetComponent<enemyscript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("isDying") == true) { return; }

        blinktimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinktimer / blinkduration);
        float intensity = (lerp * blinkintensity) + 1.0f;
        if (skinnedmeshrenderer != null)
        {
            skinnedmeshrenderer.material.color = Color.white * intensity;
        }
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damagepoint)
    {
        currenthealth -= damagepoint;

        if (floatingtext)
        {
            ShowFloatingText(damagepoint);
        }
        
       
        blinktimer = blinkduration;

        // && animator.GetBool("isDying") == false
        if (currenthealth <= 0)
        {
            _enemyscript.enabled = false;
            animator.SetBool("isDying", true);
            if (skinnedmeshrenderer != null)
            {
                skinnedmeshrenderer.material.color = Color.white * 1;
            }
                
            float randomPitch = Random.Range(minPitch, maxPitch);
            //audiosource.pitch = randomPitch;
            //audiosource.PlayOneShot(coinaudio);
            Invoke("Die", 1);
            //footstep.SetActive(false);
            
            
        }
    }

    void ShowFloatingText(int damagepoint)
    {
        var currenttext =  Instantiate(floatingtext, transform.position, Quaternion.identity, transform);
        currenttext.GetComponent<TextMesh>().text = damagepoint.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Ground" && animator.GetBool("isDying") == true)
        {
            
            Physics.IgnoreCollision(collision.collider, childcollider, true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag != "Ground" && animator.GetBool("isDying") == true)
        {
            
            Physics.IgnoreCollision(collision.collider, childcollider, true);
        }
    }
}
