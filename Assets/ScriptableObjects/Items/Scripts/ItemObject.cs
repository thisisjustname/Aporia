using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Items.Scripts;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ScriptableObjects.Items.Scripts
{
    public enum ItemType
    {
        Food,
        Equipment,
        Default
    }
    
    public abstract class ItemObject : ScriptableObject
    {
        public ItemType  type;
        public int iD;
        public int cost;
        [TextArea(15, 20)]
        public string description;
        public Sprite icon;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.iD;
    }
}