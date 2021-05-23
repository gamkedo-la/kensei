using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadFile 
{
    static List<string> dictionaryList;
    public static void LoadGame()
    {
        dictionaryList = new List<string>();
        Debug.Log("LoadGame Called");
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

        Vector3 position = new Vector3(PlayerPrefs.GetFloat("PlayerPosition.x"), PlayerPrefs.GetFloat("PlayerPosition.y"), PlayerPrefs.GetFloat("PlayerPosition.z"));
        
        foreach(KeyValuePair<string,bool> pair in GameDictionary.choiceDictionary)
        {
            dictionaryList.Add(pair.Key);
            //GameDictionary.choiceDictionary[dictionaryEntry.Key] = false;   
        }

        foreach(string entry in dictionaryList)
        {
            GameDictionary.Instance.UpdateEntry(entry, PlayerPrefs.GetInt(entry, 0) == 1);
        }
        
        Debug.Log("made it to enforce");
        EnforceDictionary();
   }

    public static GameObject ConstructItem(ItemClass item)
    {
        GameObject itemToSpawn = new GameObject(item.itemName);

        itemToSpawn.AddComponent<CanvasRenderer>();

        itemToSpawn.AddComponent<SpriteRenderer>();
            itemToSpawn.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
            itemToSpawn.GetComponent<SpriteRenderer>().sortingOrder = 1;

        itemToSpawn.AddComponent<CircleCollider2D>();
            itemToSpawn.GetComponent<CircleCollider2D>().radius = 5;

        itemToSpawn.AddComponent<PickUpItem>();
            itemToSpawn.GetComponent<PickUpItem>().circle = itemToSpawn.GetComponent<CircleCollider2D>();
            itemToSpawn.GetComponent<PickUpItem>().item = itemToSpawn;
        
        itemToSpawn.AddComponent<ItemClass>();
            itemToSpawn.GetComponent<ItemClass>().itemName = item.itemName;
            itemToSpawn.GetComponent<ItemClass>().itemSlot = item.itemSlot;
            itemToSpawn.GetComponent<ItemClass>().combatPoints = item.combatPoints;
            itemToSpawn.GetComponent<ItemClass>().itemSprite = item.itemSprite;

        itemToSpawn.SetActive(false);
        return itemToSpawn;
    }
    public static void EnforceDictionary()
    {
        Debug.Log("enforcing");
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.GetComponent<PlayerController>().EnforceDictionary();
        
    }
}
