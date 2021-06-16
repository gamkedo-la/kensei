﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShigieColliderScript : MonoBehaviour
{
    public GameObject shigie;

    void Update()
    {
        if (GameDictionary.choiceDictionary["Used Hidden Tunnel"])
        {
            this.gameObject.SetActive(false);
        }
        if(GameDictionary.choiceDictionary["Left Stronghold"])
        {
            this.gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!GameDictionary.choiceDictionary["Shigeie Walked"])
            {
                FindObjectOfType<DialogueManager>().forceLock = true;
                collider.GetComponent<PlayerController>().movementLocked = true;
                shigie.GetComponent<ShigieMovementScript>().targetPosition = this.gameObject;
                shigie.GetComponent<ShigieMovementScript>().onSwitch = true;

                if (!GameDictionary.choiceDictionary["Monk Path"] && !GameDictionary.choiceDictionary["Ronin Path"])
                {
                    GameDictionary.Instance.UpdateEntry("Samurai Path", true);
                    GameDictionary.Instance.UpdateEntry("Path Chosen", true);

                }
            GameDictionary.Instance.UpdateEntry("Shigeie Walked", true);
            }
        }

        if (collider.CompareTag("Shigie"))
        {
            collider.GetComponent<IshidaShigieDialogueTrigger>().reachedEntrance = true;
            FindObjectOfType<DialogueManager>().forceLock = false;
            this.gameObject.SetActive(false);
        }
    }

}
