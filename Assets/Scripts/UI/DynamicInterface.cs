using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

namespace UI
{
    public class DynamicInterface : UserInterface
    {

        public override void OpenInterface()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                InventoryEnabled = !InventoryEnabled;
                if (InventoryEnabled == true)
                {
                    Debug.Log("Inventory is close");
                    airPath.maxSpeed = 0;
                    target.transform.position = gameObject.transform.position;
                    target.SetActive(false);
                    inventoryCanvas.SetActive(true);

                }
                else
                {
                    Debug.Log("Inventory is open");
                    airPath.maxSpeed = maxSpeed;
                    inventoryCanvas.SetActive(false);
                    target.SetActive(true);
                }
            }
        }

        public override void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
            for (int i = inventory.Container.Items.Length - 1; i >= 0; i--)
            {
                GameObject obj = Instantiate(Resources.Load("Panel") as GameObject, Vector3.zero, Quaternion.identity,
                    transform);
                
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj);});
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj);});
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj);});
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});

                slotsOnInterface.Add(obj, inventory.Container.Items[i]);
            }
        }
    }
}
