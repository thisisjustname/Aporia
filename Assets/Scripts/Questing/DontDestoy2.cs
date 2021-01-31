using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
