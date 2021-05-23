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
        }
    }
}
