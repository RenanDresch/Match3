using System;
using UnityEngine;

namespace Game.Models.Configs
{
    [Serializable]
    public class GameStaticConfigsContainer
    {
        [SerializeField]
        string menuSceneName;
        public string MenuSceneName => menuSceneName;
        
        [SerializeField]
        string gameplaySceneName;
        public string GameplaySceneName => gameplaySceneName;
    }
}