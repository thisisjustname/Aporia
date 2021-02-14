using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class StaticInterface : UserInterface
    {

        public GameObject[] slots;
        public override void OpenInterface()
        {
            
        }

        public override void CreateSlots()
        {
            itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
            for (int i = inventory.Container.Items.Length - 1; i >= 0; i--)
            {
                var obj = slots[i];
                
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj);});
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj);});
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj);});
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});

                itemsDisplayed.Add(obj, inventory.Container.Items[i]);
            }
        }
    }
}
