using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public static class SaveFile 
{
    private static Scene scene;
    //public GameObject player;
    public static void SaveGame()
    {
        scene = SceneManager.GetActiveScene();

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
        PlayerPrefs.SetInt("Player Combat Score", player.GetComponent<StateTracker>().playerCombatPoints);
    }

    public static void StoreItemLocation(GameObject item)
    {
        scene = SceneManager.GetActiveScene();

        PlayerPrefs.SetFloat(scene.name + "_" + item.GetComponent<ItemClass>().itemName + ".x", item.transform.position.x);
        PlayerPrefs.SetFloat(scene.name + "_" + item.GetComponent<ItemClass>().itemName + ".y", item.transform.position.y);
    }

    public static void ClearItemLocation(GameObject item)
    {
        scene = SceneManager.GetActiveScene();

        PlayerPrefs.DeleteKey(scene.name + "_" + item.GetComponent<ItemClass>().itemName + ".x");
        PlayerPrefs.DeleteKey(scene.name + "_" + item.GetComponent<ItemClass>().itemName + ".y");
    }
}
