using System;
using Gaze.MCS;

namespace Game.Models.App.Scenes
{
    [Serializable]
    public class SceneContainer
    {
        [Volatile]
        public readonly IReactiveProperty<string> SceneName = new ReactiveProperty<string>();
        [Volatile]
        public readonly IReactiveProperty<bool> IsSwitchingScene = new ReactiveProperty<bool>();
    }
}