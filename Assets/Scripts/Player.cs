using Questing;
using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    
    public static Player instance;
    public InventoryObject inventory;
    
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
            PositionSaveLoadManager.Instance.LoadGame();
            Player.instance.transform.position = PositionSaveLoadManager.Instance.savePositionData.spawnPosition;
        }
        Debug.Log(SceneSwitchManager.Instance.name);
        Debug.Log(PositionSaveLoadManager.Instance.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            inventory.Save();
        
        if (Input.GetKeyDown(KeyCode.L))
            inventory.Load();
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[54];
    }
}
