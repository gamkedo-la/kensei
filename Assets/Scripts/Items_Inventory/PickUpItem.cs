using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpItem : MonoBehaviour
{

public CircleCollider2D circle;    
private GameObject player;

public GameObject item;

public Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        ItemClass icScript = gameObject.GetComponent<ItemClass>();


        if(icScript == null)
        {
            Debug.LogWarning(gameObject.name+ " did not have item class");
        }
        else if(GameDictionary.choiceDictionary.ContainsKey(icScript.itemName) == false)
        {
            Debug.LogWarning(icScript.itemName+ " did not match dictionary");
        }
        else if(GameDictionary.choiceDictionary[icScript.itemName])
        {
            Debug.Log(icScript.itemName + " has been picked up");
            Debug.Log(GameDictionary.choiceDictionary[icScript.itemName]);
            Destroy(gameObject);
        }
        else if(GameDictionary.choiceDictionary["Loading Game"] && PlayerPrefs.HasKey(scene.name + "_" + this.gameObject.GetComponent<ItemClass>().itemName + ".x"))
        {
            Debug.Log(icScript.itemName + " has been picked up and dropped");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D( Collider2D col )
    {
        if(col.CompareTag("Player"))
        {
            player = col.gameObject;
            col.GetComponent<PlayerController>().targetItem = item;

        }
    }

    void OnTriggerExit2D( Collider2D col )
    {   
        if(col.CompareTag("Player"))
        {
        player = col.gameObject;
        col.GetComponent<PlayerController>().targetItem = null;

        }
    }
    public void OnPickUp()
    {
        item.SetActive(false);

    }
}
