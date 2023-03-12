using UnityEngine;

namespace Game.Components
{
    [RequireComponent(typeof(Camera))]
    public class GameCamera : BaseComponent
    {
        [SerializeField]
        [HideInInspector]
        Camera cameraComponent;
        
        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            models.AppContainer.GameCamera.Value = cameraComponent;
        }
        
        #if UNITY_EDITOR

        void Reset()
        {
            cameraComponent = GetComponent<Camera>();
        }

        #endif
    }
}