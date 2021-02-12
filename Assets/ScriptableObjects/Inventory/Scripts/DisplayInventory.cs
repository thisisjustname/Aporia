using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects.Inventory.Scripts
{
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
            inventoryCanvas.enabled = false;
        
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
                    inventoryCanvas.enabled = true;

                }
                else
                {
                    airPath.maxSpeed = maxSpeed;
                    inventoryCanvas.enabled = false;
                    target.SetActive(true);
                }
            }
        }


        public void UpdateDisplay()
        {
            for (int i = inventory.Container.Items.Count - 1; i >= 0; i--)
            {
                InventorySlot slot = inventory.Container.Items[i];
                
                if (itemsDisplayed.ContainsKey(slot))
                {
                    itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text =
                        slot.amount.ToString();
                }
                else
                {
                    GameObject obj = DisplayItem(slot);
                    itemsDisplayed.Add(slot, obj);
                }
            }
        }

        public void CreateDisplay()
        {
            for (int i = 0; i < inventory.Container.Items.Count; i++)
            {
                InventorySlot slot = inventory.Container.Items[i];
                DisplayItem(slot);
            }    
        }

        public GameObject DisplayItem(InventorySlot slot)
        {
            GameObject prefab = Instantiate(Resources.Load("Panel")) as GameObject;
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            Transform child = obj.transform.GetChild(0);
            Image sprite = child.GetComponent<Image>();
            sprite.sprite = inventory.database.getItem[slot.item.Id].icon;
            sprite.SetNativeSize();
            RectTransform rectTransform = child.GetComponent<RectTransform>();

            float difference;
            if (rectTransform.sizeDelta.x > rectTransform.sizeDelta.y)
            {
                difference = rectTransform.sizeDelta.x / 40;
            }
            else
            {
                difference = rectTransform.sizeDelta.y / 40;
            }

            var sizeDelta = rectTransform.sizeDelta;
            float x = sizeDelta.x / difference;
            float y = sizeDelta.y / difference;
            sizeDelta = new Vector2(x, y);
            rectTransform.sizeDelta = sizeDelta;
            rectTransform.anchoredPosition = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);

            return obj;
        }
    }
}