using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveStongholdScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {   
            Debug.Log("collided to leave sh");
            GameDictionary.Instance.UpdateEntry("Left Stronghold", true);
            GameDictionary.Instance.UpdateEntry("Left Stronghold Path", true);
            //load forest
            SceneLoader.Load("ForestLevel");
            
        }
    }
}
