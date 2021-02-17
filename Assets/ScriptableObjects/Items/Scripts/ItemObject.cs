using ScriptableObjects.Items.Scripts;
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

[System.Serializable]
public class Item
{
    public string Name;
    public ItemBuff[] buffs;
    public int Id;

    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;

        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = buffs.Length - 1; i >= 0; i--)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;

    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = Random.Range(min, max);
    }
}