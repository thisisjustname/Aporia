using UnityEngine;

public class DontDestoy : MonoBehaviour
{
    public static DontDestoy instance = null;
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
