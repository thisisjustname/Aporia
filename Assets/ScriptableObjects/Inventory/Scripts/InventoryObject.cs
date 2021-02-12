using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ScriptableObjects.Items.Scripts;
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
            for (int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].iD == _item.Id)
                {
                    Container.Items[i].AddAmount(_amount);
                    return;
                }
            }
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
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
                Container = (Inventory) formatter.Deserialize(stream);
                stream.Close();
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            Container = new Inventory();
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public List<InventorySlot> Items = new List<InventorySlot>();
    }

    [System.Serializable]
    public class InventorySlot
    {
        public int iD;
        public Item item;
        public int amount;

        public InventorySlot(int _id, Item _item, int _amount)
        {
            iD = _id;
            item = _item;
            amount = _amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}