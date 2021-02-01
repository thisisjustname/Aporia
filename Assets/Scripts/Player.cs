using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    public int maxHealth = 100;
    public static bool wasInShop = false;
    public static int currentMoney;
    public GameObject god;
    public static bool wasInLords = false;
    public static int currentHealth = 100;
    
    public static bool appearInPoint = false;
    public static Quest quest;

    public HealthBar healthBar;
    private void Start()
    {
        healthBar.SetHealth(maxHealth);
        god = GameObject.Find("QuestGiverShop");
    }

    private void Awake()
    {
        instance = this;

        if (appearInPoint)
        {
            SaveLoadManager.Instance.LoadGame();
            Player.instance.transform.position = SaveLoadManager.Instance.saveData.spawnPosition;
        }
        Debug.Log(SceneSwitchManager.Instance.name);
        Debug.Log(SaveLoadManager.Instance.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeGamage(20);
        }
        
    }

    void TakeGamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void StartQuest()
    {
        Player.wasInLords = true;
    }

    public void WasInLorrrddss()
    {
        Player.wasInLords = false;
    }

    public void IncreaseValue(int value)
    {
        god.GetComponent<QuestGiver>().quest.goal.currentAmount += value;
    }
}
