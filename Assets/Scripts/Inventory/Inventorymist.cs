using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Inventorymist : MonoBehaviour
{
    private bool InventoryEnabled;
    public GameObject inventory;
    public GameObject target;
    private int allSlots;
    private int enabledSlots;
    public Sprite background;
    private GameObject[] slot;

    public GameObject slotHolder;

    private float maxSpeed;
    
    void Start()
    {
        maxSpeed = gameObject.GetComponent<AIPath>().maxSpeed;
        allSlots = 40;
        
        inventory = GameObject.Find("Canvasinvent");
        slotHolder = GameObject.Find("Slot_Holder");
        inventory.GetComponent<Canvas>().enabled = false;
        slot = new GameObject[allSlots];
        
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slotmist>().item == null)
                slot[i].GetComponent<Slotmist>().empty = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryEnabled = !InventoryEnabled;
            if (InventoryEnabled == true)
            {
                gameObject.GetComponent<AIPath>().maxSpeed = 0;
                target.transform.position = gameObject.transform.position;
                target.SetActive(false);
                inventory.GetComponent<Canvas>().enabled = true;

            }
            else
            {
                gameObject.GetComponent<AIPath>().maxSpeed = maxSpeed;
                inventory.GetComponent<Canvas>().enabled = false;
                target.SetActive(true);
            }
        }
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Itemmist item = itemPickedUp.GetComponent<Itemmist>();
            
            AddItem(itemPickedUp,item.ID, item.type, item.description, item.icon, item.def, item.cost);
        }
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon, int itemDef, int cost)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if(slot[i].GetComponent<Slotmist>().empty)
            {
                itemObject.GetComponent<Itemmist>().pickedUp = true;
                slot[i].GetComponent<Slotmist>().item = itemObject;
                slot[i].GetComponent<Slotmist>().cost = cost;
                slot[i].GetComponent<Slotmist>().icon = itemIcon;
                slot[i].GetComponent<Slotmist>().type = itemType;
                slot[i].GetComponent<Slotmist>().description = itemDescription;
                slot[i].GetComponent<Slotmist>().ID = itemID;
                slot[i].GetComponent<Slotmist>().def = itemDef;
                
                itemObject.transform.parent = slot[i].transform;    
                itemObject.GetComponent<SpriteRenderer>().enabled = false;
                slot[i].GetComponent<Slotmist>().UpdateSlotmist();
                
                slot[i].GetComponent<Slotmist>().empty = false;
                return;
            }
            
        }
    }
    
    public void AddItemInShop(int itemID, string itemType, string itemDescription, Sprite itemIcon, int itemDef, int cost)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if(slot[i].GetComponent<Slotmist>().empty)
            {
                slot[i].GetComponent<Slotmist>().icon = itemIcon;
                slot[i].GetComponent<Slotmist>().type = itemType;
                slot[i].GetComponent<Slotmist>().description = itemDescription;
                slot[i].GetComponent<Slotmist>().ID = itemID;
                slot[i].GetComponent<Slotmist>().cost = cost;
                slot[i].GetComponent<Slotmist>().def = itemDef;
                
                slot[i].GetComponent<Slotmist>().UpdateSlotmist();
                
                slot[i].GetComponent<Slotmist>().empty = false;
                return;
            }
            
        }
    }
    
    public void DeleteMushrom()
    {
        for (int i = 0; i < allSlots; i++)
        {
            if(!slot[i].GetComponent<Slotmist>().empty &  slot[i].GetComponent<Slotmist>().description == "Mushroom")
            {
                slot[i].GetComponent<Slotmist>().icon = null;
                Transform eeee;
                eeee = slot[i].transform.GetChild(0);    
                eeee.GetComponent<Image>().sprite = background;
            }
            
        }
    }
}