using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
private static ItemDictionary _instance;
    public static Dictionary<string, ItemClass> itemDictionary = new Dictionary<string, ItemClass>();
    public static ItemDictionary Instance 
    { 
        get { return _instance; }   
    } 
    private void Awake() 
    { 
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        Populate();
    } 

    public void Populate()
    {
        //populate dictionary with defaults at start of the game
        AddEntry("Weapon", null);
        AddEntry("Clothing", null);
        AddEntry("Small Item", null);
        AddEntry("Big Item", null);
    }
    public void AddEntry(string key, ItemClass value)   
    {
        if(!ItemDictionary.itemDictionary.ContainsKey(key))
        {
        ItemDictionary.itemDictionary.Add(key, value);
        }
        else{Debug.Log("Key match");}
    }
    public void UpdateEntry(string key, ItemClass value)
    {
        if(ItemDictionary.itemDictionary.ContainsKey(key))
        {
        ItemDictionary.itemDictionary[key] = value;
        }
        else{Debug.Log("No key match");}
    }
}
