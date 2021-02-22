using ScriptableObjects.Items.Scripts;
using UI;
using UnityEngine;

namespace ScriptableObjects.Inventory.Scripts
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemType[] AllowedItems = new ItemType[0];
        
        [System.NonSerialized] public UserInterface parent;

        [System.NonSerialized] public GameObject slotDisplay;

        [System.NonSerialized] public SlotUpdated OnAfterUpdate;
        [System.NonSerialized] public SlotUpdated OnBeforUpdate;
        
        public Item item;
        public int amount;

        public ItemObject ItemObject
        {
            get
            {
                if (item.Id >= 0)
                {
                    return parent.inventory.database.ItemObjects[item.Id];
                }
                return null; 
            }
        }

        public InventorySlot()
        {
            UpdateSlot(new Item(), 0);
        }
        
        public InventorySlot(Item item, int amount)
        {
            UpdateSlot(item, amount);
        }
        
        public void UpdateSlot(Item _item, int _amount)
        {
            OnBeforUpdate?.Invoke(this);

            item = _item;
            amount = _amount;

            OnAfterUpdate?.Invoke(this);
        }

        public void RemoveItem()
        {
            UpdateSlot(new Item(), 0);
        }

        public void ChangeAmount(int value)
        {
            UpdateSlot(item, amount += value);
        }

        public bool CanPlaceInSlot(ItemObject itemObject)
        {
            if (AllowedItems.Length <= 0 || itemObject == null || itemObject.data.Id < 0)
                return true;
            
            for (int i = AllowedItems.Length - 1; i >= 0; i--)
            {
                if (itemObject.type == AllowedItems[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}