using Game.Models;
using Game.Services;

namespace Game.Components
{
    public abstract class EntityBasedComponent : BaseComponent
    {
        protected EntityIdHandler EntityId { get; private set; }
        protected Entity Entity { get; private set; }

        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            EntityId = new EntityIdHandler(
                    gameObject,
                    models.Entities,
                    this);

            EntityId.ExecuteWhenAssigned(CacheEntity);
            EntityId.ExecuteWhenAssigned(OnEntityIdSet);
        }

        void CacheEntity(
            int entityId
        )
        {
            Entity = models.Entities.EntitiesMap[EntityId]
                           .Value;
        }

        protected abstract void OnEntityIdSet(
            int entityId
        );
    }
}