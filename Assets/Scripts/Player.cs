﻿using System;
using Questing;
using SaveSystem;
using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public InventoryObject inventory;
    public CollectibleItemSet collectedItems;
    public InventoryObject equipment;
    public InventoryObject shop;
    
    public int maxHealth = 100;
    public static bool wasInShop = false;
    public int currentMoney = 100;
    public GameObject god;
    public static bool wasInLords = false;
    public static int currentHealth = 100;
    
    public static bool appearInPoint = false;
    public static Quest quest;

    public HealthBar healthBar;
    
    public Attribute[] attributes;

    
    private void Start()
    {
        healthBar.SetHealth(maxHealth);
        god = GameObject.Find("QuestGiverShop");
        collectedItems = FindObjectOfType<CollectibleItemSet>();

        for (int i = attributes.Length - 1; i >= 0; i--)
            attributes[i].SetParent(this);

        for (int i = equipment.GetSlots.Length - 1; i >= 0; i--)
        {
            equipment.GetSlots[i].OnBeforUpdate += OnBeforeSlotsUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotsUpdate;
        }
        
        for (int i = shop.GetSlots.Length - 1; i >= 0; i--)
        {
            shop.GetSlots[i].OnBeforUpdate += OnBeforeSlotsUpdate;
            shop.GetSlots[i].OnAfterUpdate += OnAfterSlotsUpdate;
        }
    }

    public void OnBeforeSlotsUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed ", _slot.ItemObject, "on ", _slot.parent.inventory.type,
                    " , Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
                
                for (int i = _slot.item.buffs.Length - 1; i >= 0; i--)
                {
                    for (int j = attributes.Length - 1; j >= 0; j--)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }
                
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    
    public void OnAfterSlotsUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed ", _slot.ItemObject, "on ", _slot.parent.inventory.type,
                    " , Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = _slot.item.buffs.Length - 1; i >= 0; i--)
                {
                    for (int j = attributes.Length - 1; j >= 0; j--)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }
                
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        instance = this;

        if (appearInPoint)
        {
            PositionSaveLoadManager.Instance.LoadGame();
            instance.transform.position = PositionSaveLoadManager.Instance.savePositionData.spawnPosition;
        }
        Debug.Log(SceneSwitchManager.Instance.name);
        Debug.Log(CollectibleItemSet.Instance.name);
        Debug.Log(PositionSaveLoadManager.Instance.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            shop.Save();
            equipment.Save();
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.Load();
            shop.Load();
            equipment.Load();
        }
    }

    void TakeGamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void StartQuest()
    {
        wasInLords = true;
    }

    public void WasInLorrrddss()
    {
        wasInLords = false;
    }

    public void IncreaseValue(int value)
    {
        // god.GetComponent<QuestGiver>().quest.goal.currentAmount += value;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            if (inventory.AddItem(new Item(item.item), 1))
            {
                Destroy(other.gameObject);
                collectedItems.CollectedItems.Add(item.ID);
            }
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated. Value is now ", attribute.value.ModifiedValue));
    }
    
    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
}

[Serializable]
public class Attribute
{
    [NonSerialized] public Player parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}