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
        if(GameDictionary.choiceDictionary["Left Stronghold Path"] && !moved) 
        {
            player = FindObjectOfType<PlayerController>().gameObject;
            player.transform.position = loadPos.transform.position;
            GameDictionary.Instance.UpdateEntry("Left Stronghold Path", false);
            moved = true;
        }
    }
}
