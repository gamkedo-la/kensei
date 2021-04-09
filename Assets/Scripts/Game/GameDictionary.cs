using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDictionary : MonoBehaviour
{ 
    private static GameDictionary _instance;
    public static Dictionary<string, bool> choiceDictionary = new Dictionary<string, bool>();

    private static List<string> dictionaryList;
    public static GameDictionary Instance 
    { 
        get { return _instance; }   
    } 
    private void Awake() 
    { 
        dictionaryList = new List<string>(){"Nude", "Base Kimono", "Base Katana", "Paddle", "One Arm", "Sake Bottle", "Rusted Key", "Ginkgo Seed", "Farmers Clothes", "Bloody Tanto Blade", "Given Katana", "Given Sake Bottle", "Monk Robes", "Worn Kimono", "Naginata", "Shinzo Fragment", "Broken Haniwa", "Game Saved"};

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
        foreach(string flag in dictionaryList)
        {
            AddEntry(flag, false);
        }
        //Add new items or conditions here!

        UpdateEntry("Nude", true);
        
    }
    public void AddEntry(string key, bool value)   
    {
        if(!GameDictionary.choiceDictionary.ContainsKey(key))
        {
        GameDictionary.choiceDictionary.Add(key, value);
        }
        else{Debug.Log("Key match");}
    }
    public void UpdateEntry(string key, bool value)
    {
        if(GameDictionary.choiceDictionary.ContainsKey(key))
        {
        GameDictionary.choiceDictionary[key] = value;
        }
        else{Debug.Log("No key match");}
    }
}

