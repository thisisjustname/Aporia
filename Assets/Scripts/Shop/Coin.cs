using UnityEngine;

namespace Shop
{
     public class Coin : MonoBehaviour
     {
          public int coinValue = 1;

          private void OnTriggerEnter2D(Collider2D other)
          {
               if (other.gameObject.CompareTag("Player"))
               {
                    Player.instance.currentMoney += coinValue;
                    Destroy(gameObject);
               }
          }
     }
}
