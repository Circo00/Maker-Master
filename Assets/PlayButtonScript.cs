using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayButtonScript : MonoBehaviour
{
    
    Button mybutton;
    public LevelManager levelmanager;
    
    // Start is called before the first frame update
    void Start()
    {
        //childimage = GetComponentInChildren<Image>();
        mybutton = GetComponent<Button>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        mybutton.onClick.AddListener(Startgame);
        //Debug.Log(myimage.color);
        //mybutton.onClick.AddListener(ChangeColor);
        

    }

    void Startgame()
    {
        levelmanager.ChangeScene("Level1");
    }

    

    

}
