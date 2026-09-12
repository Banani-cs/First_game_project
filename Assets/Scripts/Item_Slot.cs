using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public GameObject Item //This is a property, heads up incase you want to you it again in this script, if u ever add a new reference after this, you will have to change the reference to the property instead of the variable.
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedItem = eventData.pointerDrag;
        Transform originalSlot = draggedItem.transform.parent;
        Debug.Log("OnDrop");
        //if there is not item already then set our item.
        if (!Item)
        {
            draggedItem.transform.SetParent(transform);
            draggedItem.transform.localPosition = new Vector2(0, 0);
        }
        else
        {
            Item.transform.SetParent(originalSlot);
            Item.transform.localPosition = new Vector2(0, 0);
            draggedItem.transform.SetParent(transform);
            draggedItem.transform.localPosition = new Vector2(0, 0);
        }
    }
}