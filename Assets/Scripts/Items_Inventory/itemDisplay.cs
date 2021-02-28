using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemDisplay : MonoBehaviour
{
    public int slot;
    public GameObject item;

    public Sprite defaultSprite;
    public GameObject player;
    public Image image;
    Color c;

    void Update()
    {
      switch(slot)
      {
        case 0: 
          item = player.GetComponent<PlayerController>().weapon;
          break;

        case 1:
          item = player.GetComponent<PlayerController>().clothing;
          break;

        case 2: 
          item = player.GetComponent<PlayerController>().bigItem;
          break;

        case 3:
          item = player.GetComponent<PlayerController>().smallItem;
          break;
      }

      if(!item) 
      {
        image.sprite = defaultSprite;
        c = image.color;
        c.a = 1f;
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
