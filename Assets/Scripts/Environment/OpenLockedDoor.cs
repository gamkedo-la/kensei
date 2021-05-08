﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLockedDoor : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.CompareTag("Player"))
        {
            door.SetActive(false);
        }
    }
}
