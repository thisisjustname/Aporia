using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New food object", menuName = "Inventory system/Items/Food")]
public class FoodObject : ItemObject
{
    public int restoreHeals;
    public void Awake()
    {
        type = ItemType.Food;
    }
}