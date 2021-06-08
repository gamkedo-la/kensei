using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnVillage : MonoBehaviour
{
    public GameObject player;
    public GameObject loadPos;
    bool moved;
    // Start is called before the first frame update
    void Update()
    {
        if(GameDictionary.choiceDictionary["Forest to Village"] && !moved) 
        {
            player.transform.position = loadPos.transform.position;
            moved = true;
            GameDictionary.Instance.UpdateEntry("Forest to Village", false);
        }
    }
}
