using Inventory;
using Pathfinding;
using UnityEngine;

namespace Shop
{
    public class Shopsystem : MonoBehaviour
    {
        private bool ShopEnabled;
        public GameObject shop;
        public GameObject target;
        private int allSlots;
        private int enabledSlots;
        private GameObject[] slot;

        public GameObject ShopHolder;

        private float maxSpeed;
    
        void Start()
        {
            maxSpeed = gameObject.GetComponent<AIPath>().maxSpeed;
            allSlots = 5;
        
            if (shop == null)
            {
                gameObject.GetComponent<Shopsystem>().enabled = false;
            }
            else
            {
                shop.GetComponent<Canvas>().enabled = false;
                slot = new GameObject[allSlots];
        
                for (int i = 0; i < allSlots; i++)
                {
                    slot[i] = ShopHolder.transform.GetChild(i).gameObject;

                    if (slot[i].GetComponent<Slotmist>().item == null)
                        slot[i].GetComponent<Slotmist>().empty = true;
                }
            }
        
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ShopEnabled = !ShopEnabled;
                if (ShopEnabled == true)
                {
                    gameObject.GetComponent<AIPath>().maxSpeed = 0;
                    target.transform.position = gameObject.transform.position;
                    target.SetActive(false);
                    shop.GetComponent<Canvas>().enabled = true;

                }
                else
                {
                    gameObject.GetComponent<AIPath>().maxSpeed = maxSpeed;
                    shop.GetComponent<Canvas>().enabled = false;
                    target.SetActive(true);
                }
            }
        }
 
        // private void OnTriggerEnter2D(Collider2D other)
        //  {
        //   if (other.tag == "Item")
        //   {
        //       GameObject itemPickedUp = other.gameObject;
        //       Itemmist item = itemPickedUp.GetComponent<Itemmist>();
            
        //       AddItem(itemPickedUp,item.ID, item.type, item.description, item.icon, item.def);
        //   }
        // }

        void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon, int itemDef)
        {
            for (int i = 0; i < allSlots; i++)
            {
                if(slot[i].GetComponent<Slotmist>().empty)
                {
                    itemObject.GetComponent<Itemmist>().pickedUp = true;
                    slot[i].GetComponent<Slotmist>().item = itemObject;
                    slot[i].GetComponent<Slotmist>().icon = itemIcon;
                    slot[i].GetComponent<Slotmist>().type = itemType;
                    slot[i].GetComponent<Slotmist>().description = itemDescription;
                    slot[i].GetComponent<Slotmist>().ID = itemID;
                    slot[i].GetComponent<Slotmist>().def = itemDef;
                
                    itemObject.transform.parent = slot[i].transform;
                    itemObject.SetActive(false);
                    slot[i].GetComponent<Slotmist>().UpdateSlotmist();
                
                    slot[i].GetComponent<Slotmist>().empty = false;
                    return;
                }
            
            }
        }
    }
}
