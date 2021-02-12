using ScriptableObjects.Items.Scripts;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    public enum ItemType
    {
        Food,
        Equipment,
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
        public ItemType  type;
        public int iD;
        public int cost;
        [TextArea(15, 20)]
        public string description;
        public Sprite icon;
        public ItemBuff[] buffs;

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

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.iD;

        buffs = new ItemBuff[item.buffs.Length];
        for (int i = buffs.Length - 1; i >= 0; i--)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max)
            {
                attribute = item.buffs[i].attribute
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