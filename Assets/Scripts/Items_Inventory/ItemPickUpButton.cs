using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpButton : MonoBehaviour
{
    public GameObject player;
    public void PickSomethingUp()
    {
        GameObject targetItem = player.GetComponent<PlayerController>().targetItem;
        player.GetComponent<PlayerController>().AddItem(targetItem);
    }
}
