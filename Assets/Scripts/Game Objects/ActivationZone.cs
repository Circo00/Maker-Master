using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public List<GameObject> waveenemylist = new List<GameObject>();
    public GameObject blockade;
    private bool activated = false;


    // Start is called before the first frame update
    void Start()
    {
        DisableEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !activated)
        {
            Debug.Log("Entered Activation Zone");
            EnableEnemies();
            blockade.SetActive(true);
            activated = true;
        }
        
    }

    private void EnableEnemies()
    {
        foreach(GameObject enemy in waveenemylist)
        {
            enemy.SetActive(true);
        }
    }

    private void DisableEnemies()
    {
        foreach (GameObject enemy in waveenemylist)
        {
            enemy.SetActive(false);
        }
    }
}
