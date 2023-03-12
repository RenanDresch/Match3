using Game.Models;
using UnityEngine;

namespace Game.Components
{
    public static class GameObjectExtensions
    {
        public static GameObject ParentEntityNode(
            this GameObject childNode,
            ModelsRepositoryWrapper models
        )
        {
            return ParentEntityFromTransform(
                childNode.transform,
                models);
        }

        static GameObject ParentEntityFromTransform(
            Transform childNodeTransform,
            ModelsRepositoryWrapper models
        )
        {
            var nodeTransform = childNodeTransform;
            do
            {
                var node = nodeTransform.gameObject;
                if (models.Entities.EntityGameObjects.Value.Contains(node))
                {
                    return node;
                }

                nodeTransform = nodeTransform.parent;
            }
            while (nodeTransform);
            
            ApplicationLogger.WithLevel(LogLevel.Error)
                            ?.Log($"Failed to get parent Entity Game Object node for {nodeTransform}");

            return null;
        }
    }
}