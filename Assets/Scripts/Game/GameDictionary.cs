using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDictionary : MonoBehaviour
{ 
    private static GameDictionary _instance;
    public static Dictionary<string, bool> choiceDictionary = new Dictionary<string, bool>();
    public static GameDictionary Instance 
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
        //Add new items or conditions here!

        AddEntry("Nude", true);
        AddEntry("Base Kimono", false);
        AddEntry("Base Katana", false);
        AddEntry("Paddle", false);
        AddEntry("One Arm", false);
        AddEntry("Sake Bottle", false);
        AddEntry("Rusted Key", false);
        AddEntry("Ginkgo Seed", false);
        AddEntry("Farmers Clothes", false);
        AddEntry("Bloody Tanto Blade", false);
        AddEntry("Given Katana", false);
        AddEntry("Given Sake Bottle", false);
        AddEntry("Monk Robes", false);
        AddEntry("Worn Kimono", false);
        AddEntry("Naginata", false);
        AddEntry("Shinzo Fragment", false);
        AddEntry("Broken Haniwa", false);
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

