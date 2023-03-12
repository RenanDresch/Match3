using UnityEngine;

namespace Game.Components
{
    [RequireComponent(typeof(Camera))]
    public class BoardRendererCamera : BaseComponent
    {
        [SerializeField]
        [HideInInspector]
        Camera cameraComponent;
        
        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            SetupCamera();
        }
        
        void SetupCamera()
        {
            var xSize = models.GameConfigsContainer.BoardGridXSize.Value;
            var ySize = models.GameConfigsContainer.BoardGridYSize.Value;
            
            cameraComponent.orthographicSize = xSize >= ySize ?
                (float)xSize / 2 + 0.2f :
                (float)ySize / 2 + 0.2f;
        }
        
        #if UNITY_EDITOR

        void Reset()
        {
            cameraComponent = GetComponent<Camera>();
        }

        #endif
    }
}