using System.Collections.Generic;
using Pathfinding;
using ScriptableObjects.Inventory.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public abstract class UserInterface : MonoBehaviour
    {
        public InventoryObject inventory;
        public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        
        public bool InventoryEnabled = true;
        public GameObject target;
        public float maxSpeed;
        public GameObject ui;
        public Canvas uiCanvas;
        public AIPath airPath;

        void Start()
        {
            target = GameObject.Find("Target");
            airPath = Player.instance.GetComponent<AIPath>();
            maxSpeed = Player.instance.GetComponent<AIPath>().maxSpeed;
            uiCanvas = ui.GetComponent<Canvas>();
            uiCanvas.enabled = InventoryEnabled;
            
            for (int i = inventory.GetSlots.Length - 1; i >= 0; i--)
            {
                inventory.GetSlots[i].parent = this;
                inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
            }
            
            CreateSlots();
            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject);});
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject);});
        }

        private void OnSlotUpdate(InventorySlot slot)
        {
            if (slot.item.Id >= 0)
            {
                Transform child = slot.slotDisplay.transform.GetChild(0);
                Image image = child.GetComponentInChildren<Image>();
                image.sprite = slot.ItemObject.icon;

                DisplayItem(image, child.GetComponent<RectTransform>(), slot.amount);
                image.color = new Color(1, 1, 1, 1);

                slot.slotDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    slot.amount == 1 ? "" : slot.amount.ToString();

                if (inventory.type == InterfaceType.Shop)
                {
                    Button button = slot.slotDisplay.transform.GetChild(3).GetComponent<Button>();
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(delegate { BuyItem(slot.item.Id); });
                    TextMeshProUGUI cost = slot.slotDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                    cost.text = string.Concat(slot.ItemObject.cost, " $");
                }
            }
            else
            {
                Transform child = slot.slotDisplay.transform.GetChild(0);
                Image image = child.GetComponentInChildren<Image>();
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
                slot.slotDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                
                
                if (inventory.type == InterfaceType.Shop)
                {
                    Button button = slot.slotDisplay.transform.GetChild(3).GetComponent<Button>();
                    button.onClick.RemoveAllListeners();
                    TextMeshProUGUI cost = slot.slotDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                    cost.text = "";
                }
            }
        }

        public void BuyItem(int id)
        {
            if (Player.instance.currentMoney >= inventory.database.ItemObjects[id].cost)
            {
                Player.instance.currentMoney -= inventory.database.ItemObjects[id].cost;
                RemoveItem(id, -1);
                Player.instance.inventory.AddItem(inventory.database.ItemObjects[id].data, 1);
            }
        }

        private void Update()
        {
            OpenInterface();
        }

        public abstract void OpenInterface();
        
        public void RemoveItem(int id, int count)
        {
            for (int i = inventory.GetSlots.Length - 1; i >= 0; i--)
            {
                if (inventory.GetSlots[i].item.Id == id)
                {
                    if (inventory.GetSlots[i].amount == count * -1)
                        inventory.GetSlots[i].RemoveItem();
                    else if (inventory.GetSlots[i].amount > count * -1)
                    {
                        inventory.GetSlots[i].ChangeAmount(count);
                    }
                    else
                    {
                        Debug.Log("Недостаточное количество");
                    }
                    return;
                }
            }
        }

        public abstract void CreateSlots();

        public void DisplayItem(Image image, RectTransform rectTransform, int amount)
        {
            SetNormalSize(image, rectTransform);

            if (amount >= 2)
                rectTransform.anchoredPosition = new Vector2(-6f, 6f);
            else
                rectTransform.anchoredPosition = new Vector2(0, 0);
            
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        }

        public void SetNormalSize(Image image, RectTransform rectTransform)
        {
            image.SetNativeSize();

            float difference;
            if (rectTransform.sizeDelta.x > rectTransform.sizeDelta.y)
            {
                difference = rectTransform.sizeDelta.x / 80;
            }
            else
            {
                difference = rectTransform.sizeDelta.y / 80;
            }
            
            var sizeDelta = rectTransform.sizeDelta;
            float x = sizeDelta.x / difference;
            float y = sizeDelta.y / difference;
            sizeDelta = new Vector2(x, y);
            rectTransform.localScale  = new Vector2(1, 1);
            rectTransform.sizeDelta = sizeDelta;
        }
        
            
        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(GameObject obj)
        {
            MouseData.slotHoverOver = obj;
        }
        
        public void OnExit(GameObject obj)
        {
            MouseData.slotHoverOver = null;
        }
        
        public void OnDragStart(GameObject obj)
        {
            MouseData.tempItemBeingDragged = CreateTempItem(obj);
        }

        public GameObject CreateTempItem(GameObject obj)    
        {
            GameObject tempItem = null;

            if (slotsOnInterface[obj].item.Id >= 0)
            {
                tempItem =  new GameObject();
                var rt = tempItem.AddComponent<RectTransform>();
                tempItem.transform.SetParent(transform.parent.gameObject.transform.parent);  
                
                var img = tempItem.AddComponent<Image>();
                img.sprite = slotsOnInterface[obj].ItemObject.icon;
                SetNormalSize(img, rt);
                img.raycastTarget = false;
            }

            return tempItem;
        }
        
        public void OnDragEnd(GameObject obj)
        {
            Destroy(MouseData.tempItemBeingDragged);
            
            if (MouseData.interfaceMouseIsOver == null)
            {
                slotsOnInterface[obj].RemoveItem();
                return;
            }
            
            if (MouseData.slotHoverOver)
            {
                InventorySlot mouseHoverSlotData =
                    MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoverOver];
                inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
            }
        }
        
        public void OnDrag(GameObject obj)
        {
            if (MouseData.tempItemBeingDragged != null)
                MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
        
        public void OnEnterInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        }
        
        public void OnExitInterface(GameObject obj)
        {
            MouseData.slotHoverOver = null;
        }
    }

    public static class MouseData
    {
        public static UserInterface interfaceMouseIsOver;
        public static GameObject tempItemBeingDragged;
        public static GameObject slotHoverOver;
    }
}