using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory system/Items/Database")]
    public class ItemDatabaseObject: ScriptableObject, ISerializationCallbackReceiver
    {
        [FormerlySerializedAs("items")] public ItemObject[] ItemObjects;

        [ContextMenu("Update ID's")]
        public void UpdateId()
        {
            for (int i = ItemObjects.Length - 1; i >= 0; i--)
            {
                if (ItemObjects[i].data.Id != i)
                    ItemObjects[i].data.Id = i;
                
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