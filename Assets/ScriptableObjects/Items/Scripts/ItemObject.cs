using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public enum ItemType
{
    Food,
    Equipment,
    Default
}

[CreateAssetMenu(fileName = "Item")]
public abstract class ItemObject : ScriptableObject
{
    public ItemType  type;
    public int ID;
    public int cost;
    [TextArea(15, 20)]
    public string description;
    public Sprite icon;
}
