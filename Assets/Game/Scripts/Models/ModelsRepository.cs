using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Models
{
    [CreateAssetMenu(menuName = "Data/ContainerRepository")]
    public partial class ModelsRepository : ScriptableObject
    {
        void OnEnable()
        {
            HandleEditorPlayStateChange();
        }

        [ContextMenu("Reset Models")]
        void ResetModel()
        {
            ModelsCleaner.Cleanup(this);
        }

        void HandleEditorPlayStateChange()
        {
            #if UNITY_EDITOR
            void ModeChanged(
                    PlayModeStateChange playModeStateChange
            )
            {
                if (playModeStateChange is PlayModeStateChange.ExitingPlayMode or PlayModeStateChange.ExitingEditMode)
                {
                    ModelsCleaner.Cleanup(this);
                }
            }

            EditorApplication.playModeStateChanged -= ModeChanged;
            EditorApplication.playModeStateChanged += ModeChanged;
            #endif
        }
    }
}