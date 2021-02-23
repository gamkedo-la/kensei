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
        //refactor???? Using array from config???????
        if (!GameDictionary.choiceDictionary.ContainsKey("Nude")) 
        {
            GameDictionary.choiceDictionary.Add("Nude", true);
        }

        if (!GameDictionary.choiceDictionary.ContainsKey("Base Kimono")) 
        {
            GameDictionary.choiceDictionary.Add("Base Kimono", false);
        }

        if (!GameDictionary.choiceDictionary.ContainsKey("Base Katana")) 
        {
            GameDictionary.choiceDictionary.Add("Base Katana", false);
        }

        if (!GameDictionary.choiceDictionary.ContainsKey("Paddle")) 
        {
            GameDictionary.choiceDictionary.Add("Paddle", false);
        }

        if (!GameDictionary.choiceDictionary.ContainsKey("One Arm")) 
        {
            GameDictionary.choiceDictionary.Add("One Arm", false);
        }

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

