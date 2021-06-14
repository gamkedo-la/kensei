using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneLoader
{
    public enum Scene{
        TheVillage, ForestLevel, Stronghold, None
    }
    // Start is called before the first frame update
    public static void Load(String scene)
    {    
        Debug.Log("SceneLoader is loading: " + scene);
        SceneManager.LoadScene(scene);
        Debug.Log("SceneLoader finished loading: " + scene);
        // now remember this so we don't loop endlessly
        Debug.Log("Writing current scene to savegame: " +scene);
        PlayerPrefs.SetString("Scene",scene);
    }


}
