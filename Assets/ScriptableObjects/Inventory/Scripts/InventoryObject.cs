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


        public void AddItem(Item _item, int _amount)
        {
            if (_item.buffs.Length > 0)
            {
                SetEmptySlot(_item, _amount);
                return;
            }
            
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].iD == _item.Id)
                {
                    Container.Items[i].AddAmount(_amount);
                    return;
                }
            }
            SetEmptySlot(_item, _amount);
        }

        public InventorySlot SetEmptySlot(Item _item, int _amount)
        {
            for (int i = Container.Items.Length - 1; i >= 0; i--)
            {
                if (Container.Items[i].iD <= -1)
                {
                    Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                    return Container.Items[i];
                }
            }
            return null;
        }

        public void MoveItem(InventorySlot item1, InventorySlot item2)
        {
            InventorySlot temp = new InventorySlot(item2.iD, item2.item, item2.amount);
            item2.UpdateSlot(item1.iD, item1.item, item1.amount);
            item1.UpdateSlot(temp.iD, temp.item, temp.amount);
        }

        public void RemoveItem(Item _item)
        {
            for (int i = Container.Items.Length - 1; i >= 0; i--)
            {
                if (Container.Items[i].item == _item)
                {
                    Container.Items[i].UpdateSlot(-1, null, 0);
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
                    Container.Items[i].UpdateSlot(newContainer.Items[i].iD, newContainer.Items[i].item,
                        newContainer.Items[i].amount);
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
                Items[i].UpdateSlot(-1, new Item(), 0);
            }
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public ItemType[] AllowedItems = new ItemType[0];
        public UserInterface parent;
        public int iD;
        public Item item;
        public int amount;

        public InventorySlot()
        {
            iD = -1;
            item = null;
            amount = 0;
        }
        
        public InventorySlot(int _id, Item _item, int _amount)
        {
            iD = _id;
            item = _item;
            amount = _amount;
        }
        
        public void UpdateSlot(int _id, Item _item, int _amount)
        {
            iD = _id;
            item = _item;
            amount = _amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }

        public bool CanPlaceinSlot(ItemObject _item)
        {
            if (AllowedItems.Length <= 0)
                return true;
            
            for (int i = AllowedItems.Length - 1; i >= 0; i--)
            {
                if (_item.type == AllowedItems[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}