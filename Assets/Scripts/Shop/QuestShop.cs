using UnityEngine;

namespace Shop
{
    public class QuestShop : MonoBehaviour
    {
        public GameObject sign;
        public void Update()
        {
            sign.SetActive(Player.wasInLords);
        }
    }
}
