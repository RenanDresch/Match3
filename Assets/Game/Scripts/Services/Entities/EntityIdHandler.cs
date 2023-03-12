using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Models;
using Gaze.MCS;
using Gaze.Utilities;
using UnityEngine;

namespace Game.Services
{
    public class EntityIdHandler
    {
        readonly CancellationTokenSource cancellationTokenSource = new();
        readonly IDestroyable destroyable;
        readonly EntitiesContainer entitiesContainer;
        readonly GameObject entity;
        readonly IReactiveProperty<int> entityId = new ReactiveProperty<int>();

        public EntityIdHandler(
            GameObject entity,
            EntitiesContainer entitiesContainer,
            IDestroyable destroyable
        )
        {
            if (!entity)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.entity = entity;
            this.entitiesContainer = entitiesContainer;
            this.destroyable = destroyable;

            destroyable.OnDestroyEvent += DisposeAsyncOperations;

            ApplicationLogger.WithLevel(LogLevel.Silly)
                            ?.Log($"Entity {entity.name} requested Id");

            entityId.SafeBindOnChangeAction(
                destroyable,
                LateReleaseIdHandler);

            UniTask.Void(
                AwaitIdAssignmentAsync,
                cancellationTokenSource.Token);
        }

        int Id
            => entityId.Value;

        static void AssertEntityIsNotNull(
            GameObject entity
        )
        {
            if (!entity)
            {
                ApplicationLogger.LogException(
                    new NullReferenceException("Entity cannot be null!"));
            }
        }

        void DisposeAsyncOperations()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        async UniTaskVoid AwaitIdAssignmentAsync(
            CancellationToken cancellationToken
        )
        {
            await UniTask.WaitUntil(
                () => entitiesContainer.GameObjectToIdMap[entity]
                                       .Value !=
                      0,
                cancellationToken: cancellationToken);

            await UniTask.NextFrame(cancellationToken);
            entityId.Value = entitiesContainer.GameObjectToIdMap[entity]
                                              .Value;
        }

        public void ExecuteWhenAssigned(
            Action<int> action
        )
        {
            if (entityId.Value != 0)
            {
                action(entityId.Value);
            }
            else
            {
                entityId.SafeBindOnChangeAction(
                    destroyable,
                    action);
            }
        }

        void LateReleaseIdHandler(
            int _
        )
        {
            UniTask.Void(
                LateReleaseIdHandlerAsync,
                cancellationTokenSource.Token);
        }

        async UniTaskVoid LateReleaseIdHandlerAsync(
            CancellationToken cancellationToken
        )
        {
            await UniTask.NextFrame(cancellationToken);
            entityId.Release();
        }

        public static implicit operator int(
            EntityIdHandler entityId
        )
        {
            return entityId.Id;
        }
    }
}