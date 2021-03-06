using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SaveFile : MonoBehaviour
{
    public GameObject player;
    public void SaveGame()
    {

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (!player) return;

        else
        {
            PlayerPrefs.SetFloat("PlayerPosition.x", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosition.y", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosition.z", player.transform.position.z);

            ItemDictionary.Instance.UpdateEntry( "Weapon", player.GetComponent<PlayerController>().weapon.GetComponent<ItemClass>());
            ItemDictionary.Instance.UpdateEntry( "Clothing", player.GetComponent<PlayerController>().clothing.GetComponent<ItemClass>());
            ItemDictionary.Instance.UpdateEntry( "Small Item", player.GetComponent<PlayerController>().smallItem.GetComponent<ItemClass>());
            ItemDictionary.Instance.UpdateEntry( "Big Item", player.GetComponent<PlayerController>().bigItem.GetComponent<ItemClass>());

        }
    }

}
