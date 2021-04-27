﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        dictionaryList = new List<string>(){"Nude", "Base Kimono", "Base Katana", "Paddle", "One Arm", "Sake Bottle", "Rusted Key", "Ginkgo Seed", "Farmers Clothes", "Bloody Tanto Blade", "Given Katana", "Given Sake Bottle", "Monk Robes", "Mysterious Clothing", "Naginata", "Shinzo Fragment", "Broken Haniwa", "Game Saved", "Dull Katana", "Monk Path", "Ronin Path", "Samurai Path", "Spoke to Sakichi", "Path Chosen", "Spoke to Takuan", "Ancient Chokuto", "Onigawara Fragment", "Message Delivered", "Sasaki Returned", "Player Leads", "Shigenari Leads", "With Takuan", "Shigenari Dead", "Gave Naginata", "Sake Cup", "Bladeless Tanto", "Blooded Tanto", "Daimyo Armor"};

        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        Populate();

        foreach(KeyValuePair<string,bool> pair in GameDictionary.choiceDictionary)
        {
            if(PlayerPrefs.HasKey(pair.Key))
            {
                int switchInt;
                switchInt = PlayerPrefs.GetInt(pair.Key);
                switch(switchInt)
                {
                    case 0:
                        GameDictionary.Instance.UpdateEntry(pair.Key,false);
                        break;

                    case 1:
                        GameDictionary.Instance.UpdateEntry(pair.Key,true);
                        break;
                }
            }
        }
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

