using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveStongholdScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameDictionary.Instance.UpdateEntry("Left Stronghold", true);
            //load forest
        }
    }
}
