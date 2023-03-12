using System;
using System.Collections.Generic;
using Gaze.MCS;
using UnityEngine;

namespace Game.Models
{
    [Serializable]
    public class EntitiesContainer
    {
        [Volatile]
        public readonly IReactiveDictionary<Collider, Entity> ColliderToEntitiesMap =
                new ReactiveDictionary<Collider, Entity>(Constants.InitialEntityCapacity);

        [Volatile]
        public readonly IReactiveProperty<int> NextAssignableId =
                        new ReactiveProperty<int>();
        
        [Volatile]
        public readonly IReactiveQueue<int> DisposedIds =
                new ReactiveQueue<int>(Constants.InitialDisposedIdsCapacity);

        [Volatile]
        public readonly IReactiveDictionary<int, Entity> EntitiesMap =
                new ReactiveDictionary<int, Entity>(Constants.InitialEntityCapacity)
                       .WithCustomDefaultValueGetter(() => new Entity());

        [Volatile]
        public readonly IReactiveDictionary<GameObject, int> GameObjectToIdMap =
                new ReactiveDictionary<GameObject, int>(Constants.InitialEntityCapacity);

        [Volatile]
        public readonly IReactiveDictionary<int, GameObject> IdToGameObjectMap =
                new ReactiveDictionary<int, GameObject>(Constants.InitialEntityCapacity);

        [Volatile]
        public IReactiveProperty<HashSet<GameObject>> EntityGameObjects =
                new ReactiveProperty<HashSet<GameObject>>(new HashSet<GameObject>(Constants.InitialEntityCapacity));
    }
}