using UnityEngine;

namespace Tools
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance;

        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (Instance != null) return;
            Instance = this as T;
            
        }
    }
}
