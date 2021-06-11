using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageLoaderTrigger : MonoBehaviour
{
    public Collider2D collider;

    public void OnTriggerEnter2D()
    {
        GameDictionary.Instance.UpdateEntry("Forest to Village", true); 
    }

}
