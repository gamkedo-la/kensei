﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFile : MonoBehaviour
{
    public GameObject player;
    private GameObject itemToSpawn;
    // Start is called before the first frame update
    public void LoadGame()
    {
        Vector3 position = new Vector3(PlayerPrefs.GetFloat("PlayerPosition.x"), PlayerPrefs.GetFloat("PlayerPosition.y"), PlayerPrefs.GetFloat("PlayerPosition.z"));
        //spawn player at location
        GameObject.Instantiate(player, position, Quaternion.EulerRotation(0,0,0));

        //iterate through item dictionary and load variables using 'add item' function
        if(ItemDictionary.itemDictionary["Weapon"]) player.GetComponent<PlayerController>().AddItem(ConstructItem(ItemDictionary.itemDictionary["Weapon"]));
        if(ItemDictionary.itemDictionary["Clothing"]) player.GetComponent<PlayerController>().AddItem(ConstructItem(ItemDictionary.itemDictionary["Clothing"]));
        if(ItemDictionary.itemDictionary["Small Item"]) player.GetComponent<PlayerController>().AddItem(ConstructItem(ItemDictionary.itemDictionary["Small Item"]));
        if(ItemDictionary.itemDictionary["Big Item"]) player.GetComponent<PlayerController>().AddItem(ConstructItem(ItemDictionary.itemDictionary["Big Item"]));

        foreach(KeyValuePair<string, bool> dictionaryEntry in GameDictionary.choiceDictionary)
        {
            if(PlayerPrefs.HasKey(dictionaryEntry.Key))
            {
                switch(PlayerPrefs.GetInt(dictionaryEntry.Key))
                {
                    case 0:
                        GameDictionary.Instance.UpdateEntry(dictionaryEntry.Key, false);
                        break;

                    case 1:
                        GameDictionary.Instance.UpdateEntry(dictionaryEntry.Key, true);
                        break;
                }   
            }   
        }
   }

    public GameObject ConstructItem(ItemClass item)
    {
        itemToSpawn = new GameObject(item.itemName);

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
}