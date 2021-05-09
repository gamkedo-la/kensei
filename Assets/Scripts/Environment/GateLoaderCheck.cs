using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLoaderCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(GameDictionary.choiceDictionary["Used Hidden Tunnel"]) GameDictionary.Instance.UpdateEntry("Used Hidden Tunnel", false);
    }
}
