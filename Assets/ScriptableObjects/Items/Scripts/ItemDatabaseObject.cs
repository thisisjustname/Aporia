using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory system/Items/Database")]
    public class ItemDatabaseObject: ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] items;
        public Dictionary<int, ItemObject> getItem = new Dictionary<int, ItemObject>();
        
        public void OnBeforeSerialize()
        {
            getItem = new Dictionary<int, ItemObject>();
        }

        public void OnAfterDeserialize()
        {
            for (int i = items.Length - 1; i >= 0; i--)
            {
                items[i].iD = i;
                getItem.Add(i, items[i]);   
            }
        }
    }
}