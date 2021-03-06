using UnityEngine;

namespace Questing
{
    public class DontDestoy2 : MonoBehaviour
    {
        public static DontDestoy2 instance = null;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        
            DontDestroyOnLoad(gameObject);
        }
    }
}
