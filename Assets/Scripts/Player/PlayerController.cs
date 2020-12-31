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
    public bool oneArm = false;

   GameObject currentWeaponModel;
   int weaponSortType;

    void Start()
    {
     defaultController = GetComponent<Animator>().runtimeAnimatorController;
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
                if(weaponSortType == 0) currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 1;
                else currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
        }
        else 
        {
            animator.SetInteger("Direction", 0);
            if(currentWeaponModel) 
            {
                if(weaponSortType == 0) currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 3;
                else currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        }

        if( Input.GetButtonDown("Inventory"))
        {
            if(inventoryOpen)
            {
            Debug.Log("CloseInventory");
            Canvas.SetActive(false);
            inventoryOpen = false; 
            }
            else
            {
            Debug.Log("Inventory");
            Canvas.SetActive(true);
            inventoryOpen = true;
            }
        }

        if( Input.GetButtonDown("PickUpItem"))
        {
            Debug.Log("pickupattempt");
            AddItem();
        }

        if(GetComponent<StateTracker>().oneArm) oneArm = true;
        if(oneArm)
        {
            if(!clothing) GetComponent<Animator>().runtimeAnimatorController = nude1AAnimator as RuntimeAnimatorController;
            if(clothing.tag == "BaseClothing")
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

    void AddItem()
    {
        if(targetItem != null)
            {
                Debug.Log("pickupinitiate");
            int slot;        
            slot = targetItem.GetComponent<ItemClass>().itemSlot;
            
                if(slot == 0)
                {
                        if(weapon != null)
                        {
                            
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(weapon, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);

                            //swap weapon
                            currentWeaponModel.SetActive(false);
                            weapon = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                        
                        }
                        else 
                        { 
                            weapon = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                        }

                        if(weapon.tag == "Katana")
                        {
                            katana.SetActive(true);
                            currentWeaponModel = katana;
                            weaponSortType = 0;
                        }
                        if(weapon.tag == "Paddle")
                        {
                            paddle.SetActive(true);
                            currentWeaponModel = paddle;
                            weaponSortType = 1;
                        }
                }

                if(slot == 1)
                {
                        if(clothing != null)
                        {
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(clothing, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            //swap weapon

                            clothing = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();                        }
                        else 
                        { 
                            clothing = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                        }

                        if(clothing.tag == "BaseClothing")
                        {
                            GetComponent<Animator>().runtimeAnimatorController = baseClothingAnimator as RuntimeAnimatorController;
                        }
                }
                if(slot == 2)
                {
                        if(bigItem != null)
                        {
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(bigItem, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            //swap weapon

                            bigItem = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                        }
                        else 
                        { 
                            bigItem = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                        }
                }
                if(slot == 3)
                {
                        if(smallItem != null)
                        {

                             //spawn old item in Scene
                            GameObject newItem = Instantiate(smallItem, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            //swap weapon

                            smallItem = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();  

                        }
                        else 
                        { 
                            smallItem = targetItem;
                            targetItem.GetComponent<PickUpItem>().OnPickUp();
                        }
                }
            }      
            else{return;}
    }

    public void DropItem(int slot)
    {
                if(slot == 0)
                {
                        if(weapon != null)
                        {
                            
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(weapon, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            currentWeaponModel.SetActive(false);
                            weapon = null;                        
                        }

                        
                }

                if(slot == 1)
                {
                        if(clothing != null)
                        {
                            //spawn old item in Scene
                            GameObject newItem = Instantiate(clothing, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            clothing = null;
                            GetComponent<Animator>().runtimeAnimatorController = defaultController;
                        }
                }
                if(slot == 2)
                {
                        if(bigItem != null)
                        {
                           GameObject newItem = Instantiate(bigItem, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            bigItem = null; 
                        }
                       
                }
                if(slot == 3)
                {
                        if(smallItem != null)
                        {
                            GameObject newItem = Instantiate(smallItem, new Vector2(rb.position.x,rb.position.y-4f), Quaternion.identity); 
                            newItem.SetActive(true);
                            smallItem = null;
                        }
                        
                }
    }
}
