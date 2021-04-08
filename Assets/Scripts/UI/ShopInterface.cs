using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ShopInterface : UserInterface
    {

        public GameObject[] slots;
        public Item[] items;

        public override void OpenInterface()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                InventoryEnabled = !InventoryEnabled;
                if (InventoryEnabled)
                {
                    airPath.maxSpeed = 0;
                    target.transform.position = gameObject.transform.position;
                    target.SetActive(false);
                    uiCanvas.enabled = InventoryEnabled;
                }
                else
                {
                    airPath.maxSpeed = maxSpeed;
                    target.SetActive(true);
                    uiCanvas.enabled = InventoryEnabled;
                }
            }
        }

        public void BuyItem()
        {
            Player.instance.inventory.AddItem(inventory.database.ItemObjects[1].data, 1);
        }

        public override void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
            for (int i = inventory.GetSlots.Length - 1; i >= 0; i--)
            {
                var obj = slots[i];
                
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj);});
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj);});
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj);});
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});

                inventory.GetSlots[i].slotDisplay = obj;
                inventory.GetSlots[i].UpdateSlot(inventory.GetSlots[i].item, inventory.GetSlots[i].amount);

                slotsOnInterface.Add(obj, inventory.Container.Slots[i]);
            }
        }
    }
}