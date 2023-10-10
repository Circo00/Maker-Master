using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeBlock : MonoBehaviour
{
    private RectTransform recttransform;

    // Start is called before the first frame update
    void Start()
    {
        recttransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.childCount < 1) { return; }

        float rectheight = 14f;

        float prevchildheight = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform currentchild = new RectTransform();

            currentchild = transform.GetChild(i).GetComponent<RectTransform>();
            rectheight += currentchild.sizeDelta.y;
            currentchild.position = new Vector2(recttransform.position.x, recttransform.position.y + recttransform.sizeDelta.y/2 - 7 - prevchildheight - currentchild.sizeDelta.y / 2 +50);
            prevchildheight += currentchild.sizeDelta.y * 5;


        }

        recttransform.sizeDelta = new Vector2(50, rectheight);

        Debug.Log(recttransform.sizeDelta);

    }
}
