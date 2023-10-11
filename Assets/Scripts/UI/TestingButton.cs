using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestingButton : MonoBehaviour
{
    
    
    public Transform skillholder;
    public Image whenbutton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToTesting()
    {
        whenbutton.transform.SetParent(skillholder);
        
    }
}
