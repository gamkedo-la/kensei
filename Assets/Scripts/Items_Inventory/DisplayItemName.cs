using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayItemName : MonoBehaviour, IPointerEnterHandler
{
    public GameObject player;
    public GameObject item;
    // Item should auto update based on what item is in the slot, like it does for the each slot right now
    public Text itemName;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //player.GetComponent<PlayerController>().checkItemInInventory(slot);
        item = player.GetComponent<PlayerController>().targetItem;
        UnityEngine.Debug.Log("item");
        UnityEngine.Debug.Log(item);
        if (item)
        {
            UnityEngine.Debug.Log(item);
            itemName.text = player.GetComponent<ItemClass>().itemName;
        }
    }
    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("'DISPLAY ITEM DESCRIPTION'");
        //player.GetComponent<PlayerController>().checkItemInInventory(slot);
        item = player.GetComponent<PlayerController>().targetItem;


        if (item)
        {
            UnityEngine.Debug.Log(item);
            itemName.text = player.GetComponent<ItemClass>().itemName;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("'Hide ITEM DESCRIPTION'");
    }
    */
}
