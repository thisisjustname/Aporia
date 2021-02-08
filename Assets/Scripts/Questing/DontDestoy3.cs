using UnityEngine;

namespace Questing
{
    public class DontDestoy3 : MonoBehaviour
    {
        public static DontDestoy3 instance = null;
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
