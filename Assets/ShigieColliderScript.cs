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
            shigie.GetComponent<ShigieMovementScript>().targetPosition = this.gameObject;
            shigie.GetComponent<ShigieMovementScript>().onSwitch = true;

                if(!GameDictionary.choiceDictionary["Monk Path"] && !GameDictionary.choiceDictionary["Ronin Path"])
                {
                    GameDictionary.Instance.UpdateEntry("Samurai Path", true);
                }
        }
    
        if(collider.CompareTag("Shigie"))
        {
            collider.GetComponent<IshidaShigieDialogueTrigger>().reachedEntrance = true;
        }
    }

}
