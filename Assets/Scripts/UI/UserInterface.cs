using System.Collections.Generic;
using Pathfinding;
using ScriptableObjects.Inventory.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace UI
{
    public abstract class UserInterface : MonoBehaviour
    {
        public InventoryObject inventory;
        public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        protected bool InventoryEnabled = true;
        public GameObject target;
        protected float maxSpeed;
        public GameObject inventoryCanvas;
        public AIPath airPath;
    
        void Start()
        {
            target = GameObject.Find("Target");
            airPath = Player.instance.GetComponent<AIPath>();
            inventoryCanvas = GameObject.Find("Inventory");
            inventoryCanvas.SetActive(InventoryEnabled);
            maxSpeed = Player.instance.GetComponent<AIPath>().maxSpeed;

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
            Player.instance.mouseItem.hoverObj = obj;
            if (itemsDisplayed.ContainsKey(obj))
                Player.instance.mouseItem.hoverItem = itemsDisplayed[obj];
        }
        
        public void OnExit(GameObject obj)
        {
            Player.instance.mouseItem.hoverObj = null;
            Player.instance.mouseItem.hoverItem = null;
        }
        
        public void OnDragStart(GameObject obj)
        {
            var mouseObject =  new GameObject();
            var rt = mouseObject.AddComponent<RectTransform>();
            mouseObject.transform.SetParent(transform.parent.gameObject.transform.parent);  
            if (itemsDisplayed[obj].iD >= 0)
            {
                var img = mouseObject.AddComponent<Image>();
                img.sprite = inventory.database.getItem[itemsDisplayed[obj].iD].icon;
                SetNormalSize(img, rt);
                img.raycastTarget = false;
            }
            Player.instance.mouseItem.obj = mouseObject;
            Player.instance.mouseItem.item = itemsDisplayed[obj];
        }
        
        public void OnDragEnd(GameObject obj)
        {
            var itemOnMouse = Player.instance.mouseItem;
            var mouseHoverItem = itemOnMouse.hoverItem;
            var mouseHoverObject = itemOnMouse.hoverObj;
            var GetItemObject = inventory.database.getItem;

            if (itemOnMouse.uI != null)
            {
                if (mouseHoverObject)
                    if (mouseHoverItem.CanPlaceinSlot(GetItemObject[itemsDisplayed[obj].iD]) &&
                        (mouseHoverItem.item.Id <= -1 || (mouseHoverItem.item.Id >= 0 &&
                                                          itemsDisplayed[obj]
                                                              .CanPlaceinSlot(GetItemObject[mouseHoverItem.item.Id]))))
                        inventory.MoveItem(itemsDisplayed[obj],
                            mouseHoverItem.parent.itemsDisplayed[itemOnMouse.hoverObj]);
            }
            else
            {
                inventory.RemoveItem(itemsDisplayed[obj].item);
            }
            Destroy(itemOnMouse.obj);
            itemOnMouse.item = null;
        }
        
        public void OnDrag(GameObject obj)
        {
            if (Player.instance.mouseItem.obj != null)
                Player.instance.mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
        
        public void OnEnterInterface(GameObject obj)
        {
            Player.instance.mouseItem.uI = obj.GetComponent<UserInterface>();
        }
        
        public void OnExitInterface(GameObject obj)
        {
            Player.instance.mouseItem.uI = null;
        }
    }

    public class MouseItem
    {
        public UserInterface uI;
        public GameObject obj;
        public InventorySlot item;
        public InventorySlot hoverItem;
        public GameObject hoverObj;
    }
}