using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public static class SaveFile 
{
    //public GameObject player;
    public static void SaveGame(SceneLoader.Scene scene)
    {

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (!player) return;

        else
        {   
            GameDictionary.Instance.UpdateEntry("Game Saved", true);
            PlayerPrefs.SetString("Scene", scene.ToString());
            PlayerPrefs.SetFloat("PlayerPosition.x", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosition.y", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosition.z", player.transform.position.z);

            foreach(KeyValuePair<string,bool> pair in GameDictionary.choiceDictionary)
            {
                PlayerPrefs.SetInt(pair.Key, (pair.Value ? 1 : 0));
            }

           /* if(player.GetComponent<PlayerController>().weapon)
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
            else{ItemDictionary.Instance.UpdateEntry( "Big Item",null);}*/
        }
    }
}
