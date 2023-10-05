using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3);   
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.transform.parent != null && collision.collider.transform.parent.tag == "Enemy")
        {
            EnemyHealthScript enemyhealth = collision.collider.transform.parent.GetComponent<EnemyHealthScript>();
            enemyhealth.TakeDamage(damage);
            //Debug.Log("Hit Enemy!!!!");
            Destroy(gameObject);

        }
    }
}
