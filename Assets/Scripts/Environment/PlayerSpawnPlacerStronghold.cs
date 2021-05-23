using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPlacerStronghold : MonoBehaviour
{
    public GameObject player;
    public GameObject tunnel;
    bool moved;
    // Start is called before the first frame update
    void Update()
    {
       if(GameDictionary.choiceDictionary["Used Hidden Tunnel"] && !moved)
       {
           player.transform.position = tunnel.transform.position;
           moved = true;
       }
    }
}
