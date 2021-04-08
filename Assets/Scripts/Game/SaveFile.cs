using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class SaveFile 
{
    //public GameObject player;
    public static void SaveGame()
    {

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (!player) return;

        else
        {   
            GameDictionary.Instance.UpdateEntry("Game Saved", true);

            PlayerPrefs.SetFloat("PlayerPosition.x", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosition.y", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosition.z", player.transform.position.z);

            if(player.GetComponent<PlayerController>().weapon)
            {
                ItemDictionary.Instance.UpdateEntry( "Weapon", player.GetComponent<PlayerController>().weapon.GetComponent<ItemClass>());
            }
            else{ItemDictionary.Instance.UpdateEntry( "Weapon",null);}

            if(player.GetComponent<PlayerController>().clothing)
            {
                ItemDictionary.Instance.UpdateEntry( "Clothing", player.GetComponent<PlayerController>().clothing.GetComponent<ItemClass>());
            }
            else{ItemDictionary.Instance.UpdateEntry( "Clothing",null);}

            if(player.GetComponent<PlayerController>().smallItem)
            {
                ItemDictionary.Instance.UpdateEntry( "Small Item", player.GetComponent<PlayerController>().smallItem.GetComponent<ItemClass>());
            }
            else{ItemDictionary.Instance.UpdateEntry( "Small Item", null);}

            if(player.GetComponent<PlayerController>().bigItem)
            {
                ItemDictionary.Instance.UpdateEntry( "Big Item", player.GetComponent<PlayerController>().bigItem.GetComponent<ItemClass>());
            }
            else{ItemDictionary.Instance.UpdateEntry( "Big Item",null);}
            
            foreach(KeyValuePair<string, bool> dictionaryEntry in GameDictionary.choiceDictionary)
            {
               switch(dictionaryEntry.Value)
               {
                   case true:
                        PlayerPrefs.SetInt(dictionaryEntry.Key, 1);
                        break;

                   case false: 
                        PlayerPrefs.SetInt(dictionaryEntry.Key, 0);
                        break;
               }

            }

        }
    }

}
