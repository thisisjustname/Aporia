using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    private bool InventoryEnabled = false;
    public GameObject target;
    private float maxSpeed;
    public Canvas inventoryCanvas;
    public AIPath airPath;
    
    void Start()
    {
        target = GameObject.Find("Target");
        airPath = Player.instance.GetComponent<AIPath>();
        inventoryCanvas = GameObject.Find("Canvasinvent").GetComponent<Canvas>();
        
        maxSpeed = Player.instance.GetComponent<AIPath>().maxSpeed;
        CreateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
        if (Input.GetKeyDown(KeyCode.I))
            {
                InventoryEnabled = !InventoryEnabled;
                if (InventoryEnabled == true)
                {
                    airPath.maxSpeed = 0;
                    target.transform.position = gameObject.transform.position;
                    target.SetActive(false);
                    inventoryCanvas.GetComponent<Canvas>().enabled = true;

                }
                else
                {
                    airPath.maxSpeed = maxSpeed;
                    inventoryCanvas.GetComponent<Canvas>().enabled = false;
                    target.SetActive(true);
                }
            }
    }


    public void UpdateDisplay()
    {
        for (int i = inventory.Container.Count - 1; i >= 0; i--)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventory.Container[i].amount.ToString();
            }
            else
            {
                GameObject obj = DisplayItem(i);
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            DisplayItem(i);
        }    
    }

    public GameObject DisplayItem(int i)
    {
        GameObject prefab = Instantiate(Resources.Load("Panel")) as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
        Transform child = obj.transform.GetChild(0);
        Image sprite = child.GetComponent<Image>();
        sprite.sprite = inventory.Container[i].item.icon;
        sprite.SetNativeSize();
        RectTransform rectTransform = child.GetComponent<RectTransform>();

        float difference;
        if (rectTransform.sizeDelta.x > rectTransform.sizeDelta.y)
        {
            difference = rectTransform.sizeDelta.x / 40;
        }
        else
        {
            difference = rectTransform.sizeDelta.y / 50;
        }
        float x = rectTransform.sizeDelta.x / difference;
        float y = rectTransform.sizeDelta.y / difference;
        rectTransform.sizeDelta = new Vector2(x, y);
        rectTransform.anchoredPosition = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);

        return obj;
    }
}