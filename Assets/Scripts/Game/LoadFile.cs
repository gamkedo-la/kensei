using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class LoadFile
{
    static List<string> dictionaryList;
    public static Scene scene;
    private static bool debugSkips = true;
    private static GameObject[] objArray;
    public static void LoadGame()
    {
        Debug.Log("LOADING SAVE DATA...");

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (!player)
        {
            Debug.Log("ERROR: LoadGame did not find a player!");
            return;
        }

        Debug.Log("LOADING DICTIONARY DATA...");
        dictionaryList = new List<string>();
        foreach (KeyValuePair<string, bool> pair in GameDictionary.choiceDictionary)
        {
            dictionaryList.Add(pair.Key);
            //GameDictionary.choiceDictionary[dictionaryEntry.Key] = false;   
        }
        foreach (string entry in dictionaryList)
        {
            GameDictionary.Instance.UpdateEntry(entry, PlayerPrefs.GetInt(entry, 0) == 1);
        }

        Debug.Log("SPAWNING RESOURCES...");
        objArray = Resources.LoadAll("", typeof(GameObject)).Cast<GameObject>().ToArray();
        foreach (GameObject obj in objArray)
        {
            if (obj != null)
            {
                if (PlayerPrefs.HasKey(scene.name + "_" + obj.GetComponent<ItemClass>().itemName + ".x")
                && PlayerPrefs.HasKey(scene.name + "_" + obj.GetComponent<ItemClass>().itemName + ".y"))
                {
                    player.GetComponent<PlayerController>().SpawnItem(obj);
                    Debug.Log("- Tried to Spawn" + obj.name);
                }
                else
                {
                    Debug.Log(obj.name + " is missing missing position data.");
                }
            }
        }

        Debug.Log("LOADING PLAYER STATS...");
        player.GetComponent<StateTracker>().playerCombatPoints = PlayerPrefs.GetInt("Player Combat Score");
        player.GetComponent<StateTracker>().CalculateNewCombatScore();

        Debug.Log("ENFORCING DICTIONARY...");
        EnforceDictionary();

        // teleport to the correct map (scene)
        Debug.Log("LOADING MAP...");
        string loadme = PlayerPrefs.GetString("Scene");
        if (loadme != null)
        {
            // only if changed
            if (SceneManager.GetActiveScene().name != loadme)
            {
                Debug.Log("- Current scene: " + SceneManager.GetActiveScene().name);
                Debug.Log("- LoadGame is about to load scene: " + loadme);

                if (debugSkips == false)
                {
                    SceneManager.LoadScene(loadme);
                }
                else
                {
                    Debug.LogWarning("DEBUG SKIPS ON IN LOADFILE AND NOT LOADING SCENE");
                }
                Debug.Log("- LoadGame finished loading scene: " + loadme);
            }
            else
            {
                Debug.Log("- We are already in the saved scene: " + loadme + ".");
            }
        }
        else
        {
            Debug.Log("- No scene saved in playerperfs. Nothing to load.");
        }

        float newx = PlayerPrefs.GetFloat("PlayerPosition.x");
        float newy = PlayerPrefs.GetFloat("PlayerPosition.y");
        float newz = PlayerPrefs.GetFloat("PlayerPosition.z");
        Debug.Log("TELEPORTING PLAYER TO " + newx + "," + newy);
        if (debugSkips == false)
        {
            player.transform.position = new Vector3(newx, newy, newz);
        }
        else
        {
            Debug.LogWarning("DEBUG SKIPS ON IN LOADFILE AND NOT LOADING PLAYER AT LOCATION");
        }


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
