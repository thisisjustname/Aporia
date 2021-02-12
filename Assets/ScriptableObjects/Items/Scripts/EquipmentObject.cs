using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New equipment object", menuName = "Inventory system/Items/Equipment")]
    public class EquipmentObject : ItemObject
    {
        public int def;
        public void Awake()
        {
            type = ItemType.Equipment;
        }
    }
}