using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory system/Items/Database")]
    public class ItemDatabaseObject: ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] items;

        [ContextMenu("Update ID's")]
        public void UpdateId()
        {
            for (int i = items.Length - 1; i >= 0; i--)
            {
                if (items[i].data.Id != i)
                    items[i].data.Id = i;
                
            }
        }
        
        public void OnBeforeSerialize()
        {
           
        }

        public void OnAfterDeserialize()
        {
            UpdateId();
        }
    }
}