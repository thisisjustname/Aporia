using UnityEngine.Serialization;

namespace ScriptableObjects.Inventory.Scripts
{
    [System.Serializable]
    public class Inventory
    {
        [FormerlySerializedAs("Items")] public InventorySlot[] Slots = new InventorySlot[54];

        public void Clear()
        {
            for (int i = Slots.Length - 1; i >= 0; i--)
            {
                Slots[i].RemoveItem();
            }
        }
    }
}