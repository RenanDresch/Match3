using Game.Models.App;
using Game.Models.Configs;
using Game.Models.GameLoop;
using Game.Models.Novelty;
using UnityEngine;

namespace Game.Models
{
    public partial class ModelsRepository
    {
        [SerializeField]
        GameStaticConfigsContainer gameStaticConfigsContainer;
        public GameStaticConfigsContainer GameStaticConfigsContainer => gameStaticConfigsContainer;

        [SerializeField]
        GameConfigsContainer gameConfigsContainer;
        public GameConfigsContainer GameConfigsContainer => gameConfigsContainer;
        
        [SerializeField]
        GamePrefabsContainer gamePrefabsContainer;
        public GamePrefabsContainer GamePrefabsContainer => gamePrefabsContainer;
        
        [SerializeField]
        [Container]
        GameLoopContainer gameLoopContainer;
        public GameLoopContainer GameLoopContainer
            => gameLoopContainer;
        
        [SerializeField]
        [Container]
        EntitiesContainer entitiesContainer;
        public EntitiesContainer EntitiesContainer
            => entitiesContainer;

        [SerializeField]
        [Container]
        AppContainer appContainer;
        public AppContainer AppContainer
            => appContainer;

        [SerializeField]
        [Container]
        NoveltyContainer noveltyContainer;
        public NoveltyContainer NoveltyContainer
            => noveltyContainer;
    }
}