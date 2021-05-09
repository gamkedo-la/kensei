using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShigieColliderScript : MonoBehaviour
{

public GameObject shigie;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerController>().movementLocked = true;
            shigie.GetComponent<SimpleMovementScript>().targetPosition = this.gameObject;
            shigie.GetComponent<SimpleMovementScript>().onSwitch = true;
        }
    }

}
