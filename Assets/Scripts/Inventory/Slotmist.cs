using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Slotmist : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;
    public bool empty;
    public Sprite icon;
    public string  type;
    public int ID;
    public int cost;
    public string description;
    public Transform slotIconGO;
    public int def;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
    }


    private void Start()
    { 
        slotIconGO = transform.GetChild(0);        
    }
   


    public void UpdateSlotmist()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
        Transform child = slotIconGO;
        Image sprite = child.GetComponent<Image>();
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

        
    }

    public void UseItem()
    {
        item.GetComponent<Itemmist>().ItemUsage();
    }
    
}
