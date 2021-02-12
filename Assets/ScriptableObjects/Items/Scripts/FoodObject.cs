using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New food object", menuName = "Inventory system/Items/Food")]
    public class FoodObject : ItemObject
    {
        public int restoreHeals;
        public void Awake()
        {
            type = ItemType.Food;
        }
    }
}