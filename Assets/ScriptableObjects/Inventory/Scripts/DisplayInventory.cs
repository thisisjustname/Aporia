using System;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ScriptableObjects.Inventory.Scripts
{
    public class DisplayInventory : MonoBehaviour
    {
        public MouseItem mouseItem = new MouseItem();
        
        public static DisplayInventory instance;
        
        public InventoryObject inventory;
        private Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
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
            
            CreateSlots();
        }

        public void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            UpdateSlots();
            
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
        
        public void UpdateSlots()
        {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
            {
                if (_slot.Value.iD >= 0)
                {
                    Transform child = _slot.Key.transform.GetChild(0);
                    Image image = child.GetComponentInChildren<Image>();
                    image.sprite = inventory.database.getItem[_slot.Value.iD].icon;

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

        public void CreateSlots()
        {
            itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
            for (int i = inventory.Container.Items.Length - 1; i >= 0; i--)
            {
                GameObject obj = Instantiate(Resources.Load("Panel") as GameObject, Vector3.zero, Quaternion.identity,
                    transform);
                
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj);});
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj);});
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj);});
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});

                itemsDisplayed.Add(obj, inventory.Container.Items[i]);
            }
        }
        

        public void ClearDisplay()
        {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }

        public void DisplayItem(Image _image, RectTransform _rectTransform, int _amount)
        {
            SetNormalSize(_image, _rectTransform);

            if (_amount >= 2)
                _rectTransform.anchoredPosition = new Vector2(-2.5f, 2.5f);
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
                difference = _rectTransform.sizeDelta.x / 40;
            }
            else
            {
                difference = _rectTransform.sizeDelta.y / 40;
            }
            
            var sizeDelta = _rectTransform.sizeDelta;
            float x = sizeDelta.x / difference;
            float y = sizeDelta.y / difference;
            sizeDelta = new Vector2(x, y);
            _rectTransform.localScale  = new Vector2(1, 1);
            _rectTransform.sizeDelta = sizeDelta;
        }
        
            
        private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(GameObject obj)
        {
            mouseItem.hoverObj = obj;
            if (itemsDisplayed.ContainsKey(obj))
                mouseItem.hoverItem = itemsDisplayed[obj];
        }
        
        public void OnExit(GameObject obj)
        {
            mouseItem.hoverObj = null;
            mouseItem.hoverItem = null;
        }
        
        public void OnDragStart(GameObject obj)
        {
            var mouseObject =  new GameObject();
            var rt = mouseObject.AddComponent<RectTransform>();
            mouseObject.transform.SetParent(transform.parent);
            if (itemsDisplayed[obj].iD >= 0)
            {
                var img = mouseObject.AddComponent<Image>();
                img.sprite = inventory.database.getItem[itemsDisplayed[obj].iD].icon;
                SetNormalSize(img, rt);
                img.raycastTarget = false;
            }
            mouseItem.obj = mouseObject;
            mouseItem.item = itemsDisplayed[obj];
        }
        
        public void OnDragEnd(GameObject obj)
        {
            if (mouseItem.hoverObj)
            {
                inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
            }
            else
            {
                inventory.RemoveItem(itemsDisplayed[obj].item);
            }
            Destroy(mouseItem.obj);
            mouseItem.item = null;
        }
        
        public void OnDrag(GameObject obj)
        {
            if (mouseItem.obj != null)
                mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public class MouseItem
    {
        public GameObject obj;
        public InventorySlot item;
        public InventorySlot hoverItem;
        public GameObject hoverObj;
    }
}