using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass : MonoBehaviour
{
    
    public enum Slot{Weapon,Clothing,BigItem,SmallItem};
    
    public string itemName;
    public Slot itemSlot;
    public int combatPoints;

    public Sprite itemSprite;

}