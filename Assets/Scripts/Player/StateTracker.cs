using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTracker : MonoBehaviour
{
    public GameObject player;
    public GameObject currentWeapon;
    public int currentWeaponPoints;
    public GameObject currentClothing;
    public int currentClothingPoints;
    public GameObject currentBigItem;
    public int currentBigItemPoints;
    public GameObject currentSmallItem;
    public int currentSmallItemPoints;
    public int playerCombatPoints = 10;
    public int combatScore;
    public bool oneArm = false;
    public bool incrementedCombatPoints = false;

    public void CalculateNewCombatScore()
    {
        oneArm = GameDictionary.choiceDictionary["One Arm"];

        if (!player.GetComponent<PlayerController>().weapon)
        {
            currentWeaponPoints = 0;
            currentWeapon = null;
        }
        else
        {
            currentWeapon = player.GetComponent<PlayerController>().weapon;
            currentWeaponPoints = currentWeapon.GetComponent<ItemClass>().combatPoints;
            Debug.Log(currentWeapon.GetComponent<ItemClass>().itemName + " " + currentWeapon.GetComponent<ItemClass>().combatPoints);
        }

        if (!player.GetComponent<PlayerController>().clothing)
        {
            currentClothingPoints = 0;
            currentClothing = null;
        }
        else
        {
            currentClothing = player.GetComponent<PlayerController>().clothing;
            currentClothingPoints = currentClothing.GetComponent<ItemClass>().combatPoints;
            Debug.Log(currentClothing.GetComponent<ItemClass>().itemName + " " + currentClothing.GetComponent<ItemClass>().combatPoints);
        }

        if (!player.GetComponent<PlayerController>().bigItem) currentBigItemPoints = 0;
        else
        {
            currentBigItem = player.GetComponent<PlayerController>().bigItem;
            currentBigItemPoints = currentBigItem.GetComponent<ItemClass>().combatPoints;
            Debug.Log(currentBigItem.GetComponent<ItemClass>().itemName + " " + currentBigItem.GetComponent<ItemClass>().combatPoints);
        }

        if (!player.GetComponent<PlayerController>().smallItem) currentSmallItemPoints = 0;
        else
        {
            currentSmallItem = player.GetComponent<PlayerController>().smallItem;
            currentSmallItemPoints = currentSmallItem.GetComponent<ItemClass>().combatPoints;
            Debug.Log(currentSmallItem.GetComponent<ItemClass>().itemName + " " + currentSmallItem.GetComponent<ItemClass>().combatPoints);
        }

        if (oneArm) combatScore = (currentWeaponPoints + currentClothingPoints + currentBigItemPoints + currentSmallItemPoints + playerCombatPoints) / 2;
        else combatScore = currentWeaponPoints + currentClothingPoints + currentBigItemPoints + currentSmallItemPoints + playerCombatPoints;
        Debug.Log(combatScore);
    }


}
