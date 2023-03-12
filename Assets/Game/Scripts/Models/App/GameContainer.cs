using System;
using Game.Models.App.Scenes;
using Gaze.MCS;
using UnityEngine;

namespace Game.Models.App
{
    [Serializable]
    public class AppContainer
    {
        [SerializeField]
        [Container]
        SceneContainer sceneContainer;
        public SceneContainer SceneContainer => sceneContainer;
        
        [Volatile]
        public readonly IReactiveProperty<Camera> GameCamera = new ReactiveProperty<Camera>();

        [Volatile]
        public readonly IReactiveProperty<GameState> GameState = new ReactiveProperty<GameState>();
    }
}