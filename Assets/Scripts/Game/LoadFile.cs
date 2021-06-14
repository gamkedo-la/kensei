﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class LoadFile
{
    static List<string> dictionaryList;
    public static Scene scene;

    private static GameObject[] objArray;
    public static void LoadGame()
    {
        //scene = SceneManager.GetActiveScene(); // unused

        /*string loadme = PlayerPrefs.GetString("Scene");
        if (loadme!=null) {
            
            // only if changed
            if (SceneManager.GetActiveScene().name != loadme) {
                Debug.Log("Current scene: "+SceneManager.GetActiveScene().name);
                Debug.Log("LoadGame is about to load scene: "+loadme);
               // SceneManager.LoadScene(loadme);
                Debug.Log("LoadGame finished loading scene: "+loadme);
            } else {
                Debug.Log("Skipping LoadScene because "+loadme+" is already the active scene.");
            }
        } else {
            Debug.Log("ERROR: no scene stored in playerperfs! Ignoring.");
        }*/

        dictionaryList = new List<string>();

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

        if (!player) {
            Debug.Log("ERROR: LoadGame did not find a player!");
            return;
        }

        Vector3 position = new Vector3(PlayerPrefs.GetFloat("PlayerPosition.x"), PlayerPrefs.GetFloat("PlayerPosition.y"), PlayerPrefs.GetFloat("PlayerPosition.z"));
       // player.transform.position = position;

        foreach (KeyValuePair<string, bool> pair in GameDictionary.choiceDictionary)
        {
            dictionaryList.Add(pair.Key);
            //GameDictionary.choiceDictionary[dictionaryEntry.Key] = false;   
        }

        foreach (string entry in dictionaryList)
        {
            GameDictionary.Instance.UpdateEntry(entry, PlayerPrefs.GetInt(entry, 0) == 1);
        }

        objArray = Resources.LoadAll("", typeof(GameObject)).Cast<GameObject>().ToArray();
        
        foreach (GameObject obj in objArray)
        {
            if (obj != null)
            {
                if (PlayerPrefs.HasKey(scene.name + "_" + obj.GetComponent<ItemClass>().itemName + ".x"))
                {
                    if (PlayerPrefs.HasKey(scene.name + "_" + obj.GetComponent<ItemClass>().itemName + ".y"))
                    {
                        player.GetComponent<PlayerController>().SpawnItem(obj);
                        Debug.Log("Tried to Spawn"+obj.name);
                    }
                }
            }
        }
        player.GetComponent<StateTracker>().playerCombatPoints = PlayerPrefs.GetInt("Player Combat Score");
        Debug.Log("made it to enforce");
        EnforceDictionary();
        player.GetComponent<StateTracker>().CalculateNewCombatScore();
    }

    public static GameObject ConstructItem(ItemClass item)
    {
        /*GameObject itemToSpawn = new GameObject(item.itemName);

        itemToSpawn.AddComponent<CanvasRenderer>();

        itemToSpawn.AddComponent<SpriteRenderer>();
        itemToSpawn.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
        itemToSpawn.GetComponent<SpriteRenderer>().sortingOrder = 1;

        itemToSpawn.AddComponent<CircleCollider2D>();
        itemToSpawn.GetComponent<CircleCollider2D>().radius = 5;

        itemToSpawn.AddComponent<PickUpItem>();
        itemToSpawn.GetComponent<PickUpItem>().circle = itemToSpawn.GetComponent<CircleCollider2D>();
        //itemToSpawn.GetComponent<PickUpItem>().item = itemToSpawn;

        itemToSpawn.AddComponent<ItemClass>();
        itemToSpawn.GetComponent<ItemClass>().itemName = item.itemName;
        itemToSpawn.GetComponent<ItemClass>().itemSlot = item.itemSlot;
        itemToSpawn.GetComponent<ItemClass>().combatPoints = item.combatPoints;
        itemToSpawn.GetComponent<ItemClass>().itemSprite = item.itemSprite;

        itemToSpawn.SetActive(false);*/
        return null;
    }
    public static void EnforceDictionary()
    {
        Debug.Log("enforcing");
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.GetComponent<PlayerController>().EnforceDictionary();

    }
}
