using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject spawnobject;

    private float previoustime = 0;
    public float cooldowntime = 2f;
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
            SpawnObject();
        }
        
    }

    void SpawnObject()
    {
        Instantiate(spawnobject, transform.position, Quaternion.identity);
    }
}
