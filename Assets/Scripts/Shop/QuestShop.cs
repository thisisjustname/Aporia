using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestShop : MonoBehaviour
{
    public GameObject sign;
    public void Update()
    {
        sign.SetActive(Player.wasInLords);
    }
}
