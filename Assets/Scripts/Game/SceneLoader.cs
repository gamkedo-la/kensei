using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneLoader
{
    public enum Scene{
        TheVillage, ForestLevel, Stronghold
    }
    // Start is called before the first frame update
    public static void Load(String scene)
    {    
    SceneManager.LoadScene(scene);
    Debug.Log("loaded");
    }


}
