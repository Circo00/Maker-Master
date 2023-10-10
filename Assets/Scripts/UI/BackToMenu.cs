using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    Button mybutton;
    public SceneManagerScript scenemanager;
    // Start is called before the first frame update
    void Start()
    {
        mybutton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        mybutton.onClick.AddListener(Menu);
    }

    public void Menu()
    {
        scenemanager.ChangeScene("Main Menu");
    }
}