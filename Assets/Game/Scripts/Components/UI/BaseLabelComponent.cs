using TMPro;
using UnityEngine;

namespace Game.Components.UI
{
    public abstract class BaseLabelComponent : BaseComponent
    {
        [SerializeField]
        [HideInInspector]
        protected TMP_Text label;
        
        #if UNITY_EDITOR
        void Reset()
        {
            label = GetComponent<TMP_Text>();
        }
        #endif
    }
}