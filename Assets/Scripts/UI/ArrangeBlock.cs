using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeBlock : MonoBehaviour
{
    private RectTransform recttransform;
    private float defaultheight = 14f;


    // Start is called before the first frame update
    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.childCount < 1) { return; }


        float prevchildheight = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform currentchild = new RectTransform();

            currentchild = transform.GetChild(i).GetComponent<RectTransform>();
            currentchild.position = new Vector2(recttransform.position.x, recttransform.position.y - prevchildheight*5 - 35);
            

            prevchildheight += currentchild.sizeDelta.y;


        }

        

        recttransform.sizeDelta = new Vector2(50, defaultheight + prevchildheight);

        

    }
}
