using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public RuntimeAnimatorController defaultController;
    public AnimatorOverrideController baseClothingAnimator;
    public AnimatorOverrideController baseClothing1AAnimator;
    public AnimatorOverrideController nude1AAnimator;
    public GameObject Canvas;
    Vector2 movement;
    bool facingUp;
    bool inventoryOpen = false;
    public GameObject targetItem;
    public GameObject weapon;
    public GameObject clothing;
    public GameObject bigItem;
    public GameObject smallItem;
    public GameObject katana;
    public GameObject paddle;
    public int playerCombatPoints = 10;
    GameObject currentWeaponModel;
    int weaponSortType;

    void Start()
    {
     defaultController = GetComponent<Animator>().runtimeAnimatorController;
     Cursor.lockState = CursorLockMode.Confined;

     //check for player pref data, if no data load defaults otherwise load save 
    }

    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyUp("w") && movement.magnitude == 0)
        {
            facingUp = true;
        }
        if(facingUp)
        {
            if(movement.magnitude > 0)
            {
                facingUp = false;
            }
            else { facingUp = true;}
        }
        
        
        if(movement.y > 0 || facingUp == true)
        {
            animator.SetInteger("Direction", 3);
            if(currentWeaponModel) 
            {
                if(weaponSortType == 0) currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 2;
                else currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 4;
            }
        }
        else 
        {
            animator.SetInteger("Direction", 0);
            if(currentWeaponModel) 
            {
                if(weaponSortType == 0) currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 4;
                else currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }

        if( Input.GetButtonDown("Inventory"))
        {
            if(inventoryOpen)
            {
            Canvas.SetActive(false);
            inventoryOpen = false; 
            }
            
            else
            {
            Canvas.SetActive(true);
            inventoryOpen = true;
            }
        }

        if( Input.GetButtonDown("PickUpItem"))
        {
            AddItem(targetItem);
        }

        //Handle One Arm
        if(GameDictionary.choiceDictionary["One Arm"])
        {
            if(GameDictionary.choiceDictionary["Nude"]) GetComponent<Animator>().runtimeAnimatorController = nude1AAnimator as RuntimeAnimatorController;

            //choose the right animator -- make switch
            if(GameDictionary.choiceDictionary["Base Kimono"])
            {
            GetComponent<Animator>().runtimeAnimatorController = baseClothing1AAnimator as RuntimeAnimatorController;
            }
        }   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
        animator.SetFloat("Speed", movement.magnitude * moveSpeed * Time.fixedDeltaTime);

    }

    public void AddItem(GameObject targetItem)
    {
        if(targetItem != null)
            {
            
            ItemClass.Slot slot;        
            slot = targetItem.GetComponent<ItemClass>().itemSlot;
            
                if(slot == ItemClass.Slot.Weapon)
                {
                        if(weapon != null)
                        {
                            
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(weapon, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            //flag old item false in dictionary
                            GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                            Debug.Log( GameDictionary.choiceDictionary[newItem.GetComponent<ItemClass>().itemName]);

                            //swap weapon
                            currentWeaponModel.SetActive(false);
                            weapon = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                            //flag new item true in dictionary
                            GameDictionary.Instance.UpdateEntry(weapon.GetComponent<ItemClass>().itemName, true);
                            Debug.Log( GameDictionary.choiceDictionary[weapon.GetComponent<ItemClass>().itemName]);
                        
                        }
                        else 
                        { 
                            weapon = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                            //flag new item true in dictionary
                            GameDictionary.Instance.UpdateEntry(weapon.GetComponent<ItemClass>().itemName, true);
                            Debug.Log( GameDictionary.choiceDictionary[weapon.GetComponent<ItemClass>().itemName]);

                        }
                        
                        //change names to socket location
                        if(GameDictionary.choiceDictionary["Base Katana"])
                        {
                            katana.SetActive(true);
                            currentWeaponModel = katana;
                            weaponSortType = 0;
                        }
                        if(GameDictionary.choiceDictionary["Paddle"])
                        {
                            paddle.SetActive(true);
                            currentWeaponModel = paddle;
                            weaponSortType = 1;
                        }
                }

                if(slot == ItemClass.Slot.Clothing)
                {
                        if(clothing != null)
                        {
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(clothing, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            //flag old item false in dictionary
                            GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                            Debug.Log( GameDictionary.choiceDictionary[newItem.GetComponent<ItemClass>().itemName]);

                            clothing = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                            //flag new item true in dictionary
                            GameDictionary.Instance.UpdateEntry(clothing.GetComponent<ItemClass>().itemName, true);
                            Debug.Log( GameDictionary.choiceDictionary[clothing.GetComponent<ItemClass>().itemName]);
                        }
                        else 
                        { 
                            clothing = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                            GameDictionary.Instance.UpdateEntry("Nude", false);
                            Debug.Log( GameDictionary.choiceDictionary["Nude"]);
                            //flag new item true in dictionary
                            GameDictionary.Instance.UpdateEntry(clothing.GetComponent<ItemClass>().itemName, true);
                            Debug.Log( GameDictionary.choiceDictionary[clothing.GetComponent<ItemClass>().itemName]);
                        }

                        //set proper animator
                        if(GameDictionary.choiceDictionary["Base Kimono"])
                        {
                            GetComponent<Animator>().runtimeAnimatorController = baseClothingAnimator as RuntimeAnimatorController;
                        }
                }
                if(slot == ItemClass.Slot.BigItem)
                {
                        if(bigItem != null)
                        {
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(bigItem, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            //flag old item false in dictionary
                            GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                            Debug.Log( GameDictionary.choiceDictionary[newItem.GetComponent<ItemClass>().itemName]);

                            bigItem = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                            //flag new item true in dictionary
                            GameDictionary.Instance.UpdateEntry(bigItem.GetComponent<ItemClass>().itemName, true);
                            Debug.Log( GameDictionary.choiceDictionary[bigItem.GetComponent<ItemClass>().itemName]);

                        }
                        else 
                        { 
                            bigItem = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                            //flag new item true in dictionary
                            GameDictionary.Instance.UpdateEntry(bigItem.GetComponent<ItemClass>().itemName, true);
                            Debug.Log( GameDictionary.choiceDictionary[bigItem.GetComponent<ItemClass>().itemName]);
                        }
                }
                if(slot == ItemClass.Slot.SmallItem)
                {
                        if(smallItem != null)
                        {

                             //spawn old item in Scene
                            GameObject newItem = Instantiate(smallItem, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            //flag old item false in dictionary
                            GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                            Debug.Log( GameDictionary.choiceDictionary[newItem.GetComponent<ItemClass>().itemName]);

                            smallItem = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();  
                            //flag new item true in dictionary
                            GameDictionary.Instance.UpdateEntry(smallItem.GetComponent<ItemClass>().itemName, true);
                            Debug.Log( GameDictionary.choiceDictionary[smallItem.GetComponent<ItemClass>().itemName]);
                        }
                        else 
                        { 
                            smallItem = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                            //flag new item true in dictionary
                            GameDictionary.Instance.UpdateEntry(smallItem.GetComponent<ItemClass>().itemName, true);
                            Debug.Log( GameDictionary.choiceDictionary[smallItem.GetComponent<ItemClass>().itemName]);
                        }
                }
            }      
            else{return;}
    }

    public void DropItem(ItemClass.Slot slot)
    {
                if(slot == ItemClass.Slot.Weapon)
                {
                        if(weapon != null)
                        {
                            
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(weapon, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            currentWeaponModel.SetActive(false);
                            weapon = null;
                            //flag old item false in dictionary 
                            GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                            Debug.Log( GameDictionary.choiceDictionary[newItem.GetComponent<ItemClass>().itemName]);                      
                        }

                        
                }

                if(slot == ItemClass.Slot.Clothing)
                {
                        if(clothing != null)
                        {
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(clothing, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            GameDictionary.Instance.UpdateEntry("Nude", true);
                            Debug.Log( GameDictionary.choiceDictionary["Nude"]);

                            clothing = null;
                            GetComponent<Animator>().runtimeAnimatorController = defaultController;
                            //flag old item false in dictionary
                            GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                            Debug.Log( GameDictionary.choiceDictionary[newItem.GetComponent<ItemClass>().itemName]);
                        }
                }
                if(slot == ItemClass.Slot.BigItem)
                {
                        if(bigItem != null)
                        {
                           GameObject newItem = Instantiate(bigItem, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            bigItem = null; 
                            //flag old item false in dictionary
                            GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                            Debug.Log( GameDictionary.choiceDictionary[newItem.GetComponent<ItemClass>().itemName]);
                        }
                       
                }
                if(slot == ItemClass.Slot.SmallItem)
                {
                        if(smallItem != null)
                        {
                            GameObject newItem = Instantiate(smallItem, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            smallItem = null;
                            //flag old item false in dictionary
                            GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                            Debug.Log( GameDictionary.choiceDictionary[newItem.GetComponent<ItemClass>().itemName]);
                        }
                        
                }
    }
}
