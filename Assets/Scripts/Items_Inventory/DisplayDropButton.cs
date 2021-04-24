using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayDropButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject drop;
    public GameObject player;
    public ItemClass.Slot slot;
    bool droppable = false;
    public GameObject itemInfoPanel;

    public GameObject itemNameText;
    public GameObject itemDescriptionText;
    public GameObject itemCombatScore;

    public void Update()
    {

    }

    public void OnPointerEnter( PointerEventData eventData)
    {
    drop.SetActive(true);
    droppable = true;

        switch(slot)
        {
            case ItemClass.Slot.Weapon:
                if(player.GetComponent<PlayerController>().weapon)
                {
                    itemInfoPanel.SetActive(true);
                    itemNameText.GetComponent<Text>().text = player.GetComponent<PlayerController>().weapon.GetComponent<ItemClass>().name;
                    itemDescriptionText.GetComponent<Text>().text = player.GetComponent<PlayerController>().weapon.GetComponent<ItemClass>().itemDescription;
                    itemCombatScore.GetComponent<Text>().text = player.GetComponent<PlayerController>().weapon.GetComponent<ItemClass>().combatPoints.ToString();
                }
                break;

            case ItemClass.Slot.Clothing:
                if(player.GetComponent<PlayerController>().clothing)
                {
                    itemInfoPanel.SetActive(true);
                    itemNameText.GetComponent<Text>().text = player.GetComponent<PlayerController>().clothing.GetComponent<ItemClass>().name;
                    itemDescriptionText.GetComponent<Text>().text = player.GetComponent<PlayerController>().clothing.GetComponent<ItemClass>().itemDescription;
                    itemCombatScore.GetComponent<Text>().text = player.GetComponent<PlayerController>().clothing.GetComponent<ItemClass>().combatPoints.ToString();
                }
                break;

            case ItemClass.Slot.BigItem:
                if(player.GetComponent<PlayerController>().bigItem)
                {
                    itemInfoPanel.SetActive(true);
                    itemNameText.GetComponent<Text>().text = player.GetComponent<PlayerController>().bigItem.GetComponent<ItemClass>().name;
                    itemDescriptionText.GetComponent<Text>().text = player.GetComponent<PlayerController>().bigItem.GetComponent<ItemClass>().itemDescription;
                    itemCombatScore.GetComponent<Text>().text = player.GetComponent<PlayerController>().bigItem.GetComponent<ItemClass>().combatPoints.ToString();
                }
                break;

            case ItemClass.Slot.SmallItem:
                if(player.GetComponent<PlayerController>().smallItem)
                {
                    itemInfoPanel.SetActive(true);
                    itemNameText.GetComponent<Text>().text = player.GetComponent<PlayerController>().smallItem.GetComponent<ItemClass>().name;
                    itemDescriptionText.GetComponent<Text>().text = player.GetComponent<PlayerController>().smallItem.GetComponent<ItemClass>().itemDescription;
                    itemCombatScore.GetComponent<Text>().text = player.GetComponent<PlayerController>().smallItem.GetComponent<ItemClass>().combatPoints.ToString();
                }
                break;
        }
    }

    public void OnPointerExit( PointerEventData eventData)
    {
        drop.SetActive(false);
        droppable = false;
        itemInfoPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //drop item and remove from inventory
        player.GetComponent<PlayerController>().DropItem(slot);
    }
}
