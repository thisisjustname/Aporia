using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public GameObject sign;
    public void Update()
    {
        sign.SetActive(Player.wasInShop);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}