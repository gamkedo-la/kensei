using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerScriptStronghold : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if(GameDictionary.choiceDictionary["Samurai Path"] && !GameDictionary.choiceDictionary["Left Stronghold"])
        {
            this.gameObject.SetActive(false);
        }
    }
}
