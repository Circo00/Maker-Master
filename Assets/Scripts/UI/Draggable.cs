using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public AudioClip blockattach;
    public AudioSource audiosource;

    [HideInInspector]public Transform parentafterdrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Start Dragging");
        parentafterdrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Dragging");
        transform.SetParent(parentafterdrag);
        if(!transform.name.Contains("Bullet") && !transform.name.Contains("Bomb") && (parentafterdrag.name == "When Pressed" || parentafterdrag.name == "Repeat 10"))
        {
            audiosource.PlayOneShot(blockattach);
        }
        if ((transform.name.Contains("Bullet") || transform.name.Contains("Bomb")) && (parentafterdrag.name.Contains("Shoot")))
        {
            audiosource.PlayOneShot(blockattach);
        }

        image.raycastTarget = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
