using System.Linq;
using UnityEditor;

namespace Game.Models.Tools
{
    /// <summary>
    /// Used by the editor to automatically inject the ModelsRepository asset field
    /// </summary>
    public static class ModelsRepositoryPickerTool
    {
        public static string GetContainerRepositoryGuid()
        {
            #if UNITY_EDITOR
            return AssetDatabase.FindAssets("t:ModelsRepository")
                                .First();
            #else
            return null;
            #endif
        }
    }
}