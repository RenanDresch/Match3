using Game.FX;
using UnityEngine;

namespace Game.Logic
{
    [RequireComponent(typeof(Camera))]
    public class GameCamera : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Camera cameraComponent = default;

        #endregion

        #region Private Methods

        private void Start()
        {
            var gameSettings = GameSettingsManager.Instance;

            cameraComponent.orthographicSize = gameSettings.GridXSize >= gameSettings.GridYSize ?
                (float)(gameSettings.GridXSize / 2) + 0.2f :
                (float)(gameSettings.GridYSize / 2) + 0.2f;
        }

        private void Reset()
        {
            cameraComponent = GetComponent<Camera>();
        }

        #endregion
    }
}