using UnityEngine;

namespace Game.Contracts
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : MonoBehaviour
    { 
        #region Fields

        private static T instance;

        #endregion

        #region Properties

        public static T Instance
        {
            get
            {
                return instance;
            }
            protected set
            {
                if (instance != null)
                {
                    Destroy(instance.gameObject);
                }
                instance = value;
                DontDestroyOnLoad(value.gameObject);
            }
        }

        #endregion
    }
}