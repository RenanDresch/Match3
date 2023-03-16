using System;
using Game.Models.App;
using Game.Models.GameLoop.Jobs;
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
        [Volatile]
        public readonly IReactiveProperty<BoardJobData> BoardJobData = new ReactiveProperty<BoardJobData>();

        //[Volatile]
        //public readonly IReactiveProperty<BoardBuildingJob> BoardBuildingJob = new ReactiveProperty<BoardBuildingJob>(new BoardBuildingJob());
    }
}