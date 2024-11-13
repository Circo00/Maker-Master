using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject spawnobject;
    private int enemyCount;
    private float previoustime = 3;
    public float cooldowntime = 2f;
    public float enemylimit = 0;
    void Start()
    {
        Instantiate(spawnobject, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - previoustime >= cooldowntime)
        {
            previoustime = Time.time;
            CountEnemies();
            Debug.Log(enemyCount);
            if(enemyCount <= enemylimit)
            {
                SpawnObject();
                
            }
            
        }
        
    }

    void SpawnObject()
    {
        Instantiate(spawnobject, transform.position, Quaternion.identity);
    }

    private void CountEnemies()
    {
        // Find all game objects with the "enemies" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Update the enemy count
        enemyCount = enemies.Length;
    }
}
