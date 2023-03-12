using Game.Models;
using Gaze.Utilities;

namespace Game.Services
{
    public abstract class BaseEntityService : BaseService
    {
        protected readonly int entityId;

        protected BaseEntityService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models,
            int entityId
        ) :
                base(
                    destroyable,
                    models)
        {
            this.entityId = entityId;
        }

        protected Entity Entity => Models.Entities.EntitiesMap[entityId]
                                                  .Value;
    }
}