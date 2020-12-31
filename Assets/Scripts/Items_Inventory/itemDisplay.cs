using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemDisplay : MonoBehaviour
{
    public int slot;
    public GameObject item;
    public GameObject player;
    public Image image;
    Color c;
    void Update()
    {
    
    if(slot == 0)
    {
      item = player.GetComponent<PlayerController>().weapon;  
    }

    if(slot == 1)
    {
      item = player.GetComponent<PlayerController>().clothing;  
    }

    if(slot == 2)
    {
      item = player.GetComponent<PlayerController>().bigItem;  
    }

    if(slot == 3)
    {
      item = player.GetComponent<PlayerController>().smallItem;  
    }
    
    if(!item) 
    {
    image.sprite = null;
    c = image.color;
    c.a = 0f;
    image.color = c;
    }
    else
    { 
    image.sprite = item.GetComponent<ItemClass>().itemSprite;
    c = image.color;
    c.a = 1f;
    image.color = c;
    }

    }


}
