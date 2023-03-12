using System;
using Game.Models.App;
using Gaze.MCS;

namespace Game.Models.GameLoop
{
    [Serializable]
    public class GameLoopContainer
    {
        [Volatile]
        public readonly IReactiveProperty<int> Score = new ReactiveProperty<int>();
        [Volatile]
        public readonly IReactiveProperty<int> TargetScore = new ReactiveProperty<int>();
        [Volatile]
        public readonly IReactiveProperty<int> SecondsLeft = new ReactiveProperty<int>();
        [Volatile]
        public readonly IReactiveProperty<TimerState> TimerState = new ReactiveProperty<TimerState>();
    }
}