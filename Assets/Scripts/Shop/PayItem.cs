using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayItem : MonoBehaviour
{
    public GameObject player;
    public GameObject slot;
    public GameObject countOfMoney;

    public void Awake()
    {
        countOfMoney = GameObject.Find("ScoreManager");
    }
    
    
    public void Pay()
    {
        if (slot.GetComponent<Slotmist>().cost <= countOfMoney.GetComponent<ScoreManager>().score)
        {
            countOfMoney.GetComponent<ScoreManager>().score -= slot.GetComponent<Slotmist>().cost;

            player.GetComponent<Inventorymist>().AddItemInShop(slot.GetComponent<Slotmist>().ID, slot.GetComponent<Slotmist>().type, slot.GetComponent<Slotmist>().description, slot.GetComponent<Slotmist>().icon, slot.GetComponent<Slotmist>().def, slot.GetComponent<Slotmist>().cost);
        }
    }
}
