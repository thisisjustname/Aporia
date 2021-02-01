using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aporia
{
    public class Singlton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static System.Object _lock = new System.Object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance == null)
                        {
                            var singleton = new GameObject("" + typeof(T));
                            _instance = singleton.AddComponent<T>();
                            DontDestroyOnLoad(singleton);
                        }
                        else
                        {
                            DontDestroyOnLoad(_instance);
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
