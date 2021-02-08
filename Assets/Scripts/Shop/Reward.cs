using UnityEngine;

namespace Shop
{
    public class Reward : MonoBehaviour
    {
        public GameObject sign;
        public void Update()
        {
            sign.SetActive(Player.wasInShop);
        }
    }
}