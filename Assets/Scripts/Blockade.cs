using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{
    public GameObject effect;
    public TutorialManager tutmanager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            CountEnemies();
            
        }
    }

    private void CountEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("Enemy 2");
        if (enemies.Length == 0 && enemies2.Length == 0)
        {
            //GameObject spawnedeffect = Instantiate(effect, transform.position, transform.rotation);
            
            //Destroy(spawnedeffect, 3);
            //Destroy(gameObject, 1);
            gameObject.GetComponent<Collider>().enabled = false;
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            tutmanager.DisplayNotification(0, 3, "Please eliminate all enemies to proceed.");
        }
        
    }
}
