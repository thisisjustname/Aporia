using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ScriptableObjects.Items.Scripts;
using UI;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory system/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public string savePath;
        public ItemDatabaseObject database;
        public Inventory Container;


        public bool AddItem(Item _item, int _amount)
        {
            if (EmptySlotCount <= 0)
                return false;
            
            InventorySlot slot = FindItemOnInventiry(_item);
            if (!database.items[_item.Id].stackable || slot == null)
            {
                SetEmptySlot(_item, _amount);
                return true;
            }
            slot.AddAmount(_amount);
            return true;
        }

        public int EmptySlotCount
        {
            get
            {
                int counter = 0;
                for (int i = Container.Items.Length - 1; i >= 0; i--)
                {
                    if (Container.Items[i].item.Id <= -1)
                        counter++;
                }

                return counter;
            }
        }

        public InventorySlot FindItemOnInventiry(Item _item)
        {
            for (int i = Container.Items.Length - 1; i >= 0; i--)
            {
                if (Container.Items[i].item.Id == _item.Id)
                    return Container.Items[i];
            }

            return null;
        }

        public InventorySlot SetEmptySlot(Item _item, int _amount)
        {
            for (int i = Container.Items.Length - 1; i >= 0; i--)
            {
                if (Container.Items[i].item.Id <= -1)
                {
                    Container.Items[i].UpdateSlot(_item, _amount);
                    return Container.Items[i];
                }
            }
            return null;
        }

        public void SwapItems(InventorySlot item1, InventorySlot item2)
        {
            if (item2.CanPlaceinSlot(item1.ItemObject) && item1.CanPlaceinSlot(item2.ItemObject))
            {
                InventorySlot temp = new InventorySlot(item2.item, item2.amount);
                item2.UpdateSlot(item1.item, item1.amount);
                item1.UpdateSlot(temp.item, temp.amount);
            }
        }

        public void RemoveItem(Item _item)
        {
            for (int i = Container.Items.Length - 1; i >= 0; i--)
            {
                if (Container.Items[i].item == _item)
                {
                    Container.Items[i].UpdateSlot(null, 0);
                }
            }
        }

        [ContextMenu("Save")]
        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create,
                FileAccess.Write);
            formatter.Serialize(stream, Container);
            stream.Close();
        }

        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open,
                    FileAccess.Read);
                Inventory newContainer = (Inventory) formatter.Deserialize(stream);

                for (int i = Container.Items.Length - 1; i >= 0; i--)
                {
                    Container.Items[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
                }
                stream.Close();
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            Container.Clear();
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] Items = new InventorySlot[54];

        public void Clear()
        {
            for (int i = Items.Length - 1; i >= 0; i--)
            {
                Items[i].RemoveItem();
            }
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public ItemType[] AllowedItems = new ItemType[0];
        [System.NonSerialized]
        public UserInterface parent;
        public Item item;
        public int amount;

        public ItemObject ItemObject
        {
            get
            {
                if (item.Id >= 0)
                {
                    return parent.inventory.database.items[item.Id];
                }   

                return null; 
            }
        }

        public InventorySlot()
        {
            item = new Item();
            amount = 0;
        }
        
        public InventorySlot(Item _item, int _amount)
        {
            item = _item;
            amount = _amount;
        }
        
        public void UpdateSlot(Item _item, int _amount)
        {
            item = _item;
            amount = _amount;
        }

        public void RemoveItem()
        {
            item = new Item();
            amount = 0;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }

        public bool CanPlaceinSlot(ItemObject _itemObject)
        {
            if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
                return true;
            
            for (int i = AllowedItems.Length - 1; i >= 0; i--)
            {
                if (_itemObject.type == AllowedItems[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}