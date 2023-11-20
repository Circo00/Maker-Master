using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slots : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Draggable draggable = dropped.GetComponent<Draggable>();
        //draggable.parentafterdrag = transform;
        if ((transform.name == "When Pressed" || transform.name == "Repeat 10" || transform.name == "Background") && (!draggable.name.Contains("Bullet") && !draggable.name.Contains("Bomb")))
        {
            draggable.parentafterdrag = transform;
        }
        if ((transform.name.Contains("Shoot") && transform.childCount == 0|| transform.name == "Background" ) && (draggable.name.Contains("Bullet") || draggable.name.Contains("Bomb")))
        {
            draggable.parentafterdrag = transform;
        }
    }

    

    
    
}
