using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DynamicInterface : UserInterface
    {

        public override void OpenInterface()
        {
            
        }

        public override void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
            for (int i = inventory.GetSlots.Length - 1; i >= 0; i--)
            {
                GameObject obj = Instantiate(Resources.Load("Panel") as GameObject, Vector3.zero, Quaternion.identity,
                    transform);
                
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj);});
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj);});
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj);});
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});
                
                inventory.GetSlots[i].slotDisplay = obj;

                slotsOnInterface.Add(obj, inventory.GetSlots[i]);
            }
        }
    }
}
