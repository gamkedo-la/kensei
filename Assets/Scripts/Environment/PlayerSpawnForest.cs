using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnForest : MonoBehaviour
{
    public GameObject player;
    public GameObject loadPos;
    bool moved;
    // Start is called before the first frame update
    void Update()
    {
        if(GameDictionary.choiceDictionary["Samurai Path"] && !moved) 
        {
            player.transform.position = loadPos.transform.position;
            moved = true;
        }
    }
}
