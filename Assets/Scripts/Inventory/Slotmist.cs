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
        
    }

    public void UseItem()
    {
        item.GetComponent<Itemmist>().ItemUsage();
    }
    
}
