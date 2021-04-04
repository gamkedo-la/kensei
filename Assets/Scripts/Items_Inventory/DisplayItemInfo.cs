using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject item;
    public GameObject player;
    public GameObject panel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("'DISPLAY ITEM Info'");
        item = player.GetComponent<PlayerController>().targetItem;
        if (item)
        {
            UnityEngine.Debug.Log(item);
            panel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("'Hide ITEM DESCRIPTION'");
        //panel.SetActive(false);
    }
}
