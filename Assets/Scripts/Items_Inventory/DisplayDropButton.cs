using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayDropButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject drop;
    public GameObject player;
    public ItemClass.Slot slot;
    bool droppable = false;

    public void OnPointerEnter( PointerEventData eventData)
    {
    drop.SetActive(true);
    droppable = true;
    }

    public void OnPointerExit( PointerEventData eventData)
    {
    drop.SetActive(false);
    droppable = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //drop item and remove from inventory
        player.GetComponent<PlayerController>().DropItem(slot);
    }
}
