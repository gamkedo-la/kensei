using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

public CircleCollider2D circle;    

public GameObject item;


    void OnTriggerEnter2D( Collider2D col )
    {
        if(col.CompareTag("Player"))
        {
        col.GetComponent<PlayerController>().targetItem = item;
        }
    }

    void OnTriggerExit2D( Collider2D col )
    {   
        if(col.CompareTag("Player"))
        {
        col.GetComponent<PlayerController>().targetItem = null;
        }
    }
    public void OnPickUp()
    {
        item.SetActive(false);
    }
}
