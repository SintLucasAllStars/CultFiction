using Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Singleton

    public static PlayerController instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("Inventory"), SerializeField]
    private GameObjectIntDictionary placementSlotStore = GameObjectIntDictionary.New<GameObjectIntDictionary>();

    //GameObject -> hand, backpack etc.
    //int -> max 5kg to wear etc.
    private Dictionary<GameObject, int> placementSlot
    {
        get { return placementSlotStore.dictionary; }
    }

    //Current placementWear:   Placement | carry items
    private Dictionary<ItemInInventory, List<GameObject>> itemsPlacementWear = new Dictionary<ItemInInventory, List<GameObject>>();


    [Header("Items Pick up")]
    [SerializeField] LayerMask itemLayer;
    [SerializeField] float pickRadius = 3f;

    [SerializeField] Text textPickup;
    [SerializeField] string pickupText = "PRESS <E> TO PICK UP <NAME>";

    [SerializeField] Image hungryBarImage;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Text scoreText;

    private int _health;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value > 0)
            {

                if (value > 100)
                    _health = 100;
                else
                    _health = value;

                hungryBarImage.fillAmount = _health / 100f;
            }
            else
            {
                DeadByHungry();
                _health = value;
            }
        }
    }

    private bool buttonPressNoMouseEvent = false;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        textPickup.enabled = false;
        CheckedInventorySlotSize();

        for (int index = 0; index < placementSlot.Count; index++)
        {
            itemsPlacementWear.Add(new ItemInInventory(placementSlot.ElementAt(index).Key, placementSlot.ElementAt(index).Value), new List<GameObject>());
        }

        health = 100;
        InvokeRepeating("HungryLife", 2, 0.5f);
    }

    void HungryLife ()
    {
        health--;
    }

    void DeadByHungry()
    {
        hungryBarImage.fillAmount = 0;
        Destroy (this.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>());
        gameOverCanvas.SetActive(true);
        scoreText.text = scoreText.text.Replace("<punt>", ((int)(Time.timeSinceLevelLoad)).ToString());
        Debug.Log("Game Over");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Destroy(this);
    }

    public void CheckedInventorySlotSize()
    {
        int currentI = 0;
        List<GameObject> destoyList = new List<GameObject>();

        foreach (KeyValuePair<GameObject, int> entry in placementSlot)
        {
            if (entry.Key == null || !entry.Key.scene.IsValid())
            {
                destoyList.Add(entry.Key);
            }
            else
            {
                currentI += entry.Value;
            }
        }

        foreach (GameObject o in destoyList)
        {
            placementSlot.Remove(o);
        }


        if (currentI != InventoryManagement.instance.slotSize)
            InventoryManagement.instance.SlotChangedSize(currentI);

        Debug.Log("Inventory slot: " + InventoryManagement.instance.slotSize);
    }

    void Update()
    {
        //Check if MousePoint hit Item
        GameObject hitGO;
        Inventory.ItemPickup hitItemP;
        CheckRaycastHitItem(out hitGO, out hitItemP);



        //// item pickup
        if (Input.GetKey(KeyCode.E))
        {
            //Run one frame by press E
            if (Input.GetKeyDown(KeyCode.E))
                buttonPressNoMouseEvent = true;

            //Check that function is not for backpack?
            if (Input.GetMouseButtonDown(0))
            {
                //Pick up by hand left
                buttonPressNoMouseEvent = false;
                if (AddItem(hitItemP, itemsPlacementWear.ElementAt(0).Key))
                {
                    itemsPlacementWear.ElementAt(0).Value.Add(hitItemP.gameObject);
                    hitGO.transform.parent = itemsPlacementWear.ElementAt(0).Key.placement.transform;
                    SetItemToInventory(hitGO);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //Pick up by hand right
                buttonPressNoMouseEvent = false;
                if (AddItem(hitItemP, itemsPlacementWear.ElementAt(1).Key))
                {
                    itemsPlacementWear.ElementAt(1).Value.Add(hitItemP.gameObject);
                    hitGO.transform.parent = itemsPlacementWear.ElementAt(1).Key.placement.transform;
                    SetItemToInventory(hitGO);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.E) && buttonPressNoMouseEvent)
        {
            buttonPressNoMouseEvent = false;
        }

        //// item dropping
        else if (Input.GetKey(KeyCode.Q))
        {
            //Run one frame by press Q without E
            if (Input.GetKeyDown(KeyCode.E))
                buttonPressNoMouseEvent = true;

            //Check that function is not for backpack?
            if (Input.GetMouseButtonDown(0))
            {
                //Drop out by hand left
                buttonPressNoMouseEvent = false;
                RemoveItem(0);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //Drop out by hand right
                buttonPressNoMouseEvent = false;
                RemoveItem(1);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            UseItem(0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            UseItem(1);
        }
    }

    private void SetItemToInventory (GameObject itemGB)
    {
        Destroy(itemGB.GetComponent<Rigidbody>());

        if (itemGB.GetComponent<BoxCollider>() != null)
            itemGB.GetComponent<BoxCollider>().enabled = false;
        if (itemGB.GetComponent<CapsuleCollider>() != null)
            itemGB.GetComponent<CapsuleCollider>().enabled = false;

        itemGB.transform.localPosition = Vector3.zero;
        itemGB.transform.localRotation = Quaternion.identity;
    }


    private void CheckRaycastHitItem(out GameObject hitG, out Inventory.ItemPickup hitItem)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, pickRadius, itemLayer) && hit.collider.gameObject.GetComponent<Inventory.ItemPickup>() != null)
        {
            hitG = hit.collider.gameObject;
            hitItem = hitG.GetComponent<Inventory.ItemPickup>();
            if (hitItem.item != null)
            {
                textPickup.enabled = true;
                textPickup.text = pickupText.Replace("<NAME>", hitItem.item.itemName.ToUpper());
            }
        }
        else
        {
            textPickup.enabled = false;
            hitG = null;
            hitItem = null;
        }
    }

    private bool FindPlaceItem (Inventory.ItemPickup ite, out GameObject goPlacement)
    {
        GameObject ob;
        foreach (KeyValuePair<ItemInInventory, List<GameObject>> ipw in itemsPlacementWear)
        {
            if (AddItem(ite, ipw.Key, out ob, out goPlacement))
            {
                ipw.Value.Add(ob);
                return true;
            }
        }
        goPlacement = null;
        return false;
    }

    private bool AddItem (Inventory.ItemPickup ip, ItemInInventory ii,  out GameObject ipAddPlaceList, out GameObject parentPlace)
    {
        if (ip.item.itemWeight <= ii.placementWear)
        {
            ii.placementWear =- ip.item.itemWeight;
            ipAddPlaceList = ip.gameObject;
            parentPlace = ii.placement;
            return true;
        }
        ipAddPlaceList = null;
        parentPlace = null;
        return false;
    }

    private bool AddItem(Inventory.ItemPickup ip, ItemInInventory ii)
    {
        if (ip.item != null && ip.item.itemWeight <= ii.placementWear)
        {
            ii.placementWear =- ip.item.itemWeight;
            return true;
        }
        return false;
    }

    public void ThrowItem (int index, int throwHeight)
    {
        if (itemsPlacementWear.Count > index && itemsPlacementWear.ElementAt(index).Value.Count > 0)
        {
            GameObject obj = itemsPlacementWear.ElementAt(index).Value[itemsPlacementWear.ElementAt(index).Value.Count - 1];
            RemoveItem(index);
            obj.GetComponent<Rigidbody>().AddForce(-Vector3.down * throwHeight);
        }
    }

    public void DestroyItem(int index)
    {
        if (itemsPlacementWear.Count > index && itemsPlacementWear.ElementAt(index).Value.Count > 0)
        {
            GameObject obj = itemsPlacementWear.ElementAt(index).Value[itemsPlacementWear.ElementAt(index).Value.Count - 1];
            RemoveItem(index);
            Destroy(obj);
        }
    }

    private void RemoveItem(int index)
    {
        if (itemsPlacementWear.ElementAt(index).Value.Count > 0) //itemsPlacementWear.Count > index) // && itemsPlacementWear.ElementAt(index).Value.Count > 0)
        {
            GameObject selectGO = itemsPlacementWear.ElementAt(index).Value[itemsPlacementWear.ElementAt(index).Value.Count - 1];
            itemsPlacementWear.ElementAt(index).Key.placementWear =+ selectGO.GetComponent<ItemPickup>().item.itemWeight;
            itemsPlacementWear.ElementAt(index).Value.Remove(selectGO);

           selectGO.transform.parent = null;
            if (selectGO.GetComponent<BoxCollider>() != null)
                selectGO.GetComponent<BoxCollider>().enabled = true;
            if (selectGO.GetComponent<CapsuleCollider>() != null)
                selectGO.GetComponent<CapsuleCollider>().enabled = true;
            selectGO.AddComponent<Rigidbody>();
        }
    }

    private void UseItem(int index)
    {
        if (itemsPlacementWear.Count > index && itemsPlacementWear.ElementAt(index).Value.Count > 0)
        {
            Inventory.ItemPickup i = itemsPlacementWear.ElementAt(index).Value[itemsPlacementWear.ElementAt(index).Value.Count - 1].GetComponent<Inventory.ItemPickup>();
            i.item.Use(index);
        }
    }

}
