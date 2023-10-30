using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    Rigidbody rb;
    public float radius;
    public float explosionforce;
    public GameObject effect;
    public int explosiondamage;
    public AudioSource audiosource;
    public AudioClip audioclip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Enemy")
        {
            explosioneffect();
            GameObject spawnedeffect = Instantiate(effect, transform.position, transform.rotation);
            audiosource.PlayOneShot(audioclip);
            Destroy(spawnedeffect, 3);
            Destroy(gameObject, 1);
        }
        
    }

    void explosioneffect()
    {
        Collider[] affected = Physics.OverlapSphere(rb.position, radius);

        foreach (Collider nearby in affected)
        {
            Rigidbody nearby_rb = nearby.GetComponent<Rigidbody>();
            EnemyHealthScript enemyhealthscript = nearby.GetComponentInParent<EnemyHealthScript>();
            if (nearby_rb != null)
            {
                nearby_rb.AddExplosionForce(explosionforce, rb.position, radius);
            }
            if (enemyhealthscript != null)
            {
                enemyhealthscript.TakeDamage(explosiondamage);
            }
        }
    }
}
