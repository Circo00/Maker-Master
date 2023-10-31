using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator transition;
    public float transition_time;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene(string scenename)
    {
        StartCoroutine(LoadScene(scenename));
    }

    IEnumerator LoadScene(string scenename)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transition_time);

        SceneManager.LoadScene(scenename);
    }

    
}
