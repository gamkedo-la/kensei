using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPlacerStronghold : MonoBehaviour
{
    public GameObject player;
    public GameObject tunnel;
    // Start is called before the first frame update
    void Awake()
    {
       if(GameDictionary.choiceDictionary["Used Hidden Tunnel"])
       {
           player.transform.position = tunnel.transform.position;
       }
    }
}
