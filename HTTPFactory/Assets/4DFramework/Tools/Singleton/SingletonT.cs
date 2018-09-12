using System;
using UnityEngine;

namespace Magicant.Util
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool Persistent;

        protected static T instance;

        public static T Instance
        {
            get
            {
                if (Singleton<T>.instance == null)
                {
                    Singleton<T>.instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
                    if (Singleton<T>.instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                    }
                }
                return Singleton<T>.instance;
            }
        }

        protected virtual void Awake()
        {
            if (this != Singleton<T>.Instance)
            {
                UnityEngine.Object.Destroy(base.gameObject);
                return;
            }
            if (this.Persistent)
            {
                UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            }
        }
    }
}
