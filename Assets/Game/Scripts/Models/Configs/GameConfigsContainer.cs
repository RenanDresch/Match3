using System;
using Gaze.MCS;

namespace Game.Models.Configs
{
    [Serializable]
    public class GameConfigsContainer
    {
        [Volatile]
        public readonly IReactiveProperty<int> BoardGridXSize = new ReactiveProperty<int>();
        [Volatile]
        public readonly IReactiveProperty<int> BoardGridYSize = new ReactiveProperty<int>();
        [Volatile]
        public readonly IReactiveProperty<int> InitialAvailableSeconds = new ReactiveProperty<int>();
        [Volatile]
        public readonly IReactiveProperty<int> InitialTargetScore = new ReactiveProperty<int>();
    }
}