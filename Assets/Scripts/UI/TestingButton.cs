using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingButton : MonoBehaviour
{
    Button mybutton;
    public SceneManagerScript scenemanager;
    public Image whenbutton;
    public Transform skillholder;

    // Start is called before the first frame update
    void Start()
    {
        mybutton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        mybutton.onClick.AddListener(ToTesting);
    }

    void ToTesting()
    {
        whenbutton.transform.SetParent(skillholder);
        scenemanager.ChangeScene("Level1");
    }
}
