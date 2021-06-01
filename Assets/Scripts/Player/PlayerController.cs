using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public RuntimeAnimatorController defaultController;
    public AnimatorOverrideController baseClothingAnimator;
    public AnimatorOverrideController baseClothing1AAnimator;
    public AnimatorOverrideController nude1AAnimator;
    public AnimatorOverrideController farmersClothesAnimator;
    public AnimatorOverrideController farmersClothes1AAnimator;

    public AnimatorOverrideController wornKimonoAnimator;
    public AnimatorOverrideController wornKimono1AAnimator;
    public AnimatorOverrideController monkClothesAnimator;
    public AnimatorOverrideController monkClothes1AAnimator;
    public AnimatorOverrideController daimyoArmorAnimator;
    public AnimatorOverrideController daimyoArmorAnimator1A;
    public AnimatorOverrideController monkRakusu;
    public AnimatorOverrideController monkRakusu1A;
    public AnimatorOverrideController roninArmor;
    public AnimatorOverrideController roninArmor1A;
    public AnimatorOverrideController senseiKashimono;
    public AnimatorOverrideController senseiKashimono1A;

    public GameObject Canvas;
    Vector2 movement;
    public bool facingUp;
    bool inventoryOpen = false;
    public GameObject targetItem;
    public GameObject weapon;
    public GameObject clothing;
    public GameObject bigItem;
    public GameObject smallItem;
    public GameObject katana;
    public GameObject paddle;
    public GameObject naginata;
    public GameObject dullKatana;
    public GameObject ancientChokuto;
    public GameObject oldTachi;
    GameObject currentWeaponModel;
    int weaponSortType;
    public GameObject pickUpItemButton;

    public GameObject defaultClothing;
    public Transform[] teleportList;
    public bool movementLocked;
    public GameObject bloodedTanto;

    Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name == "TheVillage" && !GameDictionary.choiceDictionary["Game Saved"])
        {
            AddItem(defaultClothing);
        }

        SaveFile.SaveGame();
        defaultController = GetComponent<Animator>().runtimeAnimatorController;
        Cursor.lockState = CursorLockMode.Confined;
        LoadFile.LoadGame();
        ChooseAnimator();
    }

    void Update()
    {
        //input
        if (!movementLocked)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyUp("w") && movement.magnitude == 0)
        {
            facingUp = true;
        }
        if (facingUp)
        {
            if (movement.magnitude > 0)
            {
                facingUp = false;
            }
            else { facingUp = true; }
        }


        if (movement.y > 0 || facingUp == true)
        {
            animator.SetInteger("Direction", 3);
            if (currentWeaponModel)
            {
                if (weaponSortType == 0) currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 2;
                else currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 4;
            }
        }
        else
        {
            animator.SetInteger("Direction", 0);
            if (currentWeaponModel)
            {
                if (weaponSortType == 0) currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 4;
                else currentWeaponModel.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }

        if (Input.GetButtonDown("PickUpItem"))
        {
            AddItem(targetItem);
        }

        if (targetItem)
        {
            pickUpItemButton.SetActive(true);
        }
        else { pickUpItemButton.SetActive(false); }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {

            Debug.Log(PlayerPrefs.GetInt("Base Kimono"));
        }

        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Input");
            GameObject newGO = Instantiate(Resources.Load("Bloody Tanto Blade") as GameObject);
            Debug.Log(newGO != null);
            newGO.SetActive(true);
            newGO.transform.position = transform.position + Vector3.up * 2f;
        }
        */
        //Remove for release
        if (Input.GetKeyDown(KeyCode.Alpha1) && teleportList[0] != null)
        {
            transform.position = teleportList[0].position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && teleportList[1] != null)
        {
            transform.position = teleportList[1].position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && teleportList[2] != null)
        {
            transform.position = teleportList[2].position;
        }

        if (GameDictionary.choiceDictionary["Bloody Tanto Blade"] && GameDictionary.choiceDictionary["Bladeless Tanto"])
        {
            GameDictionary.Instance.UpdateEntry("Bloody Tanto Blade", false);
            GameDictionary.Instance.UpdateEntry("Bladeless Tanto", false);
            GameDictionary.Instance.UpdateEntry("Blooded Tanto", false);
            bigItem = null;
            smallItem = null;
            AddItem(bloodedTanto);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        if (!movementLocked)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            animator.SetFloat("Speed", movement.magnitude * moveSpeed * Time.fixedDeltaTime);
        }
        else { animator.SetFloat("Speed", 0); }

    }

    public void AddItem(GameObject targetItem)
    {
        if (targetItem == null)
        {
            Collider2D[] itemsNear = Physics2D.OverlapCircleAll(transform.position, 5f);

            for (int i = 0; i < itemsNear.Length; i++)
            {
                if (itemsNear[i].GetComponent<ItemClass>() != null)
                {
                    targetItem = itemsNear[i].gameObject;
                    break;
                }
            }
        }
        if (targetItem != null)
        {

            ItemClass.Slot slot;
            slot = targetItem.GetComponent<ItemClass>().itemSlot;

            if (slot == ItemClass.Slot.Weapon)
            {
                if (weapon != null)
                {

                    //spawn old item in Scene
                    GameObject newItem = Instantiate(weapon, new Vector2(rb.position.x, rb.position.y - 4f), Quaternion.identity);
                    newItem.SetActive(true);
                    //flag old item false in dictionary
                    GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                    SaveFile.StoreItemLocation(newItem);

                    //swap weapon
                    currentWeaponModel.SetActive(false);
                    weapon = targetItem;
                    targetItem.GetComponent<PickUpItem>().OnPickUp();
                    //flag new item true in dictionary
                    GameDictionary.Instance.UpdateEntry(weapon.GetComponent<ItemClass>().itemName, true);
                    SaveFile.ClearItemLocation(weapon);


                }
                else
                {
                    weapon = targetItem;
                    Debug.Log(weapon.name);
                    targetItem.GetComponent<PickUpItem>().OnPickUp();
                    //flag new item true in dictionary
                    GameDictionary.Instance.UpdateEntry(weapon.GetComponent<ItemClass>().itemName, true);
                    Debug.Log(GameDictionary.choiceDictionary[weapon.GetComponent<ItemClass>().itemName]);
                    SaveFile.ClearItemLocation(weapon);
                }

                //change names to socket location
                if (GameDictionary.choiceDictionary["Base Katana"])
                {
                    katana.SetActive(true);
                    currentWeaponModel = katana;
                    weaponSortType = 0;
                }

                if (GameDictionary.choiceDictionary["Paddle"])
                {
                    paddle.SetActive(true);
                    currentWeaponModel = paddle;
                    weaponSortType = 1;
                }

                if (GameDictionary.choiceDictionary["Naginata"])
                {
                    naginata.SetActive(true);
                    currentWeaponModel = naginata;
                    weaponSortType = 1;
                }

                if (GameDictionary.choiceDictionary["Dull Katana"])
                {
                    dullKatana.SetActive(true);
                    currentWeaponModel = dullKatana;
                    weaponSortType = 0;
                }

                if (GameDictionary.choiceDictionary["Ancient Chokuto"])
                {
                    ancientChokuto.SetActive(true);
                    currentWeaponModel = ancientChokuto;
                    weaponSortType = 0;
                }

                if (GameDictionary.choiceDictionary["Old Tachi"])
                {
                    oldTachi.SetActive(true);
                    currentWeaponModel = oldTachi;
                    weaponSortType = 1;
                }
            }

            if (slot == ItemClass.Slot.Clothing)
            {

                if (clothing != null)
                {
                    //spawn old item in Scene
                    GameObject newItem = Instantiate(clothing, new Vector2(rb.position.x, rb.position.y - 4f), Quaternion.identity);
                    newItem.SetActive(true);
                    //flag old item false in dictionary
                    GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                    SaveFile.StoreItemLocation(newItem);

                    clothing = targetItem;
                    targetItem.GetComponent<PickUpItem>().OnPickUp();
                    //flag new item true in dictionary
                    GameDictionary.Instance.UpdateEntry(clothing.GetComponent<ItemClass>().itemName, true);
                    SaveFile.ClearItemLocation(clothing);

                }
                else
                {
                    clothing = targetItem;
                    targetItem.GetComponent<PickUpItem>().OnPickUp();
                    GameDictionary.Instance.UpdateEntry("Nude", false);

                    //flag new item true in dictionary
                    GameDictionary.Instance.UpdateEntry(clothing.GetComponent<ItemClass>().itemName, true);
                    SaveFile.ClearItemLocation(clothing);
                }

                //set proper animator
                ChooseAnimator();

            }
            if (slot == ItemClass.Slot.BigItem)
            {
                if (bigItem != null)
                {
                    //spawn old item in Scene
                    GameObject newItem = Instantiate(bigItem, new Vector2(rb.position.x, rb.position.y - 4f), Quaternion.identity);
                    newItem.SetActive(true);
                    //flag old item false in dictionary
                    GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                    SaveFile.StoreItemLocation(newItem);

                    bigItem = targetItem;
                    targetItem.GetComponent<PickUpItem>().OnPickUp();
                    //flag new item true in dictionary
                    GameDictionary.Instance.UpdateEntry(bigItem.GetComponent<ItemClass>().itemName, true);
                    SaveFile.ClearItemLocation(bigItem);

                }
                else
                {
                    bigItem = targetItem;
                    targetItem.GetComponent<PickUpItem>().OnPickUp();
                    //flag new item true in dictionary
                    GameDictionary.Instance.UpdateEntry(bigItem.GetComponent<ItemClass>().itemName, true);
                    SaveFile.ClearItemLocation(bigItem);
                }
            }
            if (slot == ItemClass.Slot.SmallItem)
            {
                if (smallItem != null)
                {

                    //spawn old item in Scene
                    GameObject newItem = Instantiate(smallItem, new Vector2(rb.position.x, rb.position.y - 4f), Quaternion.identity);
                    newItem.SetActive(true);
                    //flag old item false in dictionary
                    GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                    SaveFile.StoreItemLocation(newItem);

                    smallItem = targetItem;
                    targetItem.GetComponent<PickUpItem>().OnPickUp();
                    //flag new item true in dictionary
                    GameDictionary.Instance.UpdateEntry(smallItem.GetComponent<ItemClass>().itemName, true);
                    SaveFile.ClearItemLocation(smallItem);
                }
                else
                {
                    smallItem = targetItem;
                    targetItem.GetComponent<PickUpItem>().OnPickUp();
                    //flag new item true in dictionary
                    GameDictionary.Instance.UpdateEntry(smallItem.GetComponent<ItemClass>().itemName, true);
                    SaveFile.ClearItemLocation(smallItem);
                }
            }
        }
        else { return; }
    }

    public void DropItem(ItemClass.Slot slot)
    {


        if (slot == ItemClass.Slot.Weapon)
        {
            if (weapon != null)
            {

                //spawn old item in Scene
                GameObject newItem = Instantiate(weapon, new Vector2(rb.position.x, rb.position.y - 4f), Quaternion.identity);
                newItem.SetActive(true);
                currentWeaponModel.SetActive(false);
                weapon = null;
                //flag old item false in dictionary 
                GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                SaveFile.StoreItemLocation(newItem);

            }
        }

        if (slot == ItemClass.Slot.Clothing)
        {
            if (clothing != null)
            {
                //spawn old item in Scene
                GameObject newItem = Instantiate(clothing, new Vector2(rb.position.x, rb.position.y - 4f), Quaternion.identity);
                newItem.SetActive(true);
                GameDictionary.Instance.UpdateEntry("Nude", true);

                clothing = null;
                GetComponent<Animator>().runtimeAnimatorController = defaultController;
                //flag old item false in dictionary
                GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                SaveFile.StoreItemLocation(newItem);

            }

            ChooseAnimator();
        }
        if (slot == ItemClass.Slot.BigItem)
        {
            if (bigItem != null)
            {
                GameObject newItem = Instantiate(bigItem, new Vector2(rb.position.x, rb.position.y - 4f), Quaternion.identity);
                newItem.SetActive(true);
                bigItem = null;
                //flag old item false in dictionary
                GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                SaveFile.StoreItemLocation(newItem);

            }

        }
        if (slot == ItemClass.Slot.SmallItem)
        {
            if (smallItem != null)
            {
                GameObject newItem = Instantiate(smallItem, new Vector2(rb.position.x, rb.position.y - 4f), Quaternion.identity);
                newItem.SetActive(true);
                smallItem = null;
                //flag old item false in dictionary
                GameDictionary.Instance.UpdateEntry(newItem.GetComponent<ItemClass>().itemName, false);
                SaveFile.StoreItemLocation(newItem);

            }

        }
    }
    public void CheckForItems()
    {
        Collider2D[] itemsNear = Physics2D.OverlapCircleAll(transform.position, 5f);

        for (int i = 0; i < itemsNear.Length; i++)
        {
            if (itemsNear[i].GetComponent<ItemClass>() != null)
            {
                targetItem = itemsNear[i].gameObject;
                break;
            }
            else { targetItem = null; }
        }
    }
    public void ChooseAnimator()
    {

        if (GameDictionary.choiceDictionary["One Arm"])
        {
            if (GameDictionary.choiceDictionary["Nude"]) GetComponent<Animator>().runtimeAnimatorController = nude1AAnimator as RuntimeAnimatorController;

            if (GameDictionary.choiceDictionary["Base Kimono"])
            {
                GetComponent<Animator>().runtimeAnimatorController = baseClothing1AAnimator as RuntimeAnimatorController;
            }

            if (GameDictionary.choiceDictionary["Farmers Clothes"])
            {
                GetComponent<Animator>().runtimeAnimatorController = farmersClothes1AAnimator as RuntimeAnimatorController;
            }

            if (GameDictionary.choiceDictionary["Monk Robes"])
            {
                GetComponent<Animator>().runtimeAnimatorController = monkClothes1AAnimator as RuntimeAnimatorController;
            }

            if (GameDictionary.choiceDictionary["Mysterious Clothing"])
            {
                GetComponent<Animator>().runtimeAnimatorController = wornKimono1AAnimator as RuntimeAnimatorController;
            }
            if (GameDictionary.choiceDictionary["Daimyo Armor"])
            {
                GetComponent<Animator>().runtimeAnimatorController = daimyoArmorAnimator1A as RuntimeAnimatorController;
            }
            if (GameDictionary.choiceDictionary["Monk Rakusu"])
            {
                GetComponent<Animator>().runtimeAnimatorController = monkRakusu1A as RuntimeAnimatorController;
            }
            if (GameDictionary.choiceDictionary["Ronin Armor"])
            {
                GetComponent<Animator>().runtimeAnimatorController = roninArmor1A as RuntimeAnimatorController;
            }
            if (GameDictionary.choiceDictionary["Sensei Kashimono"])
            {
                GetComponent<Animator>().runtimeAnimatorController = senseiKashimono1A as RuntimeAnimatorController;
            }
        }
        else
        {
            if (GameDictionary.choiceDictionary["Nude"]) GetComponent<Animator>().runtimeAnimatorController = defaultController as RuntimeAnimatorController;

            if (GameDictionary.choiceDictionary["Base Kimono"])
            {
                GetComponent<Animator>().runtimeAnimatorController = baseClothingAnimator as RuntimeAnimatorController;
            }

            if (GameDictionary.choiceDictionary["Farmers Clothes"])
            {
                GetComponent<Animator>().runtimeAnimatorController = farmersClothesAnimator as RuntimeAnimatorController;
            }

            if (GameDictionary.choiceDictionary["Monk Robes"])
            {
                GetComponent<Animator>().runtimeAnimatorController = monkClothesAnimator as RuntimeAnimatorController;
            }

            if (GameDictionary.choiceDictionary["Mysterious Clothing"])
            {
                GetComponent<Animator>().runtimeAnimatorController = wornKimonoAnimator as RuntimeAnimatorController;
            }
            if (GameDictionary.choiceDictionary["Daimyo Armor"])
            {
                GetComponent<Animator>().runtimeAnimatorController = daimyoArmorAnimator as RuntimeAnimatorController;
            }
            if (GameDictionary.choiceDictionary["Monk Rakusu"])
            {
                GetComponent<Animator>().runtimeAnimatorController = monkRakusu as RuntimeAnimatorController;
            }
            if (GameDictionary.choiceDictionary["Ronin Armor"])
            {
                GetComponent<Animator>().runtimeAnimatorController = roninArmor as RuntimeAnimatorController;
            }
            if (GameDictionary.choiceDictionary["Sensei Kashimono"])
            {
                GetComponent<Animator>().runtimeAnimatorController = senseiKashimono as RuntimeAnimatorController;
            }
        }
    }

    public void SimpleAddItem()
    {
        AddItem(targetItem);
    }

    public void EnforceDictionary()
    {
        //GameObject[] objArray = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        //Debug.Log(objArray.Length);

        foreach (string entry in GameDictionary.dictionaryList)
        {
            if (GameDictionary.choiceDictionary[entry])
            {
                GameObject obj = Resources.Load(entry) as GameObject;

                if (obj != null)
                {
                    ItemClass thisItem = obj.GetComponent<ItemClass>();

                    if (thisItem != null && entry == thisItem.itemName)
                    {
                        Debug.Log("tried add" + obj.GetComponent<ItemClass>().itemName);
                        AddItem(obj);
                    }
                    else { Debug.Log("Broken item is" + obj.name); }
                }
                else
                {
                    Debug.Log(entry + " Failed to load from resources");
                }
            }
        }
        ChooseAnimator();
    }
}
