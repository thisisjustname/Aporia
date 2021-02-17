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

        void Start()
        {
            for (int i = inventory.Container.Items.Length - 1; i >= 0; i--)
            {
                inventory.Container.Items[i].parent = this;
            }
            
            CreateSlots();
            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject);});
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject);});
        }
        
        private void Update()
        {
            UpdateSlots();
            OpenInterface();
        }

        public abstract void OpenInterface();
        
        public void UpdateSlots()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in slotsOnInterface)
            {
                if (_slot.Value.item.Id >= 0)
                {
                    Transform child = _slot.Key.transform.GetChild(0);
                    Image image = child.GetComponentInChildren<Image>();
                    image.sprite = _slot.Value.ItemObject.icon;

                    DisplayItem(image, child.GetComponent<RectTransform>(), _slot.Value.amount);
                    image.color = new Color(1, 1, 1, 1);

                    _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                        _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString();
                }
                else
                {
                    Transform child = _slot.Key.transform.GetChild(0);
                    Image image = child.GetComponentInChildren<Image>();
                    image.sprite = null;
                    image.color = new Color(1, 1, 1, 0);
                    _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }

        public abstract void CreateSlots();

        public void DisplayItem(Image _image, RectTransform _rectTransform, int _amount)
        {
            SetNormalSize(_image, _rectTransform);

            if (_amount >= 2)
                _rectTransform.anchoredPosition = new Vector2(-6f, 6f);
            else
                _rectTransform.anchoredPosition = new Vector2(0, 0);
            
            _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        }

        public void SetNormalSize(Image _image, RectTransform _rectTransform)
        {
            _image.SetNativeSize();

            float difference;
            if (_rectTransform.sizeDelta.x > _rectTransform.sizeDelta.y)
            {
                difference = _rectTransform.sizeDelta.x / 80;
            }
            else
            {
                difference = _rectTransform.sizeDelta.y / 80;
            }
            
            var sizeDelta = _rectTransform.sizeDelta;
            float x = sizeDelta.x / difference;
            float y = sizeDelta.y / difference;
            sizeDelta = new Vector2(x, y);
            _rectTransform.localScale  = new Vector2(1, 1);
            _rectTransform.sizeDelta = sizeDelta;
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
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoverOver];
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