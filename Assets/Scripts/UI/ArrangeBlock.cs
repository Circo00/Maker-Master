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
        if (transform.childCount <= 1) { return; }

        float rectheight = 0f;
        for (int i = 0; i < transform.childCount; i++)
        {
            rectheight += transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
        }

        recttransform.sizeDelta = new Vector2(50, 14 + rectheight);

        Debug.Log(recttransform.sizeDelta);

    }
}
