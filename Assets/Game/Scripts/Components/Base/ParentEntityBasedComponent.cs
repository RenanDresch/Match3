using Game.Models;
using Game.Services;
using UnityEditor;
using UnityEngine;

namespace Game.Components
{
    public abstract class ParentEntityBasedComponent : BaseComponent
    {
        [SerializeField]
        GameObject entityNode;

        LateExecutionService lateExecutionService;

        protected EntityIdHandler EntityId { get; private set; }

        protected Entity Entity => models.Entities.EntitiesMap[EntityId]
                                                  .Value;

        //Cache field during editor time, avoiding runtime overhead
        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!entityNode)
            {
                var parentIEntity = GetComponent<IEntity>();
                parentIEntity ??= GetComponentInParent<IEntity>(true);

                if (parentIEntity == null)
                {
                    if (!PrefabUtility.IsPartOfPrefabAsset(gameObject))
                    {
                        Debug.LogWarning(
                            $"Unable to find parent Entity node for {transform}",
                            gameObject);
                    }
                }
                else
                {
                    entityNode = parentIEntity.Entity;
                }
            }
        }
        #endif

        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            lateExecutionService = new LateExecutionService(
                this,
                models);

            lateExecutionService.LateExecute(SetupEntityId);
        }

        void SetupEntityId()
        {
            var parentEntityNode = entityNode
                    ? entityNode
                    : gameObject.ParentEntityNode(models);

            EntityId = new EntityIdHandler(
                parentEntityNode,
                models.Entities,
                this);

            EntityId.ExecuteWhenAssigned(OnTargetEntityIdSet);
        }

        protected abstract void OnTargetEntityIdSet(
            int entityId
        );
    }
}