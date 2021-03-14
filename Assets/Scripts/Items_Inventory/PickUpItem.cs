using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

public CircleCollider2D circle;    
private GameObject player;

public GameObject item;


    void OnTriggerEnter2D( Collider2D col )
    {
        if(col.CompareTag("Player"))
        {
            player = col.gameObject;
            col.GetComponent<PlayerController>().targetItem = item;

        }
    }

    void OnTriggerExit2D( Collider2D col )
    {   
        if(col.CompareTag("Player"))
        {
        player = col.gameObject;
        col.GetComponent<PlayerController>().targetItem = null;

        }
    }
    public void OnPickUp()
    {
        item.SetActive(false);
        player.GetComponent<PlayerController>().targetItem = null;
    }
}
