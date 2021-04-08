using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    public enum ItemType
    {
        Food,
        Helmet,
        Weapon,
        Shield,
        Boots,
        Chest,
        Default
    }

    public enum Attributes
    {
        Agility,
        Intellect,
        Stamina,
        Strength
    }
    
    public abstract class ItemObject : ScriptableObject
    {
        public ItemType type;
        public bool stackable;
        public int cost;
        [TextArea(15, 20)]
        public string description;
        public Sprite icon;
        public Item data = new Item();

        public Item CreateItem()
        {
            Item newItem = new Item(this);
            return newItem;
        }
    }
}